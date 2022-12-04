using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Task.Models;

namespace TaskServer.Models
{
    public static class BitcoinDataProvider
    {
        public static List<Currency> GetBitcoinInRange(DateTime DateStart, DateTime DateEnd)
        {
            double start = new DateTimeOffset(DateStart).ToUnixTimeMilliseconds();
            double end = new DateTimeOffset(DateEnd.AddDays(1)).ToUnixTimeMilliseconds();
            string url = $"https://api.coincap.io/v2/assets/bitcoin/history?interval=d1&start={start}&end={end}";
            string json = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
            CollectionOfBitcoin bitcoins = JsonConvert.DeserializeObject<CollectionOfBitcoin>(json);
            return bitcoins.GetCur();
        }
    }
}
