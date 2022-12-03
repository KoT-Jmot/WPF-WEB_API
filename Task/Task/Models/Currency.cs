using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Models
{
    internal class Currency
    {
        public int Cur_ID {get;set;}
        public string Cur_Abbreviation { get; set; }
        public DateTime Date {get;set;}
        public double Cur_OfficialRate { get; set; }

    }
}
