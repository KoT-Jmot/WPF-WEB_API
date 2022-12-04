using System.Collections.Generic;
using Task.Models;

namespace TaskServer.Models
{
    public class CollectionOfBitcoin
    {
        public List<Bitcoin> data;
        public List<Currency> GetCur()
        {
            List<Currency> currencies = new List<Currency>();
            data.ForEach(a => currencies.Add(new Currency { Cur_Abbreviation = "BTC", Date = a.Date, Cur_OfficialRate = a.priceUsd }));
            return currencies;
        }

    }
}
