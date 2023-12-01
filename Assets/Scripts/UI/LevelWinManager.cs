using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWinManager : MonoBehaviour
{
    // Use this for initialization
    public static bool BirdArrives = false;
    public static bool GetKey = false;
    public static bool DogArrives = false;
    public static bool FishArrives = false;
    private GameObject panel;
    private GameObject tutorial;
    private Text panelText;

    //public GameObject textMesh;
    void Start()
    {
        Initialize();
        tutorial = HandleScene.FindSiblingGameObject("Tutorial");
        panel = HandleScene.FindSiblingGameObject("Canvas").transform.Find("Panel").gameObject;
        panelText = panel.transform.Find("GameResult").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        // HandleScene.RestartGame();
        // }
        //
        if (WinCondition() && !tutorial.activeSelf)
        {
            if (HandleScene.isMaxLevel())
            {
                // execute only one time for stop the timer, because this is the final level, so no next level will be loaded 
                if (!panel.activeSelf) HandleScene.LoadNextLevel();

                panelText.text = "Game Complete!!!";
                panel.SetActive(true);
               
            }
            else
            {
                HandleScene.LoadNextLevel();
            }
        }
    }

    public virtual bool WinCondition()
    {
        return BirdArrives && GetKey && DogArrives && FishArrives;
    }

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
