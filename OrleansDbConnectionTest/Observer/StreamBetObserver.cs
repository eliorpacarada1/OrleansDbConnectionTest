using Orleans.Streams;
using SiloHost.Models;

namespace SiloHost.Observer
{
    public class StreamBetObserver : IAsyncObserver<BetEvent>
    {
        public StreamBetObserver()
        {

        }
        public async Task OnCompletedAsync() => await Task.CompletedTask;
        public async Task OnErrorAsync(Exception ex)
        {
         throw new NotImplementedException();
        }

        public Task OnNextAsync(BetEvent item, StreamSequenceToken token = null)
        {
            throw new NotImplementedException();
        }
    }
}
