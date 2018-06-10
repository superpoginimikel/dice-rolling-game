using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WagerCoin : MonoBehaviour {

    private int wagerId;
    private int amount;
    private Image wagerCoinImage;

    public Button wagerButton;
    public Text wagerValueText;

    public int GetWagerId()
    {
        return wagerId;
    }

    public void Init(int wagerId, int amount, Sprite wagerCoinSprite)
    {
        this.wagerId = wagerId;
        this.amount = amount;

        wagerCoinImage = GetComponent<Image>();
        wagerCoinImage.sprite = wagerCoinSprite;
        wagerValueText.text = amount.ToString();
    }

    public void SaveSelectedWager()
    {
        GameManager.instance.SelectWager(wagerId, amount);
    }

    public void UpdateCoinSelectedUi(int totalAmount)
    {
        if(amount > totalAmount)
        {
            wagerButton.interactable = false;
        }
    }

    public void UpdateCoinSelectedUi(bool isSelected)
    {
        wagerButton.interactable = !isSelected;
    }

    #region OnClick
    public void OnClickWagerCoin()
    {
        SaveSelectedWager();
    }
    #endregion
}
