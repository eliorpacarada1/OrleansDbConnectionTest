using Orleans;
using SiloHost.Models;

namespace SiloHost.Intefaces
{
    public interface IBetGrain : IGrainWithStringKey
    {
        Task<bool> UpdateGrain(decimal amount);

        Task<decimal> ReadBetAsync();
    }
}
