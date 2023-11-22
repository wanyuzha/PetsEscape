using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this bar only used by fish
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;

      private static float maxHealth;
    private static float currentHealth;
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
        maxHealth = 5f;
        currentHealth = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = currentHealth / maxHealth;
    }


    public static void SetHealth(float health)
    {
        currentHealth = health;
    }

    public static void EatPower()
    {
        currentHealth = currentHealth + 5;
        maxHealth = maxHealth + 5;
      
    }

    public static void EndPower()
    {
        maxHealth = maxHealth - 5;
       
    }

    public static float GetMaxHealth()
    {
        return maxHealth;
    }

   
}
