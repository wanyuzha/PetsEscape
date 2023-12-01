using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This bar only used by dog
public class ProgressBarDog : MonoBehaviour
{
    private Slider slider;

    private static float powerupDuration = 10f;
    private static float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;

        ResetProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountdown();

        slider.value = currentTime / powerupDuration;

        // UpdatePosition();
    }

    public void UpdateCountdown()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0f)
        {
            currentTime = 0f;
        }

        // UpdateCountdownText();
    }

    // reset the powerup progress bar
    public static void ResetProgressBar()
    {
        currentTime = powerupDuration;
    }
}
