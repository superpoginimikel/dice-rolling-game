using UnityEngine;

public class BetGroup : MonoBehaviour {

	public BetButton GetBetButtonById(int betButtonId)
    {
        foreach(Transform child in this.transform)
        {
            BetButton betButton = child.GetComponent<BetButton>();
            if(betButton.betId == betButtonId)
            {
                return betButton;
            }
        }

        return null;
    }
}
