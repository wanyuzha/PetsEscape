using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool fish = false;
    public static bool bird = false;
    public static bool dog = false;
    public static Text winText;


    public void Start()
    {
        winText = GameObject.Find("WinText").GetComponent<Text>();
        Debug.Log(winText);
        winText.gameObject.SetActive(false);
    }
    public static void FishWin()
    {
        fish = true;
        Debug.Log("Fish Wins");
        CheckWinCondition();
    }

    public static void BirdWin()
    {
        bird = true;
        winText.gameObject.SetActive(true);
        CheckWinCondition();
    }

    public static void DogWin()
    {
        dog = true;
        Debug.Log("Dog Wins");
        CheckWinCondition();
    }

    public static void CheckWinCondition()
    {
        if (fish && bird && dog)
        {
            
            Debug.Log("All Win! Display winning sign");
        }
    }

    


}
