using UnityEngine;
using System.IO;

public class AccountManager : Singleton<AccountManager> {

    private GameModesList gameModesList;

	public int Coins
    {
        get { return PlayerPrefs.GetInt(GameDataConstant.CoinsKey, 1000); }
        set { PlayerPrefs.SetInt(GameDataConstant.CoinsKey, value); }
    }

    public int GameId
    {
        get { return PlayerPrefs.GetInt(GameDataConstant.GameIdKey, -1); }
        set { PlayerPrefs.SetInt(GameDataConstant.GameIdKey, value); }
    }

    public GameModesList GetGameModesList()
    {
        string gameModesPath = Path.Combine(Application.persistentDataPath, GameDataConstant.GameConfigJsonFile);
        string gameModesJsonString = GameUtil.GetJsonStringFromFile(gameModesPath);
        if (string.IsNullOrEmpty(gameModesJsonString))
        {
            Debug.LogError("Game Menu Json Error");

            // Show error panel, maybe return to main menu to unzip? 
            return null;
        }
        else
        {
            // create game menu
            gameModesList = JsonUtility.FromJson<GameModesList>(gameModesJsonString);
            //print("game list ::" + JsonUtility.ToJson(gameModesList));
            return gameModesList;
        }
    }

    public GameMode GetGameModeById(int gameModeId)
    {
        foreach(GameMode gameMode in gameModesList.GameList)
        {
            if(gameModeId == gameMode.GameId)
            {
                return gameMode;
            }
        }
        return null;
    }
}
