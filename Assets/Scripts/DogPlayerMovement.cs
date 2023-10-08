using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DogPlayerMovement : MonoBehaviour
{
    public bool isActiviated = false;
    public GameObject birdObject;
    public GameObject fishObject;
    List<string> items = new List<string>();
    private bool isJumping = false;
    // private float previousHeight;

    private bool inWater = false;
    public int health = 10;

    public GameObject panel;
    public Text textComponent;
    public Button rsButton;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // previousHeight = transform.position.y;
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            isActiviated = true;
            birdObject.GetComponent<BirdPlayerMovement>().isActiviated = false;
            fishObject.GetComponent<FishPlayerMovement>().isActiviated = false;
        }

        if (health < 0)
        {
            EndGame("Dog died!");
        }

        if (!isActiviated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * 6, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 6);
            Debug.Log("jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "key")
        {
            items.Add("key");
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isJumping = false;
            // previousHeight = transform.position.y;
        }

    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("dog leave water");
            inWater = false;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("dog enter water");
            inWater = true;
            health = 10;
        }

    }

    void CheckInWater()
    {
        if (inWater)
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