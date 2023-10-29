using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObjectController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // public GameObject bubble;

    private float force;
    private float waterHeight;
    public GameObject waterArea;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //waterHeight = transform.position.y;
        waterHeight = waterArea.GetComponent<DynamicWater2D>().curHeight + waterArea.GetComponent<DynamicWater2D>().bound.bottom + waterArea.transform.position.y;
    }

    /*
    Apply buoyancy to objects in water, and remove the force once the object is out of water.
    */
    void LateUpdate()
    {
        force = -1 * Physics.gravity.y * 3f;
        float difference = transform.position.y - waterHeight;
        if (difference < 0)
        {
            rigidBody.AddForce(Vector3.up * force * Math.Abs(difference), ForceMode2D.Force);
        }
    }
}
