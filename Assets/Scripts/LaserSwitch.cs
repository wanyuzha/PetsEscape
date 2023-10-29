using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitch : MonoBehaviour
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

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            laserBeam.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            laserBeam.SetActive(true);
        }
    }
}
