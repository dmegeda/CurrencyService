using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Entities
{
    public class CurrencyRateInfo
    {
        public string BaseCurrency { get; }
        public string TargetCurrency { get; }
        public decimal Price { get; }

        public CurrencyRateInfo(string baseCur, string targetCur, decimal price)
        {
            BaseCurrency = baseCur;
            TargetCurrency = targetCur;
            Price = price;
        }
    }
}
