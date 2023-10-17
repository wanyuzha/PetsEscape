using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObjectController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public GameObject bubble;

    private float force;
    private float waterHeight = -6.9f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /*
    Apply buoyancy to objects in water, and remove the force once the object is out of water.
    */
    void FixedUpdate(){
        force = -1 * Physics.gravity.y * 3f;
        float difference = transform.position.y - waterHeight;
        if(difference < 0)
        {
            rigidBody.AddForce(Vector3.up * force * Math.Abs(difference), ForceMode2D.Force);
            //Debug.Log(Math.Abs(difference));
        }

        
    }
}
