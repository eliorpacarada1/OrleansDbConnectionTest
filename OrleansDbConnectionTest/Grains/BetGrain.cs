using Orleans;
using Orleans.Runtime;
using SiloHost.Intefaces;
using SiloHost.Models;

namespace SiloHost.Grains
{
    public class BetGrain : Grain, IBetGrain
    {
        private readonly IPersistentState<BetState> _state;

        public BetGrain([PersistentState(nameof(BetGrain), "orleansStorage")] IPersistentState<BetState> state)
        {
            _state = state;
        }

        public Task<decimal> ReadBetAsync()
        {
            return Task.FromResult(_state.State.Amount);
        }

        public async Task<bool> UpdateGrain(decimal amount)
        {
            _state.State.Amount = amount;
            await _state.WriteStateAsync();
            return true;
        }
        
    }
}
