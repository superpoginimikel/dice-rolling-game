using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameUiManager gameUiManager;
    public Server server;
    private int selectedWagerAmount;
    private int totalCoins;

    private List<GameBet> gameBetsList;

    void Start()
    {
        totalCoins = AccountManager.Instance.Coins;
        gameBetsList = new List<GameBet>();

        GameDetails gameDetails = GetGameJson(AccountManager.Instance.GameId);
        server.SetGameDetails(gameDetails);
        gameUiManager.SetGameUi(gameDetails, totalCoins);
    }

    public int GetSelectedWagerAmount()
    {
        return selectedWagerAmount;
    }

    public void SelectWager(int wagerId, int wagerAmount)
    {
        this.selectedWagerAmount = wagerAmount;
        gameUiManager.WagerClickUpdateButtonInteractable(wagerId);
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

    public void PlacedABet(int betTypeGroupId, int betId)
    {
        AddOrUpdateGameBetList(betTypeGroupId, betId, selectedWagerAmount);
        totalCoins = totalCoins - selectedWagerAmount;
        UpdateCoinAndWagers();
    }

    public void BetRemoved(int betTypeGroupId, int betId, int amount)
    {
        RemoveBetFromList(betTypeGroupId, betId);
        totalCoins = totalCoins + amount;
        UpdateCoinAndWagers();
    }

    void UpdateCoinAndWagers()
    {
        gameUiManager.SetCoinsText(totalCoins);
        gameUiManager.UpdateWagers(this.totalCoins);
    }

    void AddOrUpdateGameBetList(int betTypeGroupId, int betId, int amount)
    {
        print("bet id ::" + betId);
        bool isAlreadyCreated = false;
        foreach (GameBet gameBet in gameBetsList)
        {
            print("bet id ::" + gameBet.BetId + "            amount ::" + gameBet.Amount);
            if (gameBet.BetTypeGroupId == betTypeGroupId && gameBet.BetId == betId)
            {
                print("update a bet");
                gameBet.Amount += amount;
                isAlreadyCreated = true;
                break;
            }
            print("bet id ::" + gameBet.BetId + "            amount ::" + gameBet.Amount);
        }

        if (!isAlreadyCreated)
        {
            print("create a new bet");
            GameBet newGameBet = new GameBet(betTypeGroupId, betId, amount);
            gameBetsList.Add(newGameBet);
        }
    }

    void RemoveBetFromList(int betTypeGroupId, int betId)
    {
        foreach(GameBet gameBet in gameBetsList)
        {
            if (gameBet.BetTypeGroupId == betTypeGroupId && gameBet.BetId == betId)
            {
                gameBetsList.Remove(gameBet);
                return;
            }
        }
    }

    #region OnClick
    public void OnClickPlayButton()
    {
        print("play button");
        // create json and send to server
        GameJsonToServer gameJsonToServer = new GameJsonToServer(AccountManager.Instance.GameId, gameBetsList);
        print("gameBetsList :::" + JsonUtility.ToJson(gameJsonToServer));
        server.EvaluateGame(gameJsonToServer);

        // open rolling animation
    }

    public void OnClickHelpButton()
    {
        print("help button");
        // open some panel explaining the game
    }
    #endregion


}