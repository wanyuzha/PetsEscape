using UnityEngine;
using System.Collections;

public class Level2WinManager : MonoBehaviour
{
    // Use this for initialization
    public static bool BirdArrives = false;
    public static bool GetKey = false;
    public static bool DogArrives = false;
    public static bool FishArrives = false;
    public GameObject textMesh;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(BirdArrives);
        //Debug.Log(GetKey);
        //Debug.Log(DogArrives);
        //Debug.Log(FishArrives);
        if (BirdArrives && GetKey && DogArrives && FishArrives)
        {
            textMesh.SetActive(true);
            Debug.Log("You Win!!!");

        }
        else
        {
            textMesh.SetActive(false);
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
