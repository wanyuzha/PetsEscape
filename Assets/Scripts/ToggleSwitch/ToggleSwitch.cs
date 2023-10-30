using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void setActive()
    {

    }

    protected virtual void setInactive()
    {

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            setActive();
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("canGrab") || coll.gameObject.CompareTag("canCrunch"))
        {
            setInactive();
        }
    }
}
