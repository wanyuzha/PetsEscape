using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool fish = false;
    public static bool bird = false;
    public static bool dog = false;

    
    public static void FishWin()
    {
        fish = true;
        CheckWinCondition();
    }

    public static void BirdWin()
    {
        bird = true;
        Debug.Log("Bird Wins");
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
            Debug.Log("Win!");
        }
    }

   
}
