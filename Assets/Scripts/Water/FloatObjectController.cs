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
    private float mass;
    public GameObject waterArea;
    private float floatObjectHeight;
    bool underWater = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        BoxCollider2D boxCollider;boxCollider = GetComponent<BoxCollider2D>();
        //waterHeight = waterArea.GetComponent<DynamicWater2D>().curHeight + waterArea.GetComponent<DynamicWater2D>().bound.bottom + waterArea.transform.position.y;
        waterHeight = waterArea.GetComponent<DynamicWater2D>().bound.top+0.5f;
        mass = rigidBody.mass;
        floatObjectHeight = boxCollider.size.y;

    }

    /*
    Apply buoyancy to objects in water, and remove the force once the object is out of water.
    */
    void LateUpdate()
    {

        //force = -1 * Physics.gravity.y * 3f;
        
        float difference = transform.position.y - waterHeight;
        Vector3 force = Vector3.up * 2f* mass * Math.Abs(difference);
        if (difference < 0)
        {   
            rigidBody.AddForce(force, ForceMode2D.Force);
            underWater = true;
            SwitchState(underWater);
        }
        else{
            underWater = false;
            SwitchState(underWater);
        }
        Debug.Log(waterHeight);

    }
    void SwitchState(bool isUnderWater)
    {
        if(isUnderWater)
        {
            rigidBody.drag = 5f;
        }else
        {
            rigidBody.drag = 0f;
        }
    }
}
