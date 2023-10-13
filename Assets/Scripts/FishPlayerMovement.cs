using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishPlayerMovement : Animal
{
    public int health = 5;

    const int SPEED_IN_WATER = 5;
    const int SPEED_ON_GROUND = 1;
    const int SPEED_JUMPING_Y = 5;
    const int SPEED_JUMPING_X = 3;

    public GameObject bubble;

    public FishPlayerMovement()
    {
        AnimalName = "Fish";
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            EndGame("Fish died!");
        }

        if (!isActivated)
            return;

        undisplayArrow();

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

        if (inWater)
        {
            if (rb.gravityScale > 0)
            {
                rb.gravityScale -= 0.01f;
                moveX(SPEED_IN_WATER);
            }
            else
            {
                moveX(SPEED_IN_WATER);
                moveY(SPEED_IN_WATER);
            }
        }
        else
        {
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
            jump(SPEED_JUMPING_Y);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (inWater)
            {
                bubble.GetComponent<Renderer>().enabled = true;
                bubble.transform.position = transform.position;
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D coll)
    {
        base.OnTriggerExit2D(coll);
    }

    protected override void OnTriggerEnter2D(Collider2D coll)
    {
        base.OnTriggerEnter2D(coll);
        if (coll.name.StartsWith("Water"))
        {
            health = 10;
            isJumping = false;
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