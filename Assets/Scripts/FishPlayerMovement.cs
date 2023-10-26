using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishPlayerMovement : Animal
{
    public int health;
    const int initialHealth = 5;

    const int SPEED_IN_WATER = 5;
    const int SPEED_ON_GROUND = 1;
    const int SPEED_JUMPING_Y = 3;
    const int SPEED_JUMPING_X = 1;
    const float gravityScaleInWater = 0f;
    const float gravityScaleOutWater = 1f;
    const float gravityScaleStep = 0.02f;

    public GameObject bubble;

    public FishPlayerMovement()
    {
        AnimalName = "Fish";
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = initialHealth;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (health <= 0)
        {
            EndGame("Fish died in lack of water!");
        }

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
            isJumping = false;

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

        if (Input.GetKeyDown(KeyCode.Space) && !inWater)
        {
            FishJump(SPEED_JUMPING_Y);
        }

        if (inWater && Input.GetKeyDown(skillKey))
        {
            bubble.SetActive(true);
            bubble.transform.position = transform.position + new Vector3(0, 0.5f, 0);

            //collect skill used event
            Analytics.SkillUsedEvent();
        }
    }

    protected override void OnTriggerExit2D(Collider2D coll)
    {
        base.OnTriggerExit2D(coll);
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);

        if (coll.gameObject.name == "FishGoal")
        {
            LevelWinManager.FishTouchGoal();
            if (firstWin)
            {
                showTutorialText("Fish reaches the end!\nPress [Enter] to continue!");
                firstWin = false;
            }
        }

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
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }


    protected override void CheckInWater()
    {
        if (!inWater)
        {
            health--;
        }
    }
}
