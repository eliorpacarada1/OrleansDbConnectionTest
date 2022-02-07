using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Runtime;
using SiloHost.Intefaces;
using SiloHost.Models;

namespace SiloHost.Grains
{
    [LogConsistencyProvider(ProviderName = "testLogStorage")]
    [StorageProvider(ProviderName = "orleansStorage")]
    public class BetGrain : JournaledGrain<BetState, BetEvent>, IBetGrain
    {
        private readonly IPersistentState<BetState> _state;

        public BetGrain([PersistentState(nameof(BetGrain), "orleansStorage")] IPersistentState<BetState> state)
        {
            _state = state;
        }

        public async Task<string> ReadBetAsync()
        {
            var allEvents = await RetrieveConfirmedEvents(0, Version);
            return await Task.FromResult(_state.State.Amount);
        }

        public async Task UpdateGrain(string amount)
        {
            _state.State.Amount = amount;
            RaiseEvent(new BetEvent() { GrainKey = this.GetPrimaryKeyString() });
            await ConfirmEvents();
          
        }
        
    }
}
