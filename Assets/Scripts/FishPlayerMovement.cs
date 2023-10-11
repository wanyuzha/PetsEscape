using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishPlayerMovement : MonoBehaviour
{
    public bool isActivated = false;

    private bool inWater = true;
    private bool isJumping = false;
    public int health = 5;

    const int SPEED_IN_WATER = 5;
    const int SPEED_ON_GROUND = 1;
    const int SPEED_JUMPING_Y = 5;
    const int SPEED_JUMPING_X = 3;

    public GameObject bubble;

    public GameObject panel;
    public Text textComponent;
    public Button rsButton;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
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

        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");
        if (inWater)
        {
            if (rb.gravityScale > 0)
            {
                rb.gravityScale -= 0.01f;
                rb.velocity = new Vector3(dirX * SPEED_IN_WATER, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(dirX * SPEED_IN_WATER, dirY * SPEED_IN_WATER, 0);
            }
        } else
        {
            if (isJumping)
            {
                rb.velocity = new Vector3(dirX * SPEED_JUMPING_X, rb.velocity.y, 0);
            }
            else
            {
                rb.velocity = new Vector3(dirX * SPEED_ON_GROUND, rb.velocity.y, 0);
            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !inWater && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, SPEED_JUMPING_Y);
            Debug.Log("fish jumping");
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

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            //Debug.Log("fish leave water");
            inWater = false;
            rb.gravityScale = 1;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("fish enter water");
            inWater = true;
            health = 10;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("fish on ground");
            isJumping = false;
        }
    }


    void CheckInWater()
    {
        if (!inWater)
        {
            health--;
        }
    }

    void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }

}