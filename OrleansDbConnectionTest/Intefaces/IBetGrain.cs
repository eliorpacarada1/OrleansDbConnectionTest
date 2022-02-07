using Orleans;
using SiloHost.Models;

namespace SiloHost.Intefaces
{
    public interface IBetGrain : IGrainWithStringKey
    {
        Task UpdateGrain(string amount);

        Task<string> ReadBetAsync();
    }
}
