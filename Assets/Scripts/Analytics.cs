using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

using Unity.Services.Core.Environments;
using UnityEngine.Analytics;

public class Analytics : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BirdDeadCustomEvent()
    { 
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"birdDead", "birdDead" }
        };
        

        AnalyticsService.Instance.CustomData("birdDead", parameters);
        AnalyticsService.Instance.Flush();
        

            }
}
