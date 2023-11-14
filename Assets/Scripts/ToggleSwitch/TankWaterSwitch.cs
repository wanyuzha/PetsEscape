using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWaterSwitch : ToggleSwitch
{
    public GameObject waterArea;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void setActive()
    {
        waterArea.GetComponent<DynamicWater2D>().waterLevelChange = true;
        waterArea.GetComponent<DynamicWater2D>().waterLevelDir = -1;
    }

    protected override void setInactive()
    {
        if (waterArea) waterArea.GetComponent<DynamicWater2D>().waterLevelChange = false;
    }
}
