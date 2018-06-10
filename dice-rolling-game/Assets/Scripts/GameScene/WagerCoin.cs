using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WagerCoin : MonoBehaviour {

    private int wagerId;
    private int amount;
    private Image wagerCoinImage;

    public Text wagerValueText;


    public void Init(int wagerId, int amount, Sprite wagerCoinSprite)
    {
        this.wagerId = wagerId;
        this.amount = amount;

        wagerCoinImage = GetComponent<Image>();
        wagerCoinImage.sprite = wagerCoinSprite;
        wagerValueText.text = amount.ToString();
    }

    private void SaveSelectedWager()
    {
        GameManager.instance.SelectWager(amount);
    }

    #region OnClick
    public void OnClickWagerCoin()
    {
        SaveSelectedWager();
    }
    #endregion
}
