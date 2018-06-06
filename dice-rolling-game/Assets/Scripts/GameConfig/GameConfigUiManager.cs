using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigUiManager : MonoBehaviour {

    public GameObject loadingParentGameobject;
    public Image loadingProgresssBar;
    public GameObject startButton;

    public void ShowVersionUpdatePanel()
    {
        // show version update panel
    }

    public void DoLoadingAnimation(Action OnFinished)
    {
        // Run animation for loading
        StartCoroutine(TempDoLoading(OnFinished));
    }

    IEnumerator TempDoLoading(Action OnFinished)
    {
        float minProgressBarValue = 0f;
        float maxProgressBarValue = 1f;
        float elapsedTime = 0f;
        float totalDuration = 1f;

        float fillAmountValue = 0f;

        while (elapsedTime < totalDuration)
        {
            fillAmountValue = Mathf.Lerp(minProgressBarValue, maxProgressBarValue, (elapsedTime / totalDuration));
            elapsedTime += Time.deltaTime;
            loadingProgresssBar.fillAmount = fillAmountValue;
            yield return null;
        }

        OnFinished();
    }

    public void CompleteLoading()
    {
        loadingParentGameobject.SetActive(false);
        startButton.SetActive(true);
    }

}
