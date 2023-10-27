using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStartWater : MonoBehaviour
{
    public GameObject tap;
    public GameObject tapWater;
    public GameObject waterArea;

    static bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        tapWater.SetActive(false);
    }

    public void setTap()
    {
        if (!isOpen)
        {
            isOpen = true;
            tapWater.SetActive(true);
            waterArea.GetComponent<DynamicWater2D>().waterLevelChange = true;
            waterArea.GetComponent<DynamicWater2D>().waterLevelDir = 1;
        }
        else
        {
            isOpen = false;
            tapWater.SetActive(false);
            waterArea.GetComponent<DynamicWater2D>().waterLevelChange = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            setTap();
        }
    }

}
