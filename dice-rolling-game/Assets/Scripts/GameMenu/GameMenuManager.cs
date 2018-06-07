using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour {

    public GameMenuUiManager gameMenuUiManager;

    public Transform gamesListParent;
    public GameObject gameMenuButtonPrefab;

	void Start()
    {
        gameMenuUiManager.SetCoinsText(AccountManager.Instance.Coins);
        InitMenu();
    }

    private void InitMenu()
    {
        GameModesList gameModesList = AccountManager.Instance.GetGameModesList();
        foreach(GameMode gameMode in gameModesList.GameList)
        {
            GameObject gameButton = Instantiate(gameMenuButtonPrefab, gamesListParent);
            GameModeButton gameModeButton = gameButton.GetComponent<GameModeButton>();

            string spritePath = Path.Combine(Application.persistentDataPath, gameMode.ImagePath);
            Sprite gameButtonSprite = GameUtil.GetSpriteByPath(spritePath);

            gameModeButton.Init(gameMode.GameId, gameMode.Name, gameButtonSprite, gameMode.JsonPath);
        }
    }


}
