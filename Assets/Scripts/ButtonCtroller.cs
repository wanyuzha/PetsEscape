using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonCtroller : MonoBehaviour
{
    // Start is called before the first frame update

    public Button button;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void RestartGame()
    {
        HandleScene.RestartGame();
    }
    public void LoadNextLevel()
    {
        HandleScene.LoadNextLevel();
    }
}
