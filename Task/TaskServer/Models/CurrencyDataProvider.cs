using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Task.Models;
using TaskServer.Models.Interface;

namespace TaskServer.Models
{
    public static class CurrencyDataProvider
    {
        public static List<Currency> GetCurrencyInRange(DateTime DateStart,DateTime DateEnd, List<AllInfo> CurInfo)
        {
            List<Currency> model = new List<Currency>();
            do
            {
                DateTime start = DateStart;
                DateTime end = (DateStart.Year == DateEnd.Year) ? DateEnd : DateStart.AddMonths(6);

                var ThisYearCurInfo = CurInfo.Where(s => s.Cur_DateStart <= start).OrderBy(s => s.Cur_DateStart).Last();
                if (ThisYearCurInfo.Cur_DateEnd < end)
                {
                    end = ThisYearCurInfo.Cur_DateEnd;
                    DateStart = ThisYearCurInfo.Cur_DateEnd.AddDays(1);
                }
                else
                {
                    DateStart = DateStart.AddMonths(6);
                }
                string url = $"https://www.nbrb.by/API/ExRates/Rates/Dynamics/{ThisYearCurInfo.Cur_ID}?startDate={start.Year}-{start.Month}-{start.Day}&endDate={end.Year}-{end.Month}-{end.Day}";
                string json = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
                model.AddRange(JsonConvert.DeserializeObject<List<Currency>>(json).ToList());
            } while (DateStart.Date < DateEnd.Date);
            model.ForEach(x => x.Cur_Abbreviation = CurInfo.First().Cur_Abbreviation);
            return model;
        }
    }
}
