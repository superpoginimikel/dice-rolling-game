﻿using UnityEngine;
using System;
using System.IO;

public class GameConfigManager : MonoBehaviour {

    public GameConfigUiManager gameConfigUiManager;

	// Use this for initialization
	void Start () {
        print(Application.persistentDataPath);
        VersionCheck();
    }

    void VersionCheck()
    {
        bool versionUpdate = false; // test var
        // check for any version update required

        if(versionUpdate) {
            gameConfigUiManager.ShowVersionUpdatePanel();
        } else {
            UnzipGameData();
        }
    }

    void UnzipGameData()
    {
        string unZipDataCheck = Path.Combine(Application.persistentDataPath, GameDataConstant.UnZipFilePath);
        print(unZipDataCheck);

        //unzip data
        if (!File.Exists(unZipDataCheck))
        {
            string zipName = Path.Combine(Application.streamingAssetsPath, GameDataConstant.GameZipName);
            ZipUtil.Unzip(zipName, Application.persistentDataPath);
            print("unzip done");
        } else
        {
            print("already unzipped");
        }

        gameConfigUiManager.DoLoadingAnimation(CompleteLoadingAnimation);
    }

    void CompleteLoadingAnimation()
    {
        gameConfigUiManager.CompleteLoading();
    }
}
