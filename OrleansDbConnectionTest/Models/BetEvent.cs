namespace SiloHost.Models
{
    [Serializable]
    public class BetEvent
    {
        public string GrainKey { get; set; }

        public DateTime UpdatedTime { get; } = DateTime.UtcNow;

       
    }
}
