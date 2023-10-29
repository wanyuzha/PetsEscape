using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapStartWater : MonoBehaviour
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

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            setTapActive();
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            setTapInactive();
        }
    }

    private void setTapActive()
    {
        if (!tapWater.activeSelf)
        {
            // isOpen = true;
            tapWater.SetActive(true);
            waterArea.GetComponent<DynamicWater2D>().waterLevelChange = true;
            waterArea.GetComponent<DynamicWater2D>().waterLevelDir = 1;
        }
    }

    private void setTapInactive()
    {
        if (tapWater.activeSelf)
        {
            // isOpen = false;
            tapWater.SetActive(false);
            waterArea.GetComponent<DynamicWater2D>().waterLevelChange = false;
        }
    }
}
