using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Task.Models;
using TaskServer.Models.Interface;

namespace TaskServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Index : ControllerBase
    {

        private readonly IMainModel _MainModel;

        public Index(IMainModel MainModel)
        {
            _MainModel = MainModel;
        }
        public string Get()
        {
            return "Server Started!";
        }
       [HttpGet("{Cur}/{DateStart}/{DateEnd}")]
        public List<Currency> Get(string Cur, DateTime DateStart, DateTime DateEnd)
        {
            try
            {
                var CurInfo = _MainModel.GetCurInfo(Cur);
                if (_MainModel.OnServerCurInfo!=null && _MainModel.OnServerCurInfo.Where(c => c.Date >= DateStart && c.Date <= DateEnd && CurInfo.Any(c1=>c1.Cur_ID== c.Cur_ID)).Count() == (DateEnd - DateStart).TotalDays+1)
                    return _MainModel.OnServerCurInfo.Where(c => c.Date >= DateStart.Date && c.Date <= DateEnd.Date && CurInfo.Any(c1 => c1.Cur_ID == c.Cur_ID)).ToList();
                else
                {
                    string url, json;
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
                        url = $"https://www.nbrb.by/API/ExRates/Rates/Dynamics/{ThisYearCurInfo.Cur_ID}?startDate={start.Year}-{start.Month}-{start.Day}&endDate={end.Year}-{end.Month}-{end.Day}";
                        json = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
                        model.AddRange(JsonConvert.DeserializeObject<List<Currency>>(json).ToList());
                    } while (DateStart.Date < DateEnd.Date);
                    _MainModel.OnServerCurInfo = _MainModel.OnServerCurInfo.Union(model).ToHashSet();
                    //_MainModel.OnServerCurInfo = _MainModel.OnServerCurInfo.Distinct().ToList();
                    string Savejson = JsonConvert.SerializeObject(_MainModel.OnServerCurInfo.OrderBy(c=>c.Date), Formatting.Indented);
                    System.IO.File.WriteAllText("Collection.json", Savejson);

                    return model;
                }
            }
            catch (Exception q)
            {

                return null;
            }
        }
    }
}
