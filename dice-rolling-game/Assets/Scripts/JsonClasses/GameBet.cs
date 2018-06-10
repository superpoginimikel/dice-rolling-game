using System;
using System.Collections.Generic;

[Serializable]
public class GameBet {

    public int BetTypeGroupId;
    public int BetId;
    public int Amount;
    public int NumberExistValue;

    public GameBet(int betTypeGroupId, int betId, int amount, int numberExistValue = 0)
    {
        this.BetTypeGroupId = betTypeGroupId;
        this.BetId = betId;
        this.Amount = amount;
        this.NumberExistValue = numberExistValue;
    }
}
