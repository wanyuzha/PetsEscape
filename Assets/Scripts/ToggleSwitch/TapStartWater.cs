using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStartWater : ToggleSwitch
{
    public GameObject tap;
    public GameObject tapWater;
    public GameObject waterArea;

    // private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        tapWater.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void setActive()
    {
        if (!tapWater.activeSelf)
        {
            // isOpen = true;
            tapWater.SetActive(true);
            waterArea.GetComponent<DynamicWater2D>().waterLevelChange = true;
            waterArea.GetComponent<DynamicWater2D>().waterLevelDir = 1;
        }
    }

    protected override void setInactive()
    {
        if (tapWater.activeSelf)
        {
            // isOpen = false;
            if (tapWater) tapWater.SetActive(false);
            if (waterArea) waterArea.GetComponent<DynamicWater2D>().waterLevelChange = false;
        }
    }
}
