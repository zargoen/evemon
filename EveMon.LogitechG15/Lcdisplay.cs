using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using lgLcdClassLibrary;

namespace EVEMon.LogitechG15
{
    public partial class Lcdisplay : IDisposable
    {
        private Lcdisplay() 
        {
            _defaultFont = new Font("Microsoft Sans Serif", 13.5f, FontStyle.Regular, GraphicsUnit.Point);
            _bufferOut = new byte[6880];
            _defBrush = Brushes.Black;
            _bmpLCD = new Bitmap(160, 43);
            _bmpLCD.SetResolution(46f, 46f);
            _lcdGraphics = Graphics.FromImage(_bmpLCD);
            _lcdGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            _bmpLCDX = new Bitmap(160, 43);
            _bmpLCDX.SetResolution(46f, 46f);
            _lcdGraphicsX = Graphics.FromImage(_bmpLCDX);
            _lcdGraphicsX.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

            _lines = new string[3] { "", "", "" };
            _formatStringConverted = FORMAT_CHAR_SKILL_TIME_STRING.Replace("[c]", "{0}").Replace("[s]", "{1}").Replace("[t]", "{2}");

            using (Stream strm = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("EVEMon.LogitechG15.EVEMon-all.ico")) {
                _eveMonSplash = new Icon(strm);
            }

            _LCD = new LCDInterface();
            _LCD.Open("EveMon", false);
            _btnstatehld = DateTime.Now;
            _cycletime = DateTime.Now.AddSeconds(10);
            _cycleini = false;
            _cycle = false;
            _COMPLETESTR = "Hotaru Nakayoshi\nhas finished learning skill\nHull Upgrades V";
            _leasttime = TimeSpan.FromTicks(DateTime.Now.Ticks);
        }

        #region Cleanup
        private bool _disposed = false;
        ~Lcdisplay() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool bDisposing) {
            if (!this._disposed) {
                if (bDisposing || _LCD != null) {
                    _LCD.Close();
                    _LCD = null;
                }
                _singleInstance = null;
            }
            _disposed = true;
        }
        #endregion

        //const int MIN_TIME_BETWEEN_REPAINT_MS = 200;
        const int STATE_SPLASH = 1;
        const int STATE_CLIST = 2;
        const int STATE_SKILLC = 3;
        const int STATE_CHARS = 4;
        const int STATE_S_CYCLE = 5;
        const int STATE_S_REFRESH = 6;
        /// <summary>
        /// Format for Character / Skill / Time mode. 
        /// Use [c] for character, [s] for skill name, and [t] for Time String
        /// </summary>
        const string FORMAT_CHAR_SKILL_TIME_STRING = "[c] Training Status:\n[s]\n[t]";        
        private static Lcdisplay _singleInstance;
        private volatile bool _shouldTerminate;

        private LCDInterface _LCD;
        private LcdisplayMode _displayMode = LcdisplayMode.CharSkillTimeMode;
        private Font _defaultFont;
        private Brush _defBrush;
        private Bitmap _bmpLCD;
        private Graphics _lcdGraphics;
        private Bitmap _bmpLCDX;
        private Graphics _lcdGraphicsX;
        private string _formatStringConverted;
        private DateTime _lastRepaint;
        //private bool _showSplash = true;
        private Icon _eveMonSplash;

        private string _newchar;
        private string _characterName;
        //private string _lastCharacterName;
        private string _curSkillTraining;
        //private string _lastCurSkillTraining;
        private double _curperc;
        private TimeSpan _timeToSkillComplete;
        private string _leastchar;
        private TimeSpan _leasttime;
        //private TimeSpan _lastTimeToSkillComplete;
        private string[] _lines;
        //private string[] _lastLines;
        private Byte[] _bufferOut;
        private Byte[] _lastBufferOut;
        private long _buttonstate;
        private string[] _charlist;
        private long _oldbuttonstate;
        private DateTime _btnstatehld;
        private DateTime _painttime;
        private DateTime _holdtime;
        private int _state;
        private bool _cycle;
        private bool _cycleini;
        private int _cycleint;
        private DateTime _cycletime;
        private string _refreshchar;
        public string _COMPLETESTR;
        private bool _skillcchg;

        public bool cycle 
        {
            get { return _cycle; }
            set { _cycle = value; }
        }
        public bool cycleini 
        {
            get { return _cycleini; }
            set { _cycleini = value; }
        }
        public int cycleint 
        {
            get { return _cycleint; }
            set { _cycleint = value; }
        }
        public string refreshchar
        {
            get { return _refreshchar; }
            set { _refreshchar = value; }
        }

        public LcdisplayMode LcdisplayMode 
        {
            get { return _displayMode; }
            //set { _displayMode = value; _displayModeHasChanged = true; }
            // Not going to implement other display modes right now. Will do later.
        }

        public void GetButtonState() 
        {
            // called every 50ms to scan current button states
            _LCD.ReadSoftButtons(ref _buttonstate);
            if (_oldbuttonstate != _buttonstate)
            {
                // get all buttons who havent been pressed last time
                long _press = (_oldbuttonstate ^ _buttonstate) & _buttonstate;
                // get all buttons who have been pressed last time
                //long _unpress = (_oldbuttonstate ^ _buttonstate) & _oldbuttonstate;

                for (int i = 1; i <= 8; i=i*2)
                {
                    if ((_press & i) > 0)
                    {
                        // button i pressed
                        if (i == 1)
                        {
                            displayChars();
                        }
                        if (i == 2)
                        {
                            // select next skill ready char
                            //switchState(STATE_SKILLC);
                            if (_leastchar != null)
                            {
                                _newchar = _leastchar;
                                reorderList(false);
                                _cycle = false;
                                switchState(STATE_CHARS);
                            }
                        }
                        if (i == 4)
                        {
                            // update charinfo
                            if (_state == STATE_CHARS || _state == STATE_CLIST)
                            {
                                _refreshchar = _characterName;
                                switchState(STATE_S_REFRESH);
                            }
                        }
                        if (i == 8)
                        {
                            switchCycle();
                            switchState(STATE_S_CYCLE);
                            _cycletime = DateTime.Now;
                        }
                    }
                    //if ((_unpress & i) > 0)
                    //{
                        // button i released
                    //}
                }
                _oldbuttonstate = _buttonstate;
            }
        }
        public void SkillCompleted() 
        {
            switchState(STATE_SKILLC);
            DoRepaint();
        }
        public void switchCycle() 
        {
            if (_cycle == true)
                _cycle = false;
            else
                _cycle = true;
        }
        public void displayChars() 
        {
            bool nextchar = false;
            TimeSpan now = TimeSpan.FromTicks(DateTime.Now.Ticks - _btnstatehld.Ticks);
            if (now.TotalMilliseconds < 2000)
            {
                nextchar = true;
                _cycle = false;
            }
            _btnstatehld = DateTime.Now;

            if (nextchar)
                reorderList(nextchar);
            switchState(STATE_CLIST);
            DoRepaint();
        }
        private void reorderList(bool nextchar)
        {
            string[] nlist = new string[_charlist.Length];
            int nlisti = 0;
            bool enlist = false;
            for (int i = 0; i < _charlist.Length; i++)
            {
                if (_charlist[i] == _newchar)
                {
                    if (nextchar)
                    {
                        if (_charlist.Length > i + 1)
                        {
                            _newchar = _charlist[i + 1];
                        }
                        else
                        {
                            _newchar = _charlist[0];
                        }
                        nextchar = false;
                    }
                    else
                    {
                        nlist[nlisti] = _charlist[i];
                        nlisti++;
                        enlist = true;
                    }
                }
                else if (enlist)
                {
                    nlist[nlisti] = charlist[i];
                    nlisti++;
                }
            }
            for (int i = 0; i < _charlist.Length; i++)
            {
                if (_charlist[i] == _newchar)
                {
                    break;
                }
                nlist[nlisti] = _charlist[i];
                nlisti++;
            }

            if (nlist[0] != null)
            {
                _charlist = nlist;
            }
        }

        public string CharacterName 
        {
            get { return _characterName; }
            set { _characterName = value; }
        }
        public string newchar 
        {
            get { return _newchar; }
            set { _newchar = value; }
        }
        public string CurrentSkillTrainingText 
        {
            get { return _curSkillTraining; }
            set { _curSkillTraining = value; }
        }
        public TimeSpan TimeToComplete 
        {
            get { return _timeToSkillComplete; }
            set { _timeToSkillComplete = value; }
        }
        public string leastchar 
        {
            get { return _leastchar; }
            set { _leastchar = value; }
        }
        public TimeSpan leasttime
        {
            get { return _leasttime; }
            set { _leasttime = value; }
        }
        public double curperc 
        {
            get { return _curperc; }
            set { _curperc = value; }
        }
        public string[] charlist 
        {
            get { return _charlist; }
            set { _charlist = value; }
        }
        public bool IsRunning {
            get { return (!_shouldTerminate); }
        }
        public static Lcdisplay Instance() {
            if (_singleInstance == null) {
                _singleInstance = new Lcdisplay();
            }
            return _singleInstance;
        }

        public void Start() {
            Start(LcdisplayMode.CharSkillTimeMode);
        }        
        private void Start(LcdisplayMode mode)
        {
            this._displayMode = mode;
            this._shouldTerminate = false;
            //this._showSplash = true;
            DateTime dtThredStart = DateTime.Now;

            switchState(STATE_SPLASH);
            ShowSplash();

            //Main loop
            while (!_shouldTerminate) 
            {
                DoRepaint();
                Thread.Sleep(50);
            }
        }

        public void Stop() 
        {
            this._shouldTerminate = true;
        }

        private void switchState(int state) 
        {
            _state = state;
            _painttime = DateTime.Now;
            _holdtime = DateTime.Now;
        }

        private void DoRepaint() 
        {
            TimeSpan now = TimeSpan.FromTicks(DateTime.Now.Ticks - _painttime.Ticks);
            if (now.TotalMilliseconds < 200)
            {
                return;
            }
            now = TimeSpan.FromTicks(DateTime.Now.Ticks - _holdtime.Ticks);
            if (_state == STATE_SPLASH && now.TotalSeconds > 4)
            {
                switchState(STATE_CHARS);
            }
            if (_state == STATE_CLIST && now.TotalMilliseconds > 2000)
            {
                switchState(STATE_CHARS);
            }
            if (_state == STATE_SKILLC && now.TotalSeconds > 14)
            {
                switchState(STATE_CHARS);
            }
            if ((_state == STATE_S_CYCLE || _state == STATE_S_REFRESH) && now.TotalSeconds > 2)
            {
                switchState(STATE_CHARS);
            }
            
            switch (_state) 
            {
                case STATE_SPLASH:
                    _painttime = _painttime.AddSeconds(2);
                    ShowSplash();
                    return;
                case STATE_CHARS:
                    if (_cycle)
                    {
                        if (TimeSpan.FromTicks(DateTime.Now.Ticks-_cycletime.Ticks).TotalSeconds > _cycleint)
                        {
                            _cycletime = DateTime.Now;
                            reorderList(true);
                        }
                    }
                    _painttime = _painttime.AddSeconds(1);
                    DoRepaint_CharSkillTimeMode();
                    return;
                case STATE_CLIST:
                    _painttime = _painttime.AddSeconds(2);
                    DoRepaint_charlist();
                    return;
                case STATE_SKILLC:
                    _painttime = _painttime.AddMilliseconds(800);
                    DoRepaint_skillcomplete();
                    return;
                case STATE_S_CYCLE:
                    _painttime = _painttime.AddSeconds(2);
                    DoRepaint_s_cycle();
                    return;
                case STATE_S_REFRESH:
                    _painttime = _painttime.AddSeconds(2);
                    DoRepaint_s_refresh();
                    return;
                default:
                    return;
            }
        }
        private void DoRepaint_CharSkillTimeMode() 
        {
            if (_characterName == null || _characterName == "")
                _characterName = "No Character";
            if (_curSkillTraining == null || _curSkillTraining == "")
                _curSkillTraining = "None";

            string tmpTxt = string.Format(_formatStringConverted, _characterName, _curSkillTraining, FormatTimeSpan(_timeToSkillComplete));
            string[] tmpLines = tmpTxt.Split("\n".ToCharArray());
            if (tmpLines.Length == 3) 
            {
                _lines[0] = tmpLines[0];
                _lines[1] = tmpLines[1];
                _lines[2] = tmpLines[2];
            }
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);
            RectangleF recLine1 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[0], _defaultFont));
            RectangleF recLine2 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[1], _defaultFont));
            RectangleF recLine3 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[2], _defaultFont));
            RectangleF recLine4 = new RectangleF(new PointF(0f, 0f), new SizeF(40f, 10f));
            recLine1.Offset(0f, -1f);
            recLine2.Offset(0f, recLine1.Bottom);
            recLine3.Offset(0f, recLine2.Bottom);

            Pen test = new Pen(_defBrush);

            int len = Convert.ToInt16(_curperc * 157);
            float offset = 66f;
            if (_timeToSkillComplete.Ticks == 0)
            {
                _curperc = 1;
                offset = 61f;
            }
            recLine4.Offset(offset, recLine3.Bottom - 1);
            string perc = _curperc.ToString("P2");
            _lcdGraphics.DrawString(_lines[0], _defaultFont, _defBrush, recLine1);
            _lcdGraphics.DrawString(_lines[1], _defaultFont, _defBrush, recLine2);
            _lcdGraphics.DrawString(_lines[2], _defaultFont, _defBrush, recLine3);
            _lcdGraphicsX.DrawString(perc, _defaultFont, _defBrush, recLine4);

            //_lcdGraphics.DrawRectangle(test, 0, 0, 159, 10);
            _lcdGraphics.DrawRectangle(test, 1, (recLine3.Bottom + 1), 158, 8);
            _lcdGraphics.FillRectangle(_defBrush, 2, (recLine3.Bottom + 2), len, 7);
            DoRepaint_RawBitMapMode();
        }
        private void DoRepaint_skillcomplete() 
        {
            string[] tmpLines = _COMPLETESTR.Split("\n".ToCharArray());
            if (tmpLines.Length > 1)
            {
                _lines[0] = tmpLines[0];
                _lines[1] = tmpLines[1];
                _lines[2] = tmpLines[2];
            }
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);
            RectangleF recLine1 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[0], _defaultFont));
            RectangleF recLine2 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[1], _defaultFont));
            RectangleF recLine3 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[2], _defaultFont));
            
            float offset = (159 - recLine1.Width) / 2;
            recLine1.Offset(offset, -1f);
            offset = (159 - recLine2.Width) / 2;
            recLine2.Offset(offset, recLine1.Bottom);
            offset = (159 - recLine3.Width) / 2;
            recLine3.Offset(offset, recLine2.Bottom);

            _lcdGraphics.DrawString(_lines[0], _defaultFont, _defBrush, recLine1);
            _lcdGraphics.DrawString(_lines[1], _defaultFont, _defBrush, recLine2);
            _lcdGraphics.DrawString(_lines[2], _defaultFont, _defBrush, recLine3);

             if (_skillcchg)
            {
                double tmp = 1;
                string perc = tmp.ToString("P2");
                Pen test = new Pen(_defBrush);
                RectangleF recLine4 = new RectangleF(new PointF(0f, 0f), new SizeF(40f, 10f));

                recLine4.Offset(61f, recLine3.Bottom - 1);
                _lcdGraphicsX.DrawString(perc, _defaultFont, _defBrush, recLine4);
                //_lcdGraphics.DrawRectangle(test, 0, 0, 159, 10);
                _lcdGraphics.DrawRectangle(test, 1, (recLine3.Bottom + 1), 158, 8);
                _lcdGraphics.FillRectangle(_defBrush, 2, (recLine3.Bottom + 2), 157, 7);
                _skillcchg = false;
            }
            else
            {
                _skillcchg = true;
            }
            DoRepaint_RawBitMapMode(LcdisplayPriority.Alert);
        }
        private void DoRepaint_charlist() 
        {
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);
            RectangleF recLine1 = new RectangleF(new PointF(0f, 0f), new SizeF(158f, 11f));
            RectangleF recLine2 = new RectangleF(new PointF(0f, 0f), new SizeF(158f, 11f));
            RectangleF recLine3 = new RectangleF(new PointF(0f, 0f), new SizeF(158f, 11f));
            RectangleF recLine4 = new RectangleF(new PointF(0f, 0f), new SizeF(158f, 11f));
            recLine1.Offset(0f, -1f);
            recLine2.Offset(0f, recLine1.Bottom);
            recLine3.Offset(0f, recLine2.Bottom);
            recLine4.Offset(0f, recLine3.Bottom);

            _lcdGraphicsX.DrawString(_charlist[0], _defaultFont, _defBrush, recLine1);
            if (_charlist.Length > 1)
                _lcdGraphics.DrawString(_charlist[1], _defaultFont, _defBrush, recLine2);
            if (_charlist.Length > 2)
                _lcdGraphics.DrawString(_charlist[2], _defaultFont, _defBrush, recLine3);
            if (_charlist.Length > 3)
                _lcdGraphics.DrawString(_charlist[3], _defaultFont, _defBrush, recLine4);

            //_lcdGraphics.DrawRectangle(test, 0, 0, 159, 10);
            //_lcdGraphics.DrawRectangle(test, 0, 0, 159, 10);
            _lcdGraphics.FillRectangle(_defBrush, 0, 0, 159, 11);
            DoRepaint_RawBitMapMode();
        }
        private void DoRepaint_s_cycle()
        {
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);
            string[] _line = new string[2];

            _line[0] = "Autocycle is now ";
            if (_cycle)
                _line[0] += "on";
            else
                _line[0] += "off";

            _line[1] = "Cycle Time is: " + _cycleint + "s";

            RectangleF recLine1 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_line[0], _defaultFont));
            RectangleF recLine2 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_line[1], _defaultFont));

            float offset = (159 - recLine1.Width) / 2;
            recLine1.Offset(offset, -1f);
            offset = (159 - recLine2.Width) / 2;
            recLine2.Offset(offset, recLine1.Bottom);

            _lcdGraphics.DrawString(_line[0], _defaultFont, _defBrush, recLine1);
            _lcdGraphics.DrawString(_line[1], _defaultFont, _defBrush, recLine2);
            DoRepaint_RawBitMapMode();
        }
        private void DoRepaint_s_refresh()
        {
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);

            _lines[0] = "Refreshing Character Information";
            _lines[1] = "of";
            _lines[2] = _refreshchar;

            RectangleF recLine1 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[0], _defaultFont));
            RectangleF recLine2 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[1], _defaultFont));
            RectangleF recLine3 = new RectangleF(new PointF(0f, 0f), _lcdGraphics.MeasureString(_lines[2], _defaultFont));

            float offset = (159 - recLine1.Width) / 2;
            recLine1.Offset(offset, -1f);
            offset = (159 - recLine2.Width) / 2;
            recLine2.Offset(offset, recLine1.Bottom);
            offset = (159 - recLine3.Width) / 2;
            recLine3.Offset(offset, recLine2.Bottom);
            _lcdGraphics.DrawString(_lines[0], _defaultFont, _defBrush, recLine1);
            _lcdGraphics.DrawString(_lines[1], _defaultFont, _defBrush, recLine2);
            _lcdGraphics.DrawString(_lines[2], _defaultFont, _defBrush, recLine3);


            DoRepaint_RawBitMapMode();
        }
        private void ShowSplash() 
        {
            _lcdGraphics.Clear(Color.White);
            _lcdGraphicsX.Clear(Color.White);
            _lcdGraphics.DrawIcon(_eveMonSplash, new Rectangle(64, 5, 32, 32));
            DoRepaint_RawBitMapMode(LcdisplayPriority.Alert);
        }

        private void DoRepaint_RawBitMapMode() { DoRepaint_RawBitMapMode(LcdisplayPriority.Normal); }
        private void DoRepaint_RawBitMapMode(LcdisplayPriority priority)
        {
            int bNdx = 0;
            bool bitBOn = false;
            bool bitBOn2 = false;
            for (int y = 0; y < 43; y++) 
            {
                for (int x = 0; x < 160; x++) 
                {
                    bNdx = (160 * y) + x;
                    Color bc = _bmpLCD.GetPixel(x, y);
                    Color bc2 = _bmpLCDX.GetPixel(x, y);
                    bitBOn = (bc.GetBrightness() < .4f);
                    bitBOn2 = (bc2.GetBrightness() < .4f);
                    if (bitBOn && bitBOn2)
                    {
                        bitBOn = false;
                    }
                    else if (bitBOn || bitBOn2)
                    {
                        bitBOn = true;
                    }
                    _bufferOut[bNdx] = (byte)(bitBOn ? 255 : 0);
                }
            }

            if(_displayMode == LcdisplayMode.RawBitMapMode)
            {
                _lastBufferOut = new byte[6880];
                _bufferOut.CopyTo(_lastBufferOut, 0);
            }
            _LCD.DisplayBitmap(ref _bufferOut[0], (int)priority);
            _lastRepaint = DateTime.Now;
        }

        static string FormatTimeSpan(TimeSpan timespan) 
        {
            string rtn = "";
            if (timespan.TotalDays >= 1) 
                rtn += timespan.Days.ToString() + "d ";

            if (timespan.Hours < 10)
                rtn += "0";
            rtn += timespan.Hours.ToString() + "h ";
            if (timespan.Minutes < 10)
                rtn += "0";
            rtn += timespan.Minutes.ToString() + "m ";
            if (timespan.Seconds < 10)
                rtn += "0";
            rtn += timespan.Seconds.ToString() + "s";
            if (timespan.Ticks == 0)
                rtn = "Done";
            return rtn;
        }
    }

    public enum LcdisplayMode
    {
        RawBitMapMode,      //If you want to do your own custom bitmaps
        ThreeTextLineMode,  //If you want to display your own text lines
        CharSkillTimeMode   //Simple update mode (re-paints optimized)
    }
    public enum LcdisplayPriority
    {
        Alert = lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_ALERT,
        Normal = lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_NORMAL,
        Background = lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_BACKGROUND,
        Idle = lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_IDLE_NO_SHOW
    }
}
