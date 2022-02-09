using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Streams;
using SiloHost.Intefaces;
using SiloHost.Models;
using SiloHost.Observer;

namespace SiloHost.Grains
{
    [LogConsistencyProvider(ProviderName = "testLogStorage")]
    [StorageProvider(ProviderName = "orleansStorage")]
    public class BetGrain : JournaledGrain<BetState, BetEvent>, IBetGrain
    {
        private readonly IPersistentState<BetState> _state;
        private IAsyncObservable<BetEvent> consumer;
        internal int numConsumedItems;
        internal ILogger logger;
        private StreamSubscriptionHandle<BetEvent> consumerHandle;

        public BetGrain([PersistentState(nameof(BetGrain), "orleansStorage")] IPersistentState<BetState> state)
        {
            _state = state;
        }

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public async Task BecomeConsumer(Guid streamId)
        {
            IStreamProvider streamProvider = this.GetStreamProvider("betProvider");
            consumer = streamProvider.GetStream<BetEvent>(streamId, "default");
            consumerHandle = await consumer.SubscribeAsync(new StreamBetObserver());
        }

        

        public async Task<IReadOnlyList<BetEvent>> ReadBetAsync()
        {
            var allEvents = await RetrieveConfirmedEvents(0, Version);
            return await Task.FromResult(allEvents);
        }

        public async Task UpdateGrain(string amount)
        {
            _state.State.Amount = amount;
            RaiseEvent(new BetEvent() { GrainKey = this.GetPrimaryKeyString(), Amount = amount, GrainValue = "" });
            await ConfirmEvents();
          
        }
        //public async Task<BetEvent> GetSingleGrain(string GrainKey)
        //{
        //    BetEvent eventi = new BetEvent();
        //    if(eventi.GrainKey == GrainKey)
        //    {
        //        return eventi;
        //    }
        //    return null;
        }
        
    }
}
