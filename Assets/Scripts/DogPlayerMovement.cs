using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DogPlayerMovement : Animal
{
    // private float previousHeight;

    private GameObject collideObject;
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
    void Update()
    {
        if (!isActivated)
            return;

        undisplayArrow();

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
            jump(JUMP_SPEED_Y);
        }

        if (Input.GetKeyDown(KeyCode.Z) && currentSceneIndex != 0)
        {
            crunch();
        }
    }

    void crunch()
    {
        if (collideObject != null)
        {
            if (Vector2.Distance(collideObject.transform.position, transform.position) < 2)
            { //is nearby
                Destroy(collideObject);
                collideObject = null;
            }

        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if (collision.gameObject.name == "key")
        {
            items.Add("key");
            //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(currentSceneIndex + 1);
        }
        if (collision.gameObject.CompareTag("canCrunch"))
        {
            Debug.Log("try biting");
            collideObject = collision.gameObject;
            //if trying to pick up the item for the first time, show tutorial text
            if (firstTry)
            {
                showTutorialText("Press Z to crunch an item nearby");
                firstTry = false;
            }

        }

    }

    /*     void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.name.StartsWith("Water"))
            {
                Debug.Log("dog leave water");
                inWater = false;
            }

        } */

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);
        if (coll.gameObject.name == "door")
        {
            Debug.Log("Dog entered Door");
            Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();
            if (colliderComponent != null)
            {
                Destroy(colliderComponent);
            }
            showTutorialText("Use key to open the door\npress any key to continue");
        }
    }

    protected override void CheckInWater()
    {
        base.CheckInWater();
    }

}