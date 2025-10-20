namespace Calculator_.src
{
    class UnlockOperator : DataPoint
    {
        public UnlockOperator(double opWeight, long timeCreate, long timeNow, string ev)
        {
            this.Freq = timeNow - timeCreate;
            this.Desc = ev;
            this.Val = opWeight;
        }
        private readonly double Freq;
        private readonly string Desc;
        private readonly double Val;
        public override string GetDesc()
        {
            return Desc;
        }

        public override double GetFreq()
        {
            return Freq;
        }

        public override double GetValue()
        {
            return Val;
        }
    }
    public class ScoreMilestone(double score, long timeCreate, long timeNow, string ev) : DataPoint
    {
        private readonly long Freq = timeNow - timeCreate;
        private readonly string Desc = ev;
        private readonly double Val = score;


        public override string GetDesc()
        {
            return Desc;
        }

        public override double GetFreq()
        {
            return Freq;
        }

        public override double GetValue()
        {
            return Val;
        }
    }
}
