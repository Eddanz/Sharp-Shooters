using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sharp_Shooters
{ 
    internal static class Currency 
    {
        private static double _usdEurCur = 0.93;
        private static double _usdKronorCur = 10.86;
        private static double _eurUsdCur = 1.07;
        private static double _eurKronorDCur = 11.68;
        private static double _kronorUsdCur = 0.1;
        private static double _kronorEurCur = 0.091;

        public static double USDEURCUR //USD Dollar to Euro
        {
            get
            {
                return _usdEurCur;
            }
            set
            {
                _usdEurCur = value;
            }
        }
        public static double USDKRONORCUR //USD Dollar to Kronor
        {
            get
            {
               return _usdKronorCur;
            }
            set
            {

                _usdKronorCur = value;
            }
        }
        public static double EURUSDCUR //Euro to USD Dollar
        {
            get
            {
                return _eurUsdCur;
            }
            set
            {

                _eurUsdCur = value;
            }
        }
        public static double EURKRONORDCUR // Euro to Kronor
        {
            get
            {
                return _eurKronorDCur;
            }
            set
            {

                _eurKronorDCur = value;
            }
        }
        public static double KRONORUSDCUR //Kronor to USD Dollar
        {
            get
            {
                return _kronorUsdCur;
            }
            set
            {

                _kronorUsdCur = value;
            }
        }
        public static double KRONOREURCUR //Kronor to Euro
        {
            get
            {
                return _kronorEurCur;
            }
            set
            {
                
                _kronorEurCur = value;
            }
        }

        public static double GetExchangeRate(Accounts fromCurrency, Accounts toCurrency)
        {
            
            double returnCurrency = 1.0;
            switch (fromCurrency.Currencys)
            {
                
                case "USD":
                    switch (toCurrency.Currencys)
                    {
                        case "EURO":
                            returnCurrency = USDEURCUR;
                            return returnCurrency;
                        case "KRONOR":
                            returnCurrency = USDKRONORCUR;
                            return returnCurrency;
                    }
                    break;
                case "EURO":
                    switch (toCurrency.Currencys)
                    {
                        case "USD":
                            returnCurrency = EURUSDCUR;
                            return returnCurrency;
                        case "KRONOR":
                            returnCurrency = EURKRONORDCUR;
                            return returnCurrency;

                    }
                    break;
                case "KRONOR":
                    switch (toCurrency.Currencys)
                    {
                        case "USD":
                            returnCurrency = KRONORUSDCUR;
                            return returnCurrency;
                        case "EURO":
                            returnCurrency = KRONOREURCUR;
                            return returnCurrency;

                    }
                    break;
                default: 
                    return returnCurrency;
                    
            }
            return returnCurrency;

        }
        public static double ConvertCurrency(double amount, Accounts fromCurrency, Accounts toCurrency)
        {
            if (fromCurrency.Currencys == toCurrency.Currencys)
            {
                // Currencies are the same, no need to convert
                return amount;
            }
            else
            {
                // Currencies are different, perform conversion
                double exchangeRate = GetExchangeRate(fromCurrency, toCurrency);
                double convertedAmount = amount * exchangeRate;
                return convertedAmount;
            }
        }
    }
}
