using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This bar only used by bird catching
public class ProgressBar : MonoBehaviour
{

   
    private Slider slider;

    private static float powerupDuration = 20f;
    private float totalTime = 20f; 
    private static float currentTime = 20f;
    private float fillAmount = 1f;
    // Start is called before the first frame update

    void Start()
    {
    
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCountdown();

        fillAmount = currentTime / totalTime;

        slider.value = fillAmount;

       // UpdatePosition();



    }



  

    public void UpdateCountdown()
    {
        currentTime = currentTime - Time.deltaTime;

        if (currentTime <= 0f)
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
