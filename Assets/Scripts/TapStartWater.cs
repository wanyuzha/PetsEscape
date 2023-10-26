using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStartWater : MonoBehaviour
{
    public GameObject prefab;
    public GameObject tap;
    static bool isOpen = false;

    private float spawnRate = 0.2f; // 0.2s per instance
    private float nextSpawnTime = 0f;
    static float startTime = 0.0f;
    private float duration = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void setTap()
    {
        isOpen = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen)
        {
            if (Time.time > nextSpawnTime && Time.time - startTime < duration)
            {
                Vector3 offset = new Vector3(0.69f, -0.5f, 0f);
                Instantiate(prefab, tap.transform.position+offset, Quaternion.identity);
                nextSpawnTime = Time.time + spawnRate;
            }
            else if(Time.time - startTime > duration)
            {
                isOpen = false;
            }
        } 
    }
}
