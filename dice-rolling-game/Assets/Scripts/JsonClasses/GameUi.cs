using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameUi : MonoBehaviour {

    [Serializable]
    public class TableUi
    {
        public int VisualLayoutId;
        public Sprite TableUiSprite;
    }

    [Serializable]
    public class WagersUi
    {
        public int WagerId;
        public Sprite WagerUiSprite;
    }
}
