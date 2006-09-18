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
            string content;
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

            foreach (Match mineral in mc)
            {
                string name = mineral.Groups["name"].Value;

                Decimal price = Decimal.Parse(mineral.Groups["price"].Value, NumberStyles.Currency, CultureInfo.InvariantCulture);
                yield return new Pair<string, Decimal>(name, price);
            }
        }
        #endregion
    }
}
