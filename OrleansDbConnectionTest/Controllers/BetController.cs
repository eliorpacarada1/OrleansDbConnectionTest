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
        public async Task<ActionResult> UpdateBetAmountAsync([FromBody] string amount, string key)
        {
           var result = _factory.GetGrain<IBetGrain>(key);
           await result.UpdateGrain(amount);
           return Ok("bravo!");
        }
        [HttpGet("{key}")]
        public async Task<ActionResult> GetAllEventsFromAGrain(string key)
        {
            var result = _factory.GetGrain<IBetGrain>(key);
            var resultIVerted = await result.ReadBetAsync();
            return Ok(resultIVerted);
        }
        //[HttpGet("/getBet/{key}")]
        //public async Task<ActionResult> GetBetFromStream(string key)
        //{
        //   var result = _factory.GetGrain<IBetGrain>(key);
        //   var graini = await result.GetSingleGrain(key);
        //   return Ok(graini);
        //}

    }
}
