using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

using Unity.Services.Core.Environments;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Analytics : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        //basic data collection such as game start, game end
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this function is an example and not used.
    /*
        //For birdDeadCustomEvent, it is in the animal.cs, where bird is killed by laser
        public void BirdDeadCustomEvent()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"birdDead", "birdDead" }
            };

            AnalyticsService.Instance.CustomData("birdDead", parameters);
            AnalyticsService.Instance.Flush();
        }
    */

    public void BirdFlyCustomEvent()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"birdFly", "birdFly"}
        };

        AnalyticsService.Instance.CustomData("birdFly", parameters);
        AnalyticsService.Instance.Flush();
    }

    //this static function will be called when skill used event is triggered
    public static void SkillUsedEvent()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelName", "level" + currentLevel.ToString() }
            };
        AnalyticsService.Instance.CustomData("skillUsedEvent", parameters);
        AnalyticsService.Instance.Flush();
    }
}
