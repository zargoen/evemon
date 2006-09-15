using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using EVEMon.Common;

namespace EVEMon.Sales
{
    [DefaultMineralParser("battleclinic")]
    public class BattleclinicParser : IMineralParser
    {

        private static Regex mineralTokenizer =
            new Regex(@"<name>(?<name>.+?)</name>.+?<price>(?<price>.+?)</price>",
                      RegexOptions.Compiled | ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline) 
                        | RegexOptions.Multiline) 
                        | RegexOptions.IgnoreCase);

        #region BattleclinicParser Members
        public string Title
        {
            get { return "Battleclinic.com EVE Wide Averages"; }
        }

        public string CourtesyUrl
        {
            get { return "http://eve.battleclinic.com/eve_online/market.php"; }
        }

        public string CourtesyText
        {
            get { return "Battleclinic.com EVE Wide Averages"; }
        }

        public IEnumerable<Pair<string, decimal>> GetPrices()
        {
            string content = null;
            try
            {
                content = EVEMonWebRequest.GetUrlString("http://eve.battleclinic.com/eve_online/market.php?feed=xml");
            }
            catch (EVEMonNetworkException ne)
            {
                ExceptionHandler.LogException(ne, true);
                throw new MineralParserException(ne.Message);
            }

            //scan for prices
            
            MatchCollection mc = mineralTokenizer.Matches(content);

            Decimal price = 0;
            foreach (Match mineral in mc)
            {
                string name = mineral.Groups["name"].Value;

                try
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    NumberFormatInfo numInfo = culture.NumberFormat;
                    price = Decimal.Parse(mineral.Groups["price"].Value, NumberStyles.Currency, numInfo);
                }
                catch (FormatException fe)
                {
                    ExceptionHandler.LogException(fe, true);
                    throw new MineralParserException(fe.Message + " (value was : " + mineral.Groups["price"].Value + ")");
                }
                yield return new Pair<string, Decimal>(name, price);
            }
        }
        #endregion
    }
}
