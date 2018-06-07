﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeButton : MonoBehaviour {

    private int gameModeId;
    private string jsonPath;

    public Image gameModeImage;
    public Text gameModeName;

    public void Init(int gameModeId, string gameModeName, Sprite gameModeImage, string jsonPath)
    {
        this.gameModeId = gameModeId;
        this.gameModeName.text = gameModeName;
        this.gameModeImage.sprite = gameModeImage;
        this.jsonPath = jsonPath;
    }

    #region OnClick

    public void OnClickGameButton()
    {
        AccountManager.Instance.GameModeId = this.gameModeId;
        print("game mode id :::" + gameModeId);

        SceneManager.LoadScene("GameScene");
    }

    #endregion
}