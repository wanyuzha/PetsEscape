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
    public int JUMP_SPEED_Y = 8;

    private Animator anim;

    private Color originalState;
    private bool isBurst;
    private new Renderer renderer;
    //start time record when the bird's color is bright
    private float burstStartTime;
    private float burstMaintainTime = 10f;

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
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        originalState = renderer.material.color;
        isBurst = false;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (HandleScene.GetPauseStatus())
        {
            return;
        }

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
            jump(JUMP_SPEED_Y);
        }

        if (Input.GetKeyDown(skillKey))
        {
            crunch();
        }

        // when bird eat the apple and burst, it will be bright color and can grab dog temporarily
        if (isBurst)
        {
            if (Time.time - burstStartTime < burstMaintainTime)
            {
                renderer.material.color = originalState * 2f;
            }

            else
            {
                isBurst = false;
                renderer.material.color = originalState;
                if (powerupCanvas != null) powerupCanvas.SetActive(false);
                JUMP_SPEED_Y = 8;
            }
        }

        setAnimation();
    }

    protected override void jump(float speed)
    {
        if (isJumping && !leaveGround)
        {
            if (transform.position.y - jumpStartY < JUMP_STEP_THRESHOLD)
            {
                isJumping = false;
            }
        }
        // if (canJump && !isJumping)
        if (!isJumping)
        {
            // when jump from canCrunch item, use this, otherwise (jumping from ground) will use collide exit for isJumping = true
            // if(jumpFrom == "canCrunch") 
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, speed);

            //Debug.Log(string.Concat(AnimalName, isJumping));
        }
    }

    private void crunch()
    {
        if (collideObject != null)
        {
            //ToDo: Not very accurate, need to be modified
            if (Vector2.Distance(collideObject.transform.position, transform.position) < 5)
            { //is nearby
                if (Mathf.Abs(collideObject.transform.position.x - transform.position.x) > (collideObject.GetComponent<Renderer>().bounds.size.x / 2)) //not above
                {
                    if (collideObject.name == "notch")
                    {
                        collideObject.GetComponent<TankWater>().activated();
                    }

                    collideObject.SetActive(false);
                    collideObject = null;
                }
            }
            anim.SetTrigger("shiftTrigger");
            //collect skill used event
            Analytics.SkillUsedEvent("Dog");
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
        }

        /*if (collision.gameObject.name == "Notch")
        {
            Debug.Log("Yes it is");
            collideObject = collision.gameObject;
        }*/

        if (collision.gameObject.CompareTag("canEat"))
        {
            collision.gameObject.SetActive(false);
            JUMP_SPEED_Y = 12;
            isBurst = true;
            burstStartTime = Time.time;

            //show the powerup canvas (including powerup progress bar)
            if (powerupCanvas != null) powerupCanvas.SetActive(true);
            ProgressBarDog.ResetProgressBar();
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
            if (HandleScene.LevelNumber() < 0)
            {
                showTutorialText("Dog unlocks the Door!\nPress [Enter] to continue!");
            }
            else
            {
                if (flag != null)
                {
                    flag.SetActive(true);
                }
            }
        }
    }

    public void setAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
    }

    protected override void CheckInWater()
    {
        base.CheckInWater();
    }
}
