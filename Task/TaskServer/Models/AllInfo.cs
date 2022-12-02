using System;

namespace Task.Models
{
    public class AllInfo
    {
        public int Cur_ID { get; set; }

        public int Cur_ParentID { get; set; }
        public string Cur_Abbreviation { get; set; }
        public DateTime Cur_DateStart { get; set; }
        public DateTime Cur_DateEnd { get; set; }
    }
}
