using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 1.5f;
    public float displayTime = 1.5f;

    void Start()
    {
        StartCoroutine(FadeImage());
    }

    IEnumerator FadeImage()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null) yield break;

        // 
        canvasGroup.alpha = 1f;
        // 
        yield return new WaitForSeconds(displayTime);

        // 
        float startTime = Time.time;
        while (Time.time < startTime + fadeOutTime)
        {
            float elapsed = Time.time - startTime;
            canvasGroup.alpha = 1 - (elapsed / fadeOutTime);
            yield return null;
        }

        // 
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
