using Orleans;
using SiloHost.Models;

namespace SiloHost.Intefaces
{
    public interface IBetGrain : IGrainWithStringKey
    {
        Task UpdateGrain(string amount);

        Task<IReadOnlyList<BetEvent>> ReadBetAsync();

        Task<BetEvent> GetSingleGrain(string GrainKey);
    }
}
