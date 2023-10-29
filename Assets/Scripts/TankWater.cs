using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankWater : MonoBehaviour
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

    public void activated()
    {
        waterArea.GetComponent<DynamicWater2D>().waterLevelChange = true;
        waterArea.GetComponent<DynamicWater2D>().waterLevelDir = -1;
        waterArea.GetComponent<DynamicWater2D>().maxHeight = waterArea.GetComponent<DynamicWater2D>().minHeight + 0.5f;
    }
}
