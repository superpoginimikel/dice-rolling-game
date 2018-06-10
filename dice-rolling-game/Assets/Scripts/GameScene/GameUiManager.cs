using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUiManager : MonoBehaviour {

    public List<GameUi.TableUi> tableUiList;
    public List<GameUi.WagersUi> wagersUiList;

    [Space(5)]
    [Header("Game UI")]
    public Image tableImage;
    public Text coinsText;

    [Space(5)]
    [Header("Prefabs")]
    public GameObject wagerPrefab;

    [Space(5)]
    [Header("Parents")]
    public Transform wagerParentTransform;

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

    private void SetCoinsText(int coinsAmount)
    {
        coinsText.text = coinsAmount.ToString();
    }

    public void SetGameUi(GameDetails gameDetails)
    {
        tableImage.sprite = GetTableUi(gameDetails.VisualLayoutId);
        CreateWagers(gameDetails.WagersList);
    }

    private void CreateWagers(List<Wagers> wagersList)
    {
        int ctr = 0;
        foreach(Wagers wager in wagersList)
        {
            GameObject wagerGameObject = Instantiate(wagerPrefab, wagerParentTransform);
            wagerGameObject.GetComponent<WagerCoin>().Init(ctr, wager.Amount, GetWagerUi(wager.WagerVisualId));
            ctr++;
        }
    }
}
