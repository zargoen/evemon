using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using EVEMon.Common;

namespace EVEMon.Sales
{
    [DefaultMineralParser("cxi")]
    public class CXIParser : IMineralParser
    {

        private static Regex mineralTokenizer =
            new Regex(@"\|(?<name>.+?)\=(?<price>.+?)\|",
                      RegexOptions.Compiled);

        #region IMineralParser Members
        public string Title
        {
            get { return "Complexion Industries"; }
        }

        public string CourtesyUrl
        {
            get { return "http://www.fallenreality.net/cxi/"; }
        }

        public string CourtesyText
        {
            get { return "Complexion Industries"; }
        }

        public IEnumerable<Pair<string, decimal>> GetPrices()
        {
            string content = null;
            try
            {
                content = EVEMonWebRequest.GetUrlString("http://cxi-minerals.fallenreality.net/cxi-eve-mon-prices.txt");
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