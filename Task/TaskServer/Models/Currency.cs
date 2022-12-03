using System;

namespace Task.Models
{
    public class Currency : IEquatable<Currency>
    {
        public int Cur_ID {get;set;}
        public string Cur_Abbreviation { get;set;}
        public DateTime Date {get;set;}
        public double Cur_OfficialRate { get; set; }
        public bool Equals(Currency Cur)
        {
            return (Cur.Cur_ID == this.Cur_ID && Cur.Date == this.Date && Cur.Cur_OfficialRate == this.Cur_OfficialRate);
        }
        public override bool Equals(object obj) => Equals(obj as Currency);
        public override int GetHashCode() => (Cur_ID, Date, Cur_OfficialRate).GetHashCode();
    }

}
