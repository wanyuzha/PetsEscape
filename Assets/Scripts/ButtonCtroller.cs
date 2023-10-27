using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonCtroller : MonoBehaviour
{
    public Button button;

    public void RestartGame()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        
        // collect restart event
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelName", "level" + currentLevel.ToString() }
            };
        AnalyticsService.Instance.CustomData("restartEvent", parameters);
        AnalyticsService.Instance.Flush();
        
        HandleScene.RestartGame();
    }
    public void LoadNextLevel()
    {
        HandleScene.LoadNextLevel();
    }

    public void LoadPrevLevel()
    {
        HandleScene.LoadPrevLevel();
    }
}
