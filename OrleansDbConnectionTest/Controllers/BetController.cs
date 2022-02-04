using Microsoft.AspNetCore.Mvc;
using Orleans;
using SiloHost.Intefaces;

namespace SiloHost.Controllers
{
    [ApiController]
    [Route("/api/bet")]
    public class BetController : ControllerBase
    {
        private readonly IGrainFactory _factory;

        public BetController(IGrainFactory factory)
        {
            _factory = factory;
        }
        [HttpPost("{key}")]
        public async Task<ActionResult> UpdateBetAmountAsync([FromBody] decimal amount, string key)
        {
           var result = _factory.GetGrain<IBetGrain>(key);
           var resultIVerted = await result.UpdateGrain(amount);
           return Ok(resultIVerted);
        }
        [HttpGet("{key}")]
        public async Task<ActionResult> ReadBetLatestState(string key)
        {
            var result = _factory.GetGrain<IBetGrain>(key);
            var resultIVerted = await result.ReadBetAsync();
            return Ok(resultIVerted);
        }
    }
}
