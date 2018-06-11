using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WagerCoin : MonoBehaviour {

    private bool isSelected = false;
    private int wagerId;
    private int amount;
    private Image wagerCoinImage;

    public Button wagerButton;
    public Text wagerValueText;

    public int GetWagerId()
    {
        return wagerId;
    }

    public int GetWagerAmount()
    {
        return amount;
    }

    public void SetIsSelected(bool isSelected)
    {
        this.isSelected = isSelected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
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
        wagerButton.interactable = (amount > totalAmount) ? false : true;
    }

    public void UpdateCoinSelectedUi(bool isInteractable)
    {
        wagerButton.interactable = isInteractable;
    }

    #region OnClick
    public void OnClickWagerCoin()
    {
        SaveSelectedWager();
    }
    #endregion
}
