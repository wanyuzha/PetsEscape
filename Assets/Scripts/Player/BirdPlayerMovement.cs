using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;
using TMPro;
using Unity.Services.Analytics;

public class BirdPlayerMovement : Animal
{

    const int SPEED = 6;
    const int JUMP_SPEED_Y = 4;

    private bool isPickupAnything = false;
    private GameObject collideObject = null;
    private GameObject pickupObject = null;
    private Vector3 grabOffset;
    private Color originalState;
    private Color newState;
    private bool isBirdBurst = false;
    private new Renderer renderer;

    //start time record when the bird's color is bright
    private float startTime;
    private float brightMaintainTime = 15.0f;

    private Animator anim;
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
        renderer = GetComponent<Renderer>();
        //Debug.Log(currentSceneIndex);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        //if (!gameObject.activeSelf)
        //    return;

        if (!isActivated)
            return;

        undisplayArrow();
        // if (!firstTry)
        /*         if (currentSceneIndex > 0)
                    unshowTutorialText(); */
        moveX(SPEED);
        // moveY(SPEED);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            fly(JUMP_SPEED_Y);

            // int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
            int levelNumber = HandleScene.LevelNumber();
            if (levelNumber > 0)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "levelNumber", "Level " + levelNumber.ToString() }
            };

                AnalyticsService.Instance.CustomData("birdFlyEvent", parameters);
                AnalyticsService.Instance.Flush();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            fall(JUMP_SPEED_Y);
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

        if (Input.GetKeyDown(skillKey))
        {
            /*
             * collideObject: only valid when there is collision otherwise it will be null
             * isPickupAnything: bool if true means something being picked up and reference caught by pickupObject
             * set the parent of pickupObject makes bird and object a whole
             */
            //Debug.Log("isPickingupAnything: " + isPickupAnything);

            if (isPickupAnything)
            {
                pickupObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                // pickupObject.transform.SetParent(null);
                // pickupObject.GetComponent<Rigidbody2D>().isKinematic = false;
                pickupObject = null;
                isPickupAnything = false;

                //collect skill used event
                Analytics.SkillUsedEvent("Bird");
            }

            else
            {
                pickup();
            }

            //firstTry = false;
        }

        if (isPickupAnything)
        {
            pickupObject.transform.position = transform.position + grabOffset;
        }


        // when bird eat the apple and burst, it will be bright color and can grab dog temporarily
        if (isBirdBurst)
        {
            float elapsedTime = Time.time - startTime;


            if (elapsedTime < brightMaintainTime) renderer.material.color = newState;


            else
            {
                renderer.material.color = originalState;

                // if burst time is over, if the bird still grab dog, then bird will automatically release the dog
                if (isPickupAnything && pickupObject.CompareTag("Player"))
                {
                    pickupObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    pickupObject = null;
                    isPickupAnything = false;
                }

            }
        }

        setAnimation();

    }

    private void pickup()
    {
        if (collideObject != null)
        {
            pickupObject = collideObject;
            // pickupObject.transform.SetParent(this.transform);
            grabOffset = pickupObject.transform.position - transform.position;
            // pickupObject.GetComponent<Rigidbody2D>().isKinematic = true;
            //Physics.IgnoreCollision(this.gameObject.AddComponent<Collider>(), collideObject.GetComponent<Collider>());
            // collideObject = null;
            isPickupAnything = true;
            //Debug.Log(dirY);

            //collect skill used event
            Analytics.SkillUsedEvent("Bird");
            // Ready to play the anim of picking up
            anim.SetTrigger("shiftTrigger");
        }
    }

    private void fly(float speed)
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    private void fall(float speed)
    {
        rb.velocity = new Vector2(rb.velocity.x, -speed);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        /*
            if (collision.gameObject.name == "Window")
            {
                Collider2D colliderComponent = collision.gameObject.GetComponent<Collider2D>();
                if (colliderComponent != null) Destroy(colliderComponent);
                LevelWinManager.BirdTouchGate();
                if (firstWin)
                {
                    showTutorialText("Bird reaches the Window!\nPress [Enter] to continue!");
                    firstWin = false;
                }
            }
        */

        if (collision.gameObject.CompareTag("canGrab") || collision.gameObject.CompareTag("canCrunch") || (collision.gameObject.CompareTag("Player") && isBirdBurst))
        {
            //Debug.Log("try picking");
            if (Mathf.Abs(collision.collider.bounds.center.y - transform.position.y) > (collision.collider.bounds.size.y / 2)) //is above
            {
                collideObject = collision.gameObject;
            }
            /*
                //if trying to pick up the item for the first time, show tutorial text
                if (firstTry)
                {
                    showTutorialText("Use [Z] to grab the item!\nPress again to Release.\nPress [Enter] to continue!");
                    firstTry = false;
                }
            */
        }

        if (collision.gameObject.CompareTag("canEat"))
        {
            collision.gameObject.SetActive(false);


            isBirdBurst = true;

            startTime = Time.time;

            originalState = renderer.material.color;
            newState = renderer.material.color * 1.8f;

        }


    }

    /*
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("canGrab"))
            {
                //Debug.Log("try picking");
                collideObject = collision.gameObject;
                //if trying to pick up the item for the first time, show tutorial text
                if (firstTry)
                {
                    showTutorialText("Use [Z] to grab the item\nPress again to Release\nPress [Enter] to continue!");
                    firstTry = false;
                }
            }
        }
    */

    public void setAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("gameobject name: " + collision.gameObject.name);

        if (collideObject != null)
        {
            //Debug.Log("collideObject: " + collideObject.name);
            if (collision.gameObject.name == collideObject.name)
            {
                collideObject = null;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.name == "Window")
        {
            LevelWinManager.BirdTouchGate();
            if (firstWin)
            {
                showTutorialText("Bird reaches the Window!\nPress [Enter] to continue!");
                firstWin = false;
            }
        }
    }

    protected override void CheckInWater()
    {
        base.CheckInWater();
    }








    public bool getIsPickupAnything()
    {
        return isPickupAnything;
    }
    public void setIsPickupAnything(bool boolValue)
    {
        isPickupAnything = boolValue;
    }

    public GameObject getPickupObject()
    {
        return pickupObject;
    }

    public void setPickupObject(GameObject obj)
    {
        pickupObject = obj;
    }

}