using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using lgLcdClassLibrary;

namespace EVEMon.G15Display
{
    public enum LCDImagePriority
    {
        Alert,
        Idle,
        Background,
        Normal,
    }

    public delegate void ConfigHandler();
    public delegate void ButtonHandler();

    public class DisplayManager
    {
        public const int BUTTON1_VALUE = 1;
        public const int BUTTON2_VALUE = 2;
        public const int BUTTON3_VALUE = 4;
        public const int BUTTON4_VALUE = 8;
        private static ButtonDelegate bDelegate;
        private static ConfigureDelegate cDelegate;

        public static event ConfigHandler OnConfigRequest;
        public static event ButtonHandler OnButton1Down;
        public static event ButtonHandler OnButton2Down;
        public static event ButtonHandler OnButton3Down;
        public static event ButtonHandler OnButton4Down;
        public static event ButtonHandler OnButton1Up;
        public static event ButtonHandler OnButton2Up;
        public static event ButtonHandler OnButton3Up;
        public static event ButtonHandler OnButton4Up;

        private static bool m_b1 = false;
        private static bool m_b2 = false;
        private static bool m_b3 = false;
        private static bool m_b4 = false;
        public static bool Button1
        {
            get
            {
                return m_b1;
            }
            set
            {
                if (value != m_b1)
                {
                    if (value == true && OnButton1Down != null)
                        OnButton1Down();
                    if (value == false && OnButton1Up != null)
                        OnButton1Up();
                }
            }
        }
        public static bool Button2
        {
            get
            {
                return m_b2;
            }
            set
            {
                if (value != m_b2)
                {
                    if (value == true && OnButton2Down != null)
                        OnButton2Down();
                    if (value == false && OnButton2Up != null)
                        OnButton2Up();
                }
            }
        }
        public static bool Button3
        {
            get
            {
                return m_b3;
            }
            set
            {
                if (value != m_b3)
                {
                    if (value == true && OnButton3Down != null)
                        OnButton3Down();
                    if (value == false && OnButton3Up != null)
                        OnButton3Up();
                }
            }
        }
        public static bool Button4
        {
            get
            {
                return m_b4;
            }
            set
            {
                if (value != m_b4)
                {
                    if (value == true && OnButton4Down != null)
                        OnButton4Down();
                    if (value == false && OnButton4Up != null)
                        OnButton4Up();
                }
            }
        }
        
        public const int Width = 160;
        public const int Height = 43;

        private static byte[] m_CurrentDisplay;
        private static LCDInterface LCD;
        private static bool isInit = false;
        public static bool IsOpen { get { return isInit; } }



        private static int ButtonDelegateImplementation(int device, uint dwButtons, IntPtr pContext)
        {
            ButtonPressedChecker c = new ButtonPressedChecker(dwButtons);
            Button1 = c.Check(ButtonsPressed.Button1);
            Button2 = c.Check(ButtonsPressed.Button2);
            Button3 = c.Check(ButtonsPressed.Button3);
            Button4 = c.Check(ButtonsPressed.Button4);
            return 0;
        }
        private static int ConfigureDelegateImplementation(int connection, IntPtr pContext)
        {
            if (OnConfigRequest != null)
                OnConfigRequest();
            return 0;
        }


        public static void Init(string name, bool letstart, bool assignDelegates)
        {
            LCD = new LCDInterface();

            if (assignDelegates)
            {
                bDelegate = new ButtonDelegate(ButtonDelegateImplementation);
                cDelegate = new ConfigureDelegate(ConfigureDelegateImplementation);
                LCD.AssignButtonDelegate(bDelegate);
                LCD.AssignConfigDelegate(cDelegate);
            }

            m_CurrentDisplay = new byte[Width * Height];
            isInit = LCD.Open(name, letstart);

        }
        public static bool Close()
        {
            isInit = !LCD.Close();
            return !isInit;
        }

        public static Bitmap CreateG15Bitmap()
        {

            Bitmap b = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            if (isInit)
            {
                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Black);
            }
            return b;
        }
        public static void ReDisplay()
        {
            ReDisplay(LCDImagePriority.Normal);
        }

        private static long PriorityToLong(LCDImagePriority from)
        {
            switch (from)
            {

                case LCDImagePriority.Alert:
                    return lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_ALERT;
                case LCDImagePriority.Background:
                    return lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_BACKGROUND;
                case LCDImagePriority.Idle:
                    return lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_IDLE_NO_SHOW;
                case LCDImagePriority.Normal:
                    return lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_NORMAL;
                default:
                    return lgLcdClassLibrary.LCDInterface.lglcd_PRIORITY_NORMAL;
            }
        }

        public static void ReDisplay(LCDImagePriority priority)
        {
            if (isInit)
            {
                LCD.DisplayBitmap(ref m_CurrentDisplay[0], PriorityToLong(priority));
            }
        }

        public static void Display(Bitmap bmp)
        {
            Display(bmp, LCDImagePriority.Normal);
        }
        public static void Display(Bitmap bmp, LCDImagePriority priority)
        {
            if (isInit)
            {
                try
                {
                    m_CurrentDisplay = new byte[Width * Height];
                    Color pixelColor;
                    Byte pixelValue;
                    for (Int32 HIndex = 0; HIndex < Height; HIndex++)
                    {
                        for (Int32 WIndex = 0; WIndex < Width; WIndex++)
                        {
                            pixelColor = bmp.GetPixel(WIndex, HIndex);
                            pixelValue = pixelColor.G;
                            m_CurrentDisplay[WIndex + (HIndex * Width)] = pixelValue;
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (bmp != null) bmp.Dispose();
                    LCD.DisplayBitmap(ref m_CurrentDisplay[0], PriorityToLong(priority));

                }
            }
        }


        public class Drawing
        {
            private static Brush mBrush = Brushes.White;
            private static Pen mPen = Pens.White;


            public static Brush WhiteBrush { get { return mBrush; } }
            public static Pen WhitePen { get { return mPen; } }


            public static void DrawPositionBar(Graphics g, Rectangle r, int percent)
            {
                if (r.Width < 3 || r.Height < 3)
                    return;
                if (r.Left < 0 || r.Right > DisplayManager.Width || r.Top < 0 || r.Bottom > DisplayManager.Height)
                    return;

                g.DrawRectangle(mPen, r);
                int maxwidth = (int)Math.Ceiling(r.Width - 2 / 10d);
                if (maxwidth % 2 == 0)
                    maxwidth++;
                if (maxwidth > 5)
                    maxwidth = 5;
                if (maxwidth < 1)
                    maxwidth = 1;

                int startx = r.X + 1 + (maxwidth - 1 / 2);
                int maxlength = (r.X + r.Width - 1) - (maxwidth - 1 / 2);
                int pos = (int)(Math.Round((double)maxlength / 100, 4) * percent);
                g.FillRectangle(mBrush, new Rectangle((startx + pos - (maxwidth - 1 / 2)), r.Y, maxwidth, r.Height));
            }
            public static void DrawPositionBar(Graphics g, Point point, Size size, int percent)
            {
                DrawPositionBar(g, new Rectangle(point, size), percent);
            }
            public static void DrawPositionBar(Graphics g, int x, int y, int width, int height, int percent)
            {
                DrawPositionBar(g, new Rectangle(x, y, width, height), percent);
            }



            public static void DrawProgressBar(Graphics g, Rectangle r, int percent)
            {
                if (r.Width < 3 || r.Height < 3)
                    return;
                if (r.Left < 0 || r.Right > DisplayManager.Width || r.Top < 0 || r.Bottom > DisplayManager.Height)
                    return;

                g.DrawRectangle(mPen, r);
                int wid = (int)(Math.Round((double)r.Width / 100, 4) * percent);

                int startx = r.X + 1;
                //int pos = (int)(Math.Round((double)maxlength / 100, 4) * percent);
                g.FillRectangle(mBrush, new Rectangle(startx, r.Y, wid, r.Height));
            }
            public static void DrawProgressBar(Graphics g, Point point, Size size, int percent)
            {
                DrawPositionBar(g, new Rectangle(point, size), percent);
            }
            public static void DrawProgressBar(Graphics g, int x, int y, int width, int height, int percent)
            {
                DrawPositionBar(g, new Rectangle(x, y, width, height), percent);
            }

            public static void DrawCheckBox(Graphics g, Rectangle r, bool ischecked)
            {
                if (r.Width < 3 || r.Height < 3)
                    return;
                if (r.Left < 0 || r.Right > DisplayManager.Width || r.Top < 0 || r.Bottom > DisplayManager.Height)
                    return;

                g.DrawRectangle(mPen, r);
                if (ischecked)
                {
                    g.DrawLine(mPen, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
                    g.DrawLine(mPen, r.X + r.Width, r.Y, r.X, r.Y + r.Height);

                }
            }
            public static void DrawCheckBox(Graphics g, Point point, Size size, bool ischecked)
            {
                DrawCheckBox(g, new Rectangle(point, size), ischecked);
            }
            public static void DrawCheckBox(Graphics g, int x, int y, int width, int height, bool ischecked)
            {
                DrawCheckBox(g, new Rectangle(x, y, width, height), ischecked);
            }
        }

    }
    [Flags]
    public enum ButtonsPressed
    {
        Button1 = 1,
        Button2 = 2,
        Button3 = 4,
        Button4 = 8
    }

    public class ButtonPressedChecker
    {
        private ButtonsPressed pressed;
        public ButtonPressedChecker(uint from)
        {
            try
            {
                pressed = (ButtonsPressed)Enum.Parse(typeof(ButtonsPressed), from.ToString());
            }
            catch
            {
                pressed = 0;
            }
        }
        public bool Check(ButtonsPressed to)
        {
            return (pressed & to) == to;
        }
    }

}
