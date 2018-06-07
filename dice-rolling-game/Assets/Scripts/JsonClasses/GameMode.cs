using System;
using System.Collections.Generic;

[Serializable]
public class GameModesList
{
    public List<GameMode> GameList;
}

[Serializable]
public class GameMode {
    public int GameId;
    public string Name;
    public string ImagePath;
    public string JsonPath;
}