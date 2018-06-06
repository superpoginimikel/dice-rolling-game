using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameConfigUiManager : MonoBehaviour {

    public GameObject loadingParentGameobject;
    public Image loadingProgresssBar;
    public GameObject startButton;

    float minProgressBarValue = 0f;
    float maxProgressBarValue = 1f;
    float totalProgressBarLoadDuration = 1f;

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
        float elapsedTime = 0f;
        float fillAmountValue = 0f;

        while (elapsedTime < totalProgressBarLoadDuration)
        {
            fillAmountValue = Mathf.Lerp(minProgressBarValue, maxProgressBarValue, (elapsedTime / totalProgressBarLoadDuration));
            elapsedTime += Time.deltaTime;
            loadingProgresssBar.fillAmount = fillAmountValue;
            yield return null;
        }
        loadingProgresssBar.fillAmount = 1f;

        OnFinished();
    }

    public void CompleteLoading()
    {
        loadingParentGameobject.SetActive(false);
        startButton.SetActive(true);
    }

}
