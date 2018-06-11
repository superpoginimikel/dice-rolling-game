﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigUiManager : MonoBehaviour {

    public GameObject loadingText;
    public GameObject startButton;

    public void ShowVersionUpdatePanel()
    {
        //TODO:: show version update panel
    }

    public void StartLoadingAnimation()
    {
        //TODO:: do loading animation, maybe a running dots on loading text
    }
   
    public void CompleteLoading()
    {
        loadingText.SetActive(false);
        startButton.SetActive(true);
    }

}
