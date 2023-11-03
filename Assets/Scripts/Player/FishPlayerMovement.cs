using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class FishPlayerMovement : Animal
{
    const int SPEED_IN_WATER = 5;
    const int SPEED_ON_GROUND = 3;
    const int SPEED_JUMPING_Y = 7;
    const int SPEED_JUMPING_X = 5;
    const float gravityScaleInWater = 0f;
    const float gravityScaleOutWater = 1f;
    const float gravityScaleStep = 0.005f;

    public float scaleX = 1;
    public float scaleY = 1;

    public GameObject bubble;

    private Animator anim;
    private Vector3 initialScale;

    public FishPlayerMovement()
    {
        AnimalName = "Fish";
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        initialScale =transform.localScale;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (health <= 0)
        {
            EndGame("Fish stranded!");
        }

        // we want to see the anim no matter it is activated or not
        Vector3 scale = initialScale;
        if(transform.localScale.x < 0)
        {
            scale.x *= -scaleX;
        }
        else
        {
            scale.x *= scaleX;
        }
        scale.y *= scaleY;
        transform.localScale = scale;

        if (!isActivated)
            return;

        undisplayArrow();

        /*
            float dirX = Input.GetAxis("Horizontal");
            float dirY = Input.GetAxis("Vertical");

            if (dirX > 0)
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
            else if (dirX < 0)
            {
                transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            }
        */

        if (inWater)
        {
            health = initialHealth;

            if (rb.gravityScale > gravityScaleInWater)
            {
                rb.gravityScale -= gravityScaleStep;
                moveX(SPEED_IN_WATER);
            }
            else
            {
                rb.gravityScale = gravityScaleInWater;
                moveX(SPEED_IN_WATER);
                moveY(SPEED_IN_WATER);
                isJumping = false;
            }
        }

        else
        {
            rb.gravityScale = gravityScaleOutWater;

            if (isJumping)
            {
                moveX(SPEED_JUMPING_X);
            }
            else
            {
                moveX(SPEED_ON_GROUND);
            }
        }

        // TODO: Fish can jump whenever it's close to the water surface, while dog cannot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump(SPEED_JUMPING_Y);
            rb.gravityScale = gravityScaleOutWater;
        }

        if (inWater && Input.GetKeyDown(skillKey))
        {
            bubble.SetActive(false);
            bubble.transform.position = transform.position + new Vector3(0, scale.y*1.5f, 0);
            bubble.SetActive(true);

            //collect skill used event
            Analytics.SkillUsedEvent("Fish");
        }

        setAnimation();
    }

    protected override void OnTriggerExit2D(Collider2D coll)
    {
        base.OnTriggerExit2D(coll);
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.CompareTag("water"))
        {
            //Debug.Log(string.Concat(AnimalName, " leave water"));
            isJumping = true;
            rb.gravityScale = gravityScaleOutWater;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);
        if (coll.gameObject.CompareTag("water"))
        {
            //Debug.Log(string.Concat(AnimalName, " leave water"));
            isJumping = false;
        }
        if (coll.gameObject.name == "FishGoal")
        {
            LevelWinManager.FishTouchGoal();
            if (firstWin)
            {
                showTutorialText("Fish reaches the end!\nPress [Enter] to continue!");
                firstWin = false;
            }
        }
        /*
            else if (coll.gameObject.name == "DetectFish")
            {
                Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();
                // Debug.Log(colliderComponent);

                if (firstTry)
                {
                    showTutorialText("Use [Z] to bubble!\nPress [Enter] to continue!");
                    firstTry = false;
                }

                if (colliderComponent != null)
                {
                    Destroy(colliderComponent);
                }
            }
        */
    }

    public void setAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void OnCollisionExit2D(Collision2D coll)
    {
        base.OnCollisionExit2D(coll);
    }

    protected override void CheckInWater()
    {
        if (!inWater)
        {
            Damage(1);
        }
    }
}
