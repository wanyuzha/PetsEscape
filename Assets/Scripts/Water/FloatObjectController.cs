using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObjectController : MonoBehaviour
{
    private Rigidbody2D rb;
    // public GameObject bubble;

    private float force;
    private float waterHeight;
    // private float mass;
    public GameObject waterArea;
    // private float floatObjectHeight;
    // bool underWater = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // BoxCollider2D boxCollider;
        // boxCollider = GetComponent<BoxCollider2D>();
        waterHeight = waterArea.GetComponent<DynamicWater2D>().curHeight + waterArea.GetComponent<DynamicWater2D>().bound.bottom + waterArea.transform.position.y + 0.5f;
        // waterHeight = waterArea.GetComponent<DynamicWater2D>().bound.top + 0.5f;
        // mass = rb.mass;
        // floatObjectHeight = boxCollider.size.y;
    }

    /*
    Apply buoyancy to objects in water, and remove the force once the object is out of water.
    */
    void FixedUpdate()
    {
        //force = -1 * Physics.gravity.y * 3f;
        float difference = transform.position.y - waterHeight;
        if (difference < 0)
        {
            Vector2 force = Vector2.up * 10f * rb.mass * Math.Abs(difference);
            rb.AddForce(force, ForceMode2D.Force);
            // underWater = true;
            // SwitchState(underWater);
            rb.drag = 10f;
        }
        else
        {
            // underWater = false;
            // SwitchState(underWater);
            rb.drag = 0f;
        }
    }
    /*
        void SwitchState(bool isUnderWater)
        {
            if (isUnderWater)
            {
                rb.drag = 5f;
            }
            else
            {
                rb.drag = 0f;
            }
        }
    */
}