namespace Calculator_.src
{
    class UnlockOperator : DataPoint
    {
        public UnlockOperator(int id, int pid, double opWeight, long timeCreate, long timeNow, string ev)
        {
            this.Id = id;
            this.ParentId = pid;
            this.Freq = timeNow - timeCreate;
            this.Desc = ev;
            this.Val = opWeight;
        }
        public UnlockOperator()
        {
            
        }
        public new string GetDesc()
        {
            return Desc;
        }

        public new double GetFreq()
        {
            return Freq;
        }

        public new double GetValue()
        {
            return Val;
        }
    }
    public class ScoreMilestone : DataPoint
    {
        public ScoreMilestone()
        {

        }
        public ScoreMilestone(int id, int pid, double score, long timeCreate, long timeNow, string ev)
        {
            this.Id = id;
            this.ParentId = pid;
            this.Val = score;
            this.Freq = timeNow - timeCreate;
            this.Desc = ev;
        }

        public new string GetDesc()
        {
            return Desc;
        }

        public new double GetFreq()
        {
            return Freq;
        }

        public new double GetValue()
        {
            return Val;
        }
    }
}
