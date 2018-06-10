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
    public Text coinsText;

    [Space(5)]
    [Header("Prefabs")]
    public GameObject wagerPrefab;

    [Space(5)]
    [Header("Parents")]
    public Transform wagerParentTransform;
    public Transform betTypesParentTransform;

    public Sprite GetTableUi(int visualId)
    {
        print("visual id :::" + visualId);
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

    public void SetGameUi(GameDetails gameDetails, int totalCoins)
    {
        gameNameText.text = gameDetails.Name;
        tableImage.sprite = GetTableUi(gameDetails.VisualLayoutId);
        CreateWagers(gameDetails.WagersList);
        CreateBetTypes(gameDetails.BetTypes);
        SetCoinsText(totalCoins);
    }

    public void WagerClickUpdateButtonInteractable(int wagerId)
    {
        foreach(Transform child in wagerParentTransform)
        {
            WagerCoin childWagerCoin = child.GetComponent<WagerCoin>();
            bool isSelected = (childWagerCoin.GetWagerId() == wagerId) ? true : false;
            childWagerCoin.UpdateCoinSelectedUi(isSelected);
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
            wagerCoin.UpdateCoinSelectedUi(totalCoins);
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
                    if(betDetail.ShowBet)
                    {
                        BetButton betButton = betGroup.GetComponent<BetGroup>().GetBetButtonById(betDetail.BetId);
                        betButton.InitBetButton(betType.BetTypeGroupId, betDetail.BetName, betDetail.Multiplier);
                    }
                }
            }
        }
    }
}
