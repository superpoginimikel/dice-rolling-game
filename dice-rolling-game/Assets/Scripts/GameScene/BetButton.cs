using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour {

    private int betId;
    private int betTypeGroupId;
    private int numberExistValue;

    public Text betNameText;
    public GameObject betUiWithCoin;
    public GameObject betUiAmount;
    public Text betAmountText;

    private int betAmount;

    public void InitBetButton(int betId, int betTypeGroupId, string betName, int multiplier, int numberExistValue)
    {
        this.betId = betId;
        this.betTypeGroupId = betTypeGroupId;
        this.numberExistValue = numberExistValue;

        betAmount = 0;
        betNameText.text = betName;
        betUiWithCoin.SetActive(false);
        gameObject.SetActive(true);
    }

    public void ResetBet()
    {
        betAmount = 0;
        betUiWithCoin.SetActive(false);
    }

    #region OnClick
    public void OnClickBet()
    {
        bool canMakeABet = GameManager.instance.CheckIfCanPlaceABet();
        if (!canMakeABet)
        {
            return;
        }

        GameManager.instance.PlacedABet(betTypeGroupId, betId, numberExistValue);

        int betAmount = GameManager.instance.GetSelectedWagerAmount();
        this.betAmount += betAmount;
        betAmountText.text = this.betAmount.ToString();
        betUiAmount.SetActive(true);
        betUiWithCoin.SetActive(true);
    }

    public void OnClickCoin()
    {
        betUiAmount.SetActive(!betUiAmount.activeSelf);
    }

    public void OnClickRemoveBet()
    {
        GameManager.instance.BetRemoved(betTypeGroupId, betId, this.betAmount);

        betUiWithCoin.SetActive(false);
        this.betAmount = 0;
    }

    #endregion
}
