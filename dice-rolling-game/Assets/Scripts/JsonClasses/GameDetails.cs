using System;
using System.Collections.Generic;

[Serializable]
public class GameDetails
{
    public int GameId;
    public int VisualLayoutId;
    public List<Dice> DiceList;
    public List<Wagers> WagersList;
    public List<BetTypes> BetTypes;
}

[Serializable]
public class Dice
{
    public int Count;
    public int Sides;
}

[Serializable]
public class Wagers
{
    public int Amount;
    public int WagerVisualId;
}

[Serializable]
public class BetTypes
{
    public bool UseBet;
    public int BetTypeGroupId;
    public string BetName;
    public List<BetDetails> BetDetails;
}

[Serializable]
public class BetDetails
{
    public bool UseBet;
    public int BetId;
    public string BetName;
    public int Multiplier;
}

