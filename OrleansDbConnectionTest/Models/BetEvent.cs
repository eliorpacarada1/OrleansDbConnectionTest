namespace SiloHost.Models
{
    [Serializable]
    public class BetEvent
    {
        public BetEvent()
        {
        }

        public BetEvent(string GrainKey, string GrainValue, string Amount, DateTime updateDTime)
        {
            this.GrainKey = GrainKey;
            this.GrainValue = GrainValue;
            this.Amount = Amount;
            this.UpdatedTime = updateDTime;
        }
        public string GrainKey { get; set; }

        public string GrainValue { get; set;}

        public string Amount { get; set; }
        public DateTime UpdatedTime { get; } = DateTime.UtcNow;

       
    }
}
