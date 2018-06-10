using System;
using System.Collections.Generic;

[Serializable]
public class GameJsonToServer {

    public int GameId;
    public List<GameBet> GameBetsList;

    public GameJsonToServer(int gameId, List<GameBet> gameBetsList)
    {
        this.GameId = gameId;
        this.GameBetsList = gameBetsList;
    }
}
