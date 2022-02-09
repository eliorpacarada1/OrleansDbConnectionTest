using Orleans;
using Orleans.Streams;
using SiloHost.Intefaces;
using SiloHost.Models;

namespace SiloHost.Grains
{
    public class IntermidiateGrain : Grain, IIntermidiateGrain
    {
        private IAsyncStream<BetEvent> stream;


        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            stream = streamProvider.GetStream<BetEvent>(this.GetPrimaryKey(), "default");
            return base.OnActivateAsync();
        }

        public async Task<BetEvent> GetBet(BetEvent bet)
        {
            await stream.OnNextAsync(bet);
            return await Task.FromResult(bet);
           
        }

        public Task<BetEvent> SetBetAmount()
        {
            throw new NotImplementedException();
        }

      
    }
}
