using System;
using System.Collections.Generic;

[Serializable]
public class GameJsonFromServer {

    public List<int> diceRollList;
    public int totalAmountWon;
    public List<WinningBetDetails> winningBetDetails;
}

[Serializable]
public class WinningBetDetails
{
    public int BetTypeGroupId;
    public int BetTypeId;
    public int AmountWon;

    public WinningBetDetails(int betTypeGroupId, int betTypeId, int amountWon)
    {
        this.BetTypeGroupId = betTypeGroupId;
        this.BetTypeId = betTypeId;
        this.AmountWon = amountWon;
    }
}
