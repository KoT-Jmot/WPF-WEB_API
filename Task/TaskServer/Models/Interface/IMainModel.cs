using System.Collections.Generic;
using Task.Models;

namespace TaskServer.Models.Interface
{
    public interface IMainModel
    {
        public List<AllInfo> Cur { get; set; }

        public HashSet<Currency> OnServerCurInfo { get; set; }
        public List<AllInfo> GetCurInfo(string Cur_Abbreviation);
    }
}
