using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetButton : MonoBehaviour {

    public int betId;
    private int betTypeGroupId;
    //private string betName;
    //private int multiplier;

    public Text betNameText;
    public GameObject betUiWithCoin;
    public GameObject betUiAmount;
    public Text betAmountText;

    private int betAmount;

    public void InitBetButton(int betTypeGroupId, string betName, int multiplier)
    {
        this.betTypeGroupId = betTypeGroupId;
        //this.betName = betName;
        //this.multiplier = multiplier;

        betAmount = 0;
        betNameText.text = betName;
        betUiWithCoin.SetActive(false);
    }

    #region OnClick
    public void OnClickBet()
    {
        // deduct money, check which coin show, update ui of coin and amount text
        GameManager.instance.PlacedABet(betTypeGroupId, betId);

        int betAmount = GameManager.instance.GetSelectedWagerAmount();
        this.betAmount += betAmount;
        betAmountText.text = this.betAmount.ToString();
        betUiAmount.SetActive(true);
        betUiWithCoin.SetActive(true);
    }

    public void OnClickCoin()
    {
        betUiAmount.SetActive(!isActiveAndEnabled);
    }

    public void OnClickRemoveBet()
    {
        GameManager.instance.BetRemoved(betTypeGroupId, betId, this.betAmount);

        betUiWithCoin.SetActive(false);
        this.betAmount = 0;
    }

    #endregion
}
