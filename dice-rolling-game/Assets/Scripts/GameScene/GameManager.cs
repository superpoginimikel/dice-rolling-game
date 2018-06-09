using System;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum BetTypes
    {
        BigSmall,
        OddEven,
        Pairs,
        AnyNumber,
        AnyQuadrouple,
        NumberExist
    }

    void Start()
    {
        GameDetails gameDetails = GetGameJson(AccountManager.Instance.GameId);

        // do stuff
    }

    private GameDetails GetGameJson(int GameId)
    {
        GameMode gameMode = AccountManager.Instance.GetGameModeById(GameId);
        string jsonPath = Path.Combine(Application.persistentDataPath, gameMode.JsonPath);
        string gameDetailJson = GameUtil.GetJsonStringFromFile(jsonPath);

        GameDetails gameDetails = JsonUtility.FromJson<GameDetails>(gameDetailJson);
        print("game details :: " + gameDetails.GameId);

        return gameDetails;
    }
}