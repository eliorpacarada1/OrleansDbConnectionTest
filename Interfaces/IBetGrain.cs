using Orleans;

public interface IBetGrain : IGrainWithStringKey
{
    Task<bool> CreateBet(Bet bet);


}