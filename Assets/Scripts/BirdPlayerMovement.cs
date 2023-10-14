using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdPlayerMovement : Animal
{

    const int SPEED = 5;
    const int JUMP_SPEED_Y = 3;

    private bool isPickupAnything = false;
    private GameObject collideObject;
    private GameObject pickupObject;
    private Vector3 grabOffset;

    public BirdPlayerMovement()
    {
        AnimalName = "Bird";
        // direction = -1;
        // canJump = false;
        // targetName.Add("target_pickup_for_test");
        // targetName.Add("key");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        isActivated = true;
        Debug.Log(currentSceneIndex);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!this.gameObject.activeSelf)
            return;

        if (!isActivated)
            return;

        undisplayArrow();
        // if (!firstTry)
        if (currentSceneIndex > 0)
            unshowTutorialText();

        moveX(SPEED);

        // moveY(SPEED);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fly(JUMP_SPEED_Y);
        }

        // update picked up item
        // if (pickupObject != null)
        // {
        //     pickupObject.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        // }
        /*
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 7, 0);
            Debug.Log("jumping");
        }
        */

        // level 1 not trigger the pickup skill for bird
        if (Input.GetKeyDown(skillKey) && currentSceneIndex > 0)
        {
            /*
             * collideObject: only valid when there is collision otherwise it will be null
             * isPickupAnything: bool if true means something being picked up and reference caught by pickupObject
             * set the parent of pickupObject makes bird and object a whole
             */
            Debug.Log("isPickingupAnything: " + isPickupAnything);

            if (isPickupAnything)
            {
                pickupObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                // pickupObject.transform.SetParent(null);
                // pickupObject.GetComponent<Rigidbody2D>().isKinematic = false;
                pickupObject = null;
                isPickupAnything = false;
            }

            else if (collideObject != null)
            {
                pickupObject = collideObject;
                // pickupObject.transform.SetParent(this.transform);
                grabOffset = pickupObject.transform.position - transform.position;
                // pickupObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //Physics.IgnoreCollision(this.gameObject.AddComponent<Collider>(), collideObject.GetComponent<Collider>());
                // collideObject = null;
                isPickupAnything = true;
                //Debug.Log(dirY);
            }

            firstTry = false;
        }

        if (isPickupAnything)
        {
            pickupObject.transform.position = transform.position + grabOffset;
        }
    }

    /*    void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.name.StartsWith("Water"))
            {
                Debug.Log("bird leave water");
                inWater = false;
            }

        }

       void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.name.StartsWith("Water"))
            {
                Debug.Log("bird enter water");
                inWater = true;
                health = 10;
            }
        }*/

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.CompareTag("canGrab"))
        {
            Debug.Log("try picking");
            collideObject = collision.gameObject;
            //if trying to pick up the item for the first time, show tutorial text
            if (firstTry && currentSceneIndex > 0)
            {
                showTutorialText("Press Z to pick up the item");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("gameobject name: " + collision.gameObject.name);

        if (collideObject != null)
        {
            Debug.Log("collideObject: " + collideObject.name);
            if (collision.gameObject.name == collideObject.name)
            {
                collideObject = null;
            }
        }
    }
}
