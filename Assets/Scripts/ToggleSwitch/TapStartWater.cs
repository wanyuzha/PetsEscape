using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStartWater : ToggleSwitch
{
    public GameObject tap;
    public GameObject tapWater;
    public GameObject waterArea;
    const float waterForce = -0.08f;
    const float radius = 1f;
    Bounds bounds;

    // private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        tapWater.SetActive(false);
        InvokeRepeating("tapWaterSplash", 1, 0.3f);
        bounds = tapWater.GetComponent<Renderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void tapWaterSplash()
    {
        if (tapWater && tapWater.activeSelf)
        {
            Bounds newBounds = new Bounds(bounds.center, new Vector3(radius, bounds.max.y - bounds.min.y, 0));
            waterArea.GetComponent<DynamicWater2D>().Splash(newBounds, waterForce, true);
        }
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
        if (tapWater && tapWater.activeSelf)
        {
            // isOpen = false;
            if (tapWater) tapWater.SetActive(false);
            if (waterArea) waterArea.GetComponent<DynamicWater2D>().waterLevelChange = false;
        }
    }
}
