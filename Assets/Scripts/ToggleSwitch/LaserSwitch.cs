using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : ToggleSwitch
{
    public GameObject laserBeam;

    // Start is called before the first frame update
    void Start()
    {
        laserBeam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void setActive()
    {
        laserBeam.SetActive(false);
    }

    protected override void setInactive()
    {
        if (laserBeam) laserBeam.SetActive(true);
    }
}
