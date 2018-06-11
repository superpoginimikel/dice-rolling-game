using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    private float diceRollingAnimationDelay = 1f;
    private bool isDiceRollingDelayDone = false;

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
        gameUiManager.WagerClickUpdateButtonInteractable(wagerId, totalCoins);
    }

    private GameDetails GetGameJson(int GameId)
    {
        GameMode gameMode = AccountManager.Instance.GetGameModeById(GameId);
        string jsonPath = Path.Combine(Application.persistentDataPath, gameMode.JsonPath);
        string gameDetailJson = GameUtil.GetJsonStringFromFile(jsonPath);

        GameDetails gameDetails = JsonUtility.FromJson<GameDetails>(gameDetailJson);
        //print("game details :: " + JsonUtility.ToJson(gameDetails));

        return gameDetails;
    }

    public bool CheckIfCanPlaceABet()
    {
        if(totalCoins == 0 || totalCoins < selectedWagerAmount)
        {
            return false;
        }

        return true;
    }

    public void PlacedABet(int betTypeGroupId, int betId, int numberExistValue)
    {
        AddOrUpdateGameBetList(betTypeGroupId, betId, selectedWagerAmount, numberExistValue);
        totalCoins = totalCoins - selectedWagerAmount;
        UpdateCoinAndWagers();
    }

    public void BetRemoved(int betTypeGroupId, int betId, int amount)
    {
        RemoveBetFromList(betTypeGroupId, betId);
        totalCoins = totalCoins + amount;
        UpdateCoinAndWagers();
        gameUiManager.UpdateWagers(totalCoins);
    }

    void UpdateCoinAndWagers()
    {
        gameUiManager.SetCoinsText(totalCoins);
        gameUiManager.UpdateWagers(totalCoins);
    }

    void AddOrUpdateGameBetList(int betTypeGroupId, int betId, int amount, int numberExistValue)
    {
        bool isAlreadyCreated = false;
        foreach (GameBet gameBet in gameBetsList)
        {
            if (gameBet.BetTypeGroupId == betTypeGroupId && gameBet.BetId == betId)
            {
                gameBet.Amount += amount;
                isAlreadyCreated = true;
                break;
            }
        }

        if (!isAlreadyCreated)
        {
            GameBet newGameBet = new GameBet(betTypeGroupId, betId, amount, numberExistValue);
            gameBetsList.Add(newGameBet);
        }
        gameUiManager.EnablePlayButton(true);
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

        if(gameBetsList.Count == 0)
        {
            gameUiManager.EnablePlayButton(false);
        }
    }

    public void EvaluateWinningBets(GameJsonFromServer gameJsonFromServer)
    {
        print("winning bet ::" + JsonUtility.ToJson(gameJsonFromServer));
        StartCoroutine(EvaluateWinningBetsIEnum(gameJsonFromServer));
    }

    IEnumerator EvaluateWinningBetsIEnum(GameJsonFromServer gameJsonFromServer)
    {
        while (!isDiceRollingDelayDone)
        {
            yield return null;
        }

        gameUiManager.BetRoundDone(gameJsonFromServer);
        SaveCoinsWon(gameJsonFromServer.totalAmountWon);
        UpdateMoneyAndResetBets();
        isDiceRollingDelayDone = false;
    }

    public void SaveCoinsWon(int amountWon)
    {
        totalCoins += amountWon;
        gameUiManager.SetCoinsText(totalCoins);
        AccountManager.Instance.Coins = totalCoins;
    }

    void ShowDiceRollingAnimation()
    {
        gameUiManager.ShowLoadingAnimationText();
        StartCoroutine(ShowDiceRollingAnimationIEnum());
    }

    IEnumerator ShowDiceRollingAnimationIEnum()
    {
        yield return new WaitForSeconds(diceRollingAnimationDelay);
        isDiceRollingDelayDone = true;
    }

    void UpdateMoneyAndResetBets()
    {
        AccountManager.Instance.Coins = totalCoins;
        var foundObjects = FindObjectsOfType<BetButton>();
        foreach(BetButton betButton in foundObjects)
        {
            betButton.ResetBet();
        }
        gameBetsList.Clear();
    }

    #region OnClick
    public void OnClickPlayButton()
    {
        // create json and send to server
        GameJsonToServer gameJsonToServer = new GameJsonToServer(AccountManager.Instance.GameId, gameBetsList);
        print("gameBetsList :::" + JsonUtility.ToJson(gameJsonToServer));
        server.EvaluateGame(gameJsonToServer);

        ShowDiceRollingAnimation();
    }

    public void OnClickHelpButton()
    {
        gameUiManager.ShowHideHelpPanel(true);
    }

    public void OnClickHelpCloseButton()
    {
        gameUiManager.ShowHideHelpPanel(false);
    }

    public void OnClickBackButton()
    {
        // Run loading scene script to add transition
        SceneManager.LoadScene("GameMenu");
    }

    public void OnClickBuyCoinsIAP()
    {
        // do IAP flow
        print("IAP");
    }
    #endregion


}