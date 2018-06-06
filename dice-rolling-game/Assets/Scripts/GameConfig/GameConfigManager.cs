using UnityEngine;
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

        //unzip data
        if (!Directory.Exists(unZipDataCheck))
        {
            string zipName = Path.Combine(Application.streamingAssetsPath, GameDataConstant.GameZipName);
            ZipUtil.Unzip(zipName, Application.persistentDataPath);
            print("unzip done");
        }

        gameConfigUiManager.DoLoadingAnimation(CompleteLoadingAnimation);
    }

    void CompleteLoadingAnimation()
    {
        gameConfigUiManager.CompleteLoading();
    }
}
