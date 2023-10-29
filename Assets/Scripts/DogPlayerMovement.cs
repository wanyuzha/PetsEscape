using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;
using TMPro;
using Unity.Services.Analytics;

public class DogPlayerMovement : Animal
{
    // private float previousHeight;

    private GameObject collideObject = null;
    const int SPEED = 6;
    const int JUMP_SPEED_X = 6;
    const int JUMP_SPEED_Y = 6;

    public DogPlayerMovement()
    {
        AnimalName = "Dog";
        // targetName.Add("target_pickup_for_test");
        // targetName.Add("Chair");
        // targetName.Add("obstacle");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!isActivated)
            return;

        undisplayArrow();
        // if (!firstTry)
        /*         if (currentSceneIndex > 0)
                    unshowTutorialText();
         */
        if (isJumping)
        {
            moveX(JUMP_SPEED_X);
        }
        else
        {
            moveX(SPEED);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DogJump(JUMP_SPEED_Y);
        }

        if (Input.GetKeyDown(skillKey))
        {
            crunch();
        }
    }

    private void crunch()
    {
        if (collideObject != null)
        {
            //ToDo: Not very accurate, need to be modified
            if (Vector2.Distance(collideObject.transform.position, transform.position) < 5)
            { //is nearby
                if (Mathf.Abs(collideObject.transform.position.x - transform.position.x) > (collideObject.GetComponent<Renderer>().bounds.size.x / 2))
                {
                    if (collideObject.name == "notch")
                    {
                        collideObject.GetComponent<TankWater>().activated();
                    }

                    Destroy(collideObject);
                    collideObject = null;
                }
            }

            //collect skill used event
            Analytics.SkillUsedEvent();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.name == "key")
        {
            items.Add("key");
            LevelWinManager.DogGetKey();
        }

        if (collision.gameObject.CompareTag("canCrunch"))
        {
            //Debug.Log(collision.gameObject.transform.position.x - transform.position.x);
            //Debug.Log(collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2);
            // if (Mathf.Abs(collision.gameObject.transform.position.x - transform.position.x) > (collision.gameObject.GetComponent<Renderer>().bounds.size.x / 2))
            // {
            // Debug.Log("try biting");
            collideObject = collision.gameObject;

            /*
                //if trying to pick up the item for the first time, show tutorial text
                if (firstTry)
                {
                    showTutorialText("Use [Z] to crunch the obstacle!\nPress [Enter] to continue!");
                    firstTry = false;
                }
            */
            // }
        }

        /*if (collision.gameObject.name == "Notch")
        {
            Debug.Log("Yes it is");
            collideObject = collision.gameObject;
        }*/
    }

    /*     void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.name.StartsWith("Water"))
            {
                Debug.Log("dog leave water");
                inWater = false;
            }

        } */

    protected override void OnCollisionExit2D(Collision2D coll)
    {
        base.OnCollisionExit2D(coll);
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);

        /*
            if (coll.gameObject.name == "Door")
            {
                Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();

                if (currentSceneIndex == 0)
                {
                    if (!LevelWinManager.GetKey)
                    {
                        showTutorialText("Door is locked! Get the key and touch the door to unlock!\nPress [Enter] to continue!");
                    }
                }

                if (colliderComponent != null)
                {
                    Destroy(colliderComponent);
                }
            }
        */

        if (coll.gameObject.name == "Door" && LevelWinManager.GetKey)
        {
            LevelWinManager.DogTouchDoor();
            if (firstWin)
            {
                showTutorialText("Dog unlocks the Door!\nPress [Enter] to continue!");
                firstWin = false;
            }
        }
    }

    protected override void CheckInWater()
    {
        base.CheckInWater();
    }
}
