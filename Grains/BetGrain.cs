using Orleans;
using Orleans.Runtime;

public class BetGrain : Grain, IBetGrain
{
    private readonly IPersistentState<Bet> _bet;
    private readonly IGrainFactory _factory;


    public BetGrain([PersistentState(nameof(Bet), "orleansStorage")] IPersistentState<Bet> bet, IGrainFactory factory)
    {
        _bet = bet;
        _factory = factory;
    }

    public async Task<bool> CreateBet(Bet bet)
    {
        try
        {
            await SaveBet(bet);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return false;
    }
    public async Task SaveBet(Bet bet)
    {
        _bet.State.Amount = bet.Amount;
        await _bet.WriteStateAsync();
    }
}