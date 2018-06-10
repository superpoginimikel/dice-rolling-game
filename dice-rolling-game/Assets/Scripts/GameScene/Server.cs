using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server : MonoBehaviour {

    // bet type group id
    // 1 - big small, 2- pairs, 3- odd even, 4 - any quadruple, 5 number exist
    
    // bet type
    // 1 small, 2 big, 3 pairs, 4 odd, 5 even, 6 any quadruople, 7-26 = any number from 1-20

    GameDetails gameDetails;
    private List<Dice> gameDiceList;
    private GameJsonFromServer gameJsonFromServer;

    private int minRollResult = 1;
    
    public void SetGameDetails(GameDetails gameDetails)
    {
        this.gameDetails = gameDetails;
        gameDiceList = gameDetails.DiceList;
    }

	public void EvaluateGame(GameJsonToServer gameJson)
    {
        List<int> diceResult = RollDice();
        gameJsonFromServer = new GameJsonFromServer();
        gameJsonFromServer.diceRollList = diceResult;
        gameJsonFromServer.totalAmountWon = 0;
        gameJsonFromServer.winningBetDetails = new List<WinningBetDetails>();

        foreach (GameBet gameBet in gameJson.GameBetsList)
        {
            switch(gameBet.BetTypeGroupId)
            {
                case 1:
                    CheckForBigSmall(diceResult, gameBet);
                    break;
                case 2:
                    CheckForPairs(diceResult, gameBet);
                    break;
                case 3:
                    CheckForOddEven(diceResult, gameBet);
                    break;
                case 4:
                    CheckForQuadruple(diceResult, gameBet);
                    break;
                case 5:
                    CheckForAnyNumberThatExist(diceResult, gameBet);
                    break;
            }
        }

        print("win :::" + JsonUtility.ToJson(gameJsonFromServer));
        // return result to game
    }

    void AddNewWinningBetDetails(GameBet betMade, int amountWon)
    {
        gameJsonFromServer.totalAmountWon += amountWon;
        WinningBetDetails winningBetDetails = new WinningBetDetails(betMade.BetTypeGroupId, betMade.BetId, amountWon);
        gameJsonFromServer.winningBetDetails.Add(winningBetDetails);
    }

    void AddAmountWonAndSaveToList(GameBet betMade)
    {
        BetDetails betDetail = GetBetDetail(betMade.BetTypeGroupId, betMade.BetId);
        int multiplier = betDetail.Multiplier;
        int amountWon = (betMade.Amount * multiplier) + betMade.Amount;
        AddNewWinningBetDetails(betMade, amountWon);
    }

    List<int> RollDice()
    {
        List<int> diceRollResultList = new List<int>();
        foreach (Dice die in gameDiceList)
        {
            for(int ctr = die.Count; ctr > 0; ctr--)
            {
                int randomRollResult = Random.Range(minRollResult, die.Sides + 1);
                diceRollResultList.Add(randomRollResult);
            }
        }

        return diceRollResultList;
    }

    void CheckForBigSmall(List<int> diceRollResultList, GameBet betMade)
    {
        int totalAmount = 0;
        foreach(int dice in diceRollResultList)
        {
            totalAmount += dice;
        }
        print("total amount for big small ::" + totalAmount);

        BetDetails betDetail = GetBetDetail(betMade.BetTypeGroupId, betMade.BetId);
        if (totalAmount >= betDetail.MinRange && totalAmount <= betDetail.MaxRange)
        {
            print("bet made that won::" + betMade.BetId);
            int multiplier = betDetail.Multiplier;
            int amountWon = (betMade.Amount * multiplier) + betMade.Amount;

            AddNewWinningBetDetails(betMade, amountWon);
        }
    }

    void CheckForPairs(List<int> diceRollResultList, GameBet betMade)
    {
        int distinctCount = 0;
        bool foundAPair = false;
        foreach(int die in diceRollResultList)
        {
            distinctCount = 0;
            foreach (int dieToCheck in diceRollResultList)
            {
                if(die == dieToCheck)
                {
                    distinctCount++;
                }
            }
            if(distinctCount >= 2)
            {
                foundAPair = true;
                break;
            }
        }

        if (foundAPair)
        {
            BetDetails betDetail = GetBetDetail(betMade.BetTypeGroupId, betMade.BetId);
            int multiplier = betDetail.Multiplier;
            int amountWon = (betMade.Amount * multiplier) + betMade.Amount;
            AddNewWinningBetDetails(betMade, amountWon);
        }
    }

    void CheckForOddEven(List<int> diceRollResultList, GameBet betMade)
    {
        int totalAmount = 0;
        foreach (int dice in diceRollResultList)
        {
            totalAmount += dice;
        }

        bool wasBetCorrect = false;
        if (totalAmount % 2 == 0 && betMade.BetId == 5)
        {
            wasBetCorrect = true;
        } else if (totalAmount % 2 != 0 && betMade.BetId == 4)
        {
            wasBetCorrect = true;
        }

        if(wasBetCorrect)
        {
            BetDetails betDetail = GetBetDetail(betMade.BetTypeGroupId, betMade.BetId);
            int multiplier = betDetail.Multiplier;
            int amountWon = (betMade.Amount * multiplier) + betMade.Amount;
            AddNewWinningBetDetails(betMade, amountWon);
        }
    }

    void CheckForQuadruple(List<int> diceRollResultList, GameBet betMade)
    {
        int gameModeDiceMaxValue = 0;
        foreach (Dice die in gameDiceList)
        {
            if(die.Sides >= gameModeDiceMaxValue)
            {
                gameModeDiceMaxValue = die.Sides;
            }
        }

        bool hasQuadruple = false;
        if (diceRollResultList.Count >= 4)
        {
            int distinctCount = 0;
            foreach(int currentDie in diceRollResultList)
            {
                distinctCount = 0;
                foreach (int currentDieToCheck in diceRollResultList)
                {
                    if(currentDie == currentDieToCheck)
                    {
                        distinctCount++;
                    }
                }
                if(distinctCount >= 4)
                {
                    hasQuadruple = true;
                    break;
                }
            }

            if (hasQuadruple)
            {
                AddAmountWonAndSaveToList(betMade);
            }
        }
    }

    void CheckForAnyNumberThatExist(List<int> diceRollResultList, GameBet betMade)
    {
        foreach(int die in diceRollResultList)
        {
            if(betMade.NumberExistValue == die)
            {
                AddAmountWonAndSaveToList(betMade);
                break;
            }
        }
    }

    // not used
    int GetMultiplierValue(int betTypeGroupId, int betId)
    {
        int multiplier = 1;

        foreach(BetTypes betType in gameDetails.BetTypes)
        {
            if(betType.BetTypeGroupId == betTypeGroupId)
            {
                foreach(BetDetails betDetail in betType.BetDetails)
                {
                    if(betDetail.BetId == betId)
                    {
                        return betDetail.Multiplier;
                    }
                }
            }
        }

        return multiplier;
    }

    BetDetails GetBetDetail(int betTypeGroupId, int betId)
    {
        foreach (BetTypes betType in gameDetails.BetTypes)
        {
            if (betType.BetTypeGroupId == betTypeGroupId)
            {
                foreach (BetDetails betDetail in betType.BetDetails)
                {
                    if (betDetail.BetId == betId)
                    {
                        return betDetail;
                    }
                }
            }
        }

        return null;
    }
}
