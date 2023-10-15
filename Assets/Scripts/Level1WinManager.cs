using UnityEngine;
using System.Collections;

public class Level1WinManager : MonoBehaviour
{
    // Use this for initialization
    public static bool BirdArrives = false;
    public static bool GetKey = false;
    public static bool DogArrives = false;
    public static bool FishArrives = false;
    //public GameObject textMesh;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (BirdArrives && GetKey && DogArrives && FishArrives)
        {
            
            HandleScene handle = new HandleScene();
            handle.LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            HandleScene handle = new HandleScene();
            handle.RestartGame();
        }
        
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
