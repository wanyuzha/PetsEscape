using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
// using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCtroller : MonoBehaviour
{
    

    public void RestartGame()
    {
        HandleScene.RestartGame();

        // int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        // int levelNumber = HandleScene.LevelNumber();
        if (HandleScene.LevelNumber() > 0)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelNumber", "Level " + HandleScene.LevelNumber() }
            };

            AnalyticsService.Instance.CustomData("restartEvent", parameters);
            AnalyticsService.Instance.Flush();
        }
    }

    public void LoadNextLevel()
    {
        HandleScene.LoadNextLevel();
    }

    public void LoadPrevLevel()
    {
        HandleScene.LoadPrevLevel();
    }

    /*
        public void SkipTutorialLevel()
        {
            HandleScene.LoadFirstNonTutorialLevel();
        }
    */

    public void LoadLevelNumber(int levelNumber)
    {
        HandleScene.LoadLevelNumber(levelNumber);
    }
    public void LoadHomeMenu()
    {
        HandleScene.LoadHome();
    }
    public void TogglePause()
    {
        if(Time.timeScale==0) HandleScene.ResumeGame();
        else HandleScene.PauseGame();
    }
}
