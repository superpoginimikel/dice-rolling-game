using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameConfigManager : MonoBehaviour {

    public GameConfigUiManager gameConfigUiManager;

	void Start () {
        VersionCheck();
        gameConfigUiManager.StartLoadingAnimation();
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
        string unZipDataCheck = Path.Combine(Application.persistentDataPath, GameDataConstant.GameConfigJsonFile);

        //unzip data
        if (!File.Exists(unZipDataCheck))
        {
            string zipPath = Path.Combine(Application.streamingAssetsPath, GameDataConstant.GameZipName);
            GameUtil.UnZip(zipPath, Application.persistentDataPath);
            //print("unzip done");
        } else
        {
            //print("already unzipped");
        }

        gameConfigUiManager.CompleteLoading();
    }

    #region OnClickFunctions

    public void OnClickStartButton()
    {
        // Run loading scene script to add transition
        SceneManager.LoadScene("GameMenu");
    }

    #endregion
}
