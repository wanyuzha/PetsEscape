using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this bar only used by fish
public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private static float maxHealth = 5f;
    private static float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;

        currentHealth = maxHealth;
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
        currentHealth += 5f;
        // maxHealth += 5f;
    }

    /*
        public static void EndPower()
        {
            maxHealth = 5f;
        }
    */
}
