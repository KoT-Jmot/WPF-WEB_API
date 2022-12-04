using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Task.Models;
using TaskServer.Models;
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
                //Проверка на присутствие нужных дат в файле
                if (_MainModel.OnServerCurInfo!=null && _MainModel.OnServerCurInfo.Where(c => c.Date >= DateStart && c.Date <= DateEnd && c.Cur_Abbreviation==Cur).Count() == (DateEnd - DateStart).TotalDays+1)
                    return _MainModel.OnServerCurInfo.Where(c => c.Date >= DateStart.Date && c.Date <= DateEnd.Date && c.Cur_Abbreviation==Cur).ToList();
                else
                {
                    List<Currency> model = new List<Currency>();
                    if (Cur == "BTC")
                    {
                        model = BitcoinDataProvider.GetBitcoinInRange(DateStart, DateEnd);
                    }
                    else
                    {
                        model = CurrencyDataProvider.GetCurrencyInRange(DateStart, DateEnd, _MainModel.GetCurInfo(Cur));
                    }
                    _MainModel.SaveServerCurrencyInfo(model);

                    return model;
                }
            }
            catch
            {

                return null;
            }
        }
    }
}
