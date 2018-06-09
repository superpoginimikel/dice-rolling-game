using System;
using System.Collections.Generic;

[Serializable]
public class GameDetails
{
    public int GameId;
    public int VisualLayout;
    public List<Dice> Dice;
    public List<Wagers> Wagers;
    public List<BetTypes> BetTypes;
}

public class Dice
{
    public int Count;
    public int Sides;
}

public class Wagers
{
    public int Amount;
    public int WagerVisualId;
}

public class BetTypes
{
    public int BetTypeId;
}

