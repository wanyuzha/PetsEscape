using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCtroller : MonoBehaviour
{
    public Button button;

    public void RestartGame()
    {
        HandleScene.RestartGame();
    }
    public void LoadNextLevel()
    {
        HandleScene.LoadNextLevel();
    }
}
