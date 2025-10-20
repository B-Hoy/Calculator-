using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Formats.Asn1.AsnWriter;

namespace Calculator_.src
{
    public interface IDataPoint
    {
        double GetFreq();
        double GetValue();
        string GetDesc();
    }
    public class DataPoint : IDataPoint
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public long Freq { get; set; }
        public string Desc { get; set; }
        public double Val { get; set; }
        public DataPoint()
        {
            Freq = 0;
            Desc = "";
            Val = 0;
        }
        public string GetDesc()
        {
            return "";
        }
        public double GetFreq()
        {
            return 0;
        }
        public double GetValue()
        {
            return 0;
        }
    }
}
