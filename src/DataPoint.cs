using static System.Formats.Asn1.AsnWriter;

namespace Calculator_.src
{
    public interface IDataPoint
    {
        double GetFreq();
        double GetValue();
        string GetDesc();
    }
    public abstract class DataPoint : IDataPoint
    {
        private long Freq;
        private string Desc;
        private double Val;
        public DataPoint()
        {
            Freq = 0;
            Desc = "";
            Val = 0;
        }
        public abstract string GetDesc();
        public abstract double GetFreq();
        public abstract double GetValue();
    }
}
