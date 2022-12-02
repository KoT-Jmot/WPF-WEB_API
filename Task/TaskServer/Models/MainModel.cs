using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Task.Models;
using TaskServer.Models.Interface;

namespace TaskServer.Models
{
    public class MainModel : IMainModel
    {
        public List<AllInfo> Cur { get; set; }
        public HashSet<Currency> OnServerCurInfo{ get; set; }
        public MainModel()
        {
            //Создаётся экземпляр класса с общей инфой о валюте
            if (System.IO.File.Exists("AllInfo.json"))
            {
                Cur = JsonConvert.DeserializeObject<List<AllInfo>>(System.IO.File.ReadAllText("AllInfo.json"));
            }
            else
            {
                string url = "https://www.nbrb.by/api/exrates/currencies";
                string json = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
                System.IO.File.WriteAllText("AllInfo.json", json);
                Cur = JsonConvert.DeserializeObject<List<AllInfo>>(json);
            }
            OnServerCurInfo = new HashSet<Currency>();
            if(System.IO.File.Exists("Collection.json"))
            {
                OnServerCurInfo = JsonConvert.DeserializeObject<List<Currency>>(System.IO.File.ReadAllText("Collection.json")).ToHashSet();
            }

        }


        //Ф-ция осуществляет выборку для определённой валюты
        public List<AllInfo> GetCurInfo(string Cur_Abbreviation)
        {
            return Cur.Where(c=>c.Cur_Abbreviation== Cur_Abbreviation).ToList();
        }
    }
}
