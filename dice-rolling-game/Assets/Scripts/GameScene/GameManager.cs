using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameUiManager gameUiManager;
    private int wagerAmount;

    void Start()
    {
        GameDetails gameDetails = GetGameJson(AccountManager.Instance.GameId);
        gameUiManager.SetGameUi(gameDetails);
        // do stuff
    }

    public void SelectWager(int wagerAmount)
    {
        this.wagerAmount = wagerAmount;
    }

    private GameDetails GetGameJson(int GameId)
    {
        GameMode gameMode = AccountManager.Instance.GetGameModeById(GameId);
        string jsonPath = Path.Combine(Application.persistentDataPath, gameMode.JsonPath);
        string gameDetailJson = GameUtil.GetJsonStringFromFile(jsonPath);

        GameDetails gameDetails = JsonUtility.FromJson<GameDetails>(gameDetailJson);
        print("game details :: " + JsonUtility.ToJson(gameDetails));

        return gameDetails;
    }

    #region OnClick
    public void OnClickPlayButton()
    {
        print("play button");
        // create json and send to server
    }
    #endregion
}