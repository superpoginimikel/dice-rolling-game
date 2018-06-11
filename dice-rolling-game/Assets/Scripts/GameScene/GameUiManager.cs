using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUiManager : MonoBehaviour {

    public List<GameUi.TableUi> tableUiList;
    public List<GameUi.WagersUi> wagersUiList;
    public List<GameUi.BetTypes> betTypesUiList;

    [Space(5)]
    [Header("Game UI")]
    public Text gameNameText;
    public Image tableImage;
    public Image resultPanelImage;
    public Text coinsText;
    public Button playButton;

    public GameObject helpButtonPanel;

    [Space(5)]
    [Header("loading animation mask related")]
    public GameObject diceRollingLoadingGameObject;
    public GameObject diceRollingAnimationTextGO;
    public Text wonAmountText;

    [Space(5)]
    [Header("Prefabs")]
    public GameObject wagerPrefab;
    public GameObject diceResultPrefab;
    public GameObject betButtonPrefab;

    [Space(5)]
    [Header("Parents")]
    public Transform wagerParentTransform;
    public Transform betTypesParentTransform;
    public Transform diceResultParentTransform;

    private float wonAmountTextDelay = 1.5f;

    public Sprite GetTableUi(int visualId)
    {
        foreach (GameUi.TableUi table in tableUiList)
        {
            if (table.VisualLayoutId == visualId)
            {
                return table.TableUiSprite;
            }
        }

        return null;
    }

    public Sprite GetWagerUi(int wagerVisualId)
    {
        foreach(GameUi.WagersUi wager in wagersUiList)
        {
            if(wager.WagerId == wagerVisualId)
            {
                return wager.WagerUiSprite;
            }
        }

        return null;
    }

    public GameObject GetBetGroupTypePrefab(int betTypeGroupId)
    {
        foreach(GameUi.BetTypes betType in betTypesUiList)
        {
            if(betType.BetTypeGroupId == betTypeGroupId)
            {
                return betType.BetTypePrefab;
            }
        }

        return null;
    }

    public void SetCoinsText(int coinsAmount)
    {
        coinsText.text = coinsAmount.ToString();
    }

    public void EnablePlayButton(bool isEnable)
    {
        playButton.interactable = isEnable;
    }

    public void ShowHideHelpPanel(bool isShow)
    {
        helpButtonPanel.SetActive(isShow);
    }

    public void ShowLoadingAnimationText()
    {
        diceRollingLoadingGameObject.SetActive(true);
        diceRollingAnimationTextGO.SetActive(true);
        wonAmountText.gameObject.SetActive(false);
    }

    void ShowWonAmount(int amountWon)
    {
        if(amountWon != 0)
        {
            wonAmountText.text = amountWon.ToString();
            diceRollingAnimationTextGO.SetActive(false);
            wonAmountText.gameObject.SetActive(true);
            StartCoroutine(HideWonAmountText());
        } else
        {
            diceRollingLoadingGameObject.SetActive(false);
        }
    }

    IEnumerator HideWonAmountText()
    {
        yield return new WaitForSeconds(wonAmountTextDelay);
        diceRollingLoadingGameObject.SetActive(false);
        wonAmountText.gameObject.SetActive(false);
    }

    public void SetGameUi(GameDetails gameDetails, int totalCoins)
    {
        gameNameText.text = gameDetails.Name;
        tableImage.sprite = GetTableUi(gameDetails.VisualLayoutId);
        resultPanelImage.sprite = GetTableUi(gameDetails.VisualLayoutId);
        CreateWagers(gameDetails.WagersList);
        CreateBetTypes(gameDetails.BetTypes);
        SetCoinsText(totalCoins);
        EnablePlayButton(false);
    }

    public void WagerClickUpdateButtonInteractable(int wagerId, int totalCoins)
    {
        foreach(Transform child in wagerParentTransform)
        {
            WagerCoin childWagerCoin = child.GetComponent<WagerCoin>();
            bool isSelected = (childWagerCoin.GetWagerId() == wagerId) ? true : false;
            if(isSelected)
            {
                childWagerCoin.SetIsSelected(true);
                childWagerCoin.UpdateCoinSelectedUi(false);
            } else
            {
                childWagerCoin.SetIsSelected(false);
                bool hasEnoughMoneyToBet = (childWagerCoin.GetWagerAmount() > totalCoins) ? false : true;
                childWagerCoin.UpdateCoinSelectedUi(hasEnoughMoneyToBet);
            }
        }
    }

    private void CreateWagers(List<Wagers> wagersList)
    {
        int ctr = 0;
        foreach(Wagers wager in wagersList)
        {
            GameObject wagerGameObject = Instantiate(wagerPrefab, wagerParentTransform);
            WagerCoin wagerCoin = wagerGameObject.GetComponent<WagerCoin>();
            wagerCoin.Init(ctr, wager.Amount, GetWagerUi(wager.WagerVisualId));
            if(ctr == 0)
            {
                wagerCoin.SaveSelectedWager();
            }
            ctr++;
        }
    }

    public void UpdateWagers(int totalCoins)
    {
        foreach(Transform child in wagerParentTransform)
        {
            WagerCoin wagerCoin = child.GetComponent<WagerCoin>();
            if(wagerCoin.GetIsSelected() == false)
            {
                wagerCoin.UpdateCoinSelectedUi(totalCoins);
            } else
            {
                wagerCoin.UpdateCoinSelectedUi(false);
            }
        }
    }

    private void CreateBetTypes(List<BetTypes> betTypesList)
    {
        foreach(BetTypes betType in betTypesList)
        {
            if (betType.ShowBet)
            {
                GameObject betGroupPrefab = GetBetGroupTypePrefab(betType.BetTypeGroupId);
                GameObject betGroup = Instantiate(betGroupPrefab, betTypesParentTransform);

                foreach (BetDetails betDetail in betType.BetDetails)
                {
                    if (betDetail.ShowBet)
                    {
                        Transform betButtonParent = betGroup.GetComponent<BetGroup>().GetButtonParent();
                        GameObject betButtonGO = Instantiate(betButtonPrefab, betButtonParent);
                        BetButton betButton = betButtonGO.GetComponent<BetButton>();
                        betButton.InitBetButton(betDetail.BetId, betType.BetTypeGroupId, betDetail.BetName, betDetail.Multiplier, betDetail.NumberExistValue);
                    }
                }
            }
        }
    }

    void AddDiceRollInResultPanel(List<int> diceRollResult)
    {
        GameObject diceResultGameObject = Instantiate(diceResultPrefab, diceResultParentTransform);
        diceResultGameObject.transform.SetAsFirstSibling();
        int ctr = 0;    
        foreach(int diceRoll in diceRollResult)
        {
            if(diceResultGameObject.transform.childCount > ctr)
            {
                diceResultGameObject.transform.GetChild(ctr).gameObject.SetActive(true);
                diceResultGameObject.transform.GetChild(ctr).GetComponentInChildren<Text>().text = diceRoll.ToString();
            }
            ctr++;
        }
    }

    public void BetRoundDone(GameJsonFromServer gameJsonFromServer)
    {
        AddDiceRollInResultPanel(gameJsonFromServer.diceRollList);
        ShowWonAmount(gameJsonFromServer.totalAmountWon);
        EnablePlayButton(false);
    }
}
