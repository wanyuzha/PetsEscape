using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelWinManager : MonoBehaviour
{
    // Use this for initialization
    public static bool BirdArrives = false;
    public static bool GetKey = false;
    public static bool DogArrives = false;
    public static bool FishArrives = false;

    //public GameObject textMesh;
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            HandleScene.RestartGame();
        }

        if (WinCondition() && !HandleScene.isMaxLevel())
        {
            HandleScene.LoadNextLevel();
        }
        else if(WinCondition() && HandleScene.isMaxLevel())
        {
            Debug.Log("yOU win");
        }

    }

    public abstract bool WinCondition();
 
    public static void Initialize()
    {
        BirdArrives = false;
        GetKey = false;
        DogArrives = false;
        FishArrives = false;
    }

    public static void BirdTouchGate()
    {
        BirdArrives = true;
    }

    public static void DogGetKey()
    {
        GetKey = true;
    }

    public static void DogTouchDoor()
    {
        DogArrives = true;
    }

    public static void FishTouchGoal()
    {
        FishArrives = true;
    }
}
