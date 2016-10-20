using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace HomeBudgetViewer.Database.Engine.Restrictions.Currency
{
    public class CurrencyModel
    {
        public Currency CurrencyEnum { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        private static List<CurrencyModel> _possibleCurrencies;
        public static List<CurrencyModel> PossibleCurrencies
        {
            get
            {
                return _possibleCurrencies ?? (_possibleCurrencies = new List<CurrencyModel>()
                {
                   new CurrencyModel() {CurrencyEnum = Currency.Dolar, CurrencyName = "Dolar", CurrencySymbol = " $  "},
                   new CurrencyModel() {CurrencyEnum = Currency.Euro, CurrencyName = "Euro", CurrencySymbol = " \u20ac  "},
                   new CurrencyModel() {CurrencyEnum = Currency.Pesos, CurrencyName = "Pesos", CurrencySymbol = " $  "},
                   new CurrencyModel() {CurrencyEnum = Currency.Pound, CurrencyName = "Pound", CurrencySymbol = " \u00a3  "},
                   new CurrencyModel() {CurrencyEnum = Currency.Yen, CurrencyName = "Yen", CurrencySymbol = " \u00A5  "},
                   new CurrencyModel() {CurrencyEnum = Currency.Zloty, CurrencyName = "Zloty", CurrencySymbol = " ZŁ "},
                });
            }
        }
    }
}
