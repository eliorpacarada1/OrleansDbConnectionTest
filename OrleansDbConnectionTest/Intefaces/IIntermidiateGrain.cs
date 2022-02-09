using Orleans;
using SiloHost.Models;

namespace SiloHost.Intefaces
{
    public interface IIntermidiateGrain : IGrainWithGuidKey
    {
        Task<BetEvent> GetBet(BetEvent bet);
        Task<BetEvent> SetBetAmount();
    }
}
