using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUiManager : MonoBehaviour {

    public Text coinsText;

    public void SetCoinsText(int coinsValue)
    {
        coinsText.text = coinsValue.ToString();
    }
}
