using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdPlayerMovement : MonoBehaviour
{
    public bool isActiviated;

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
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            EndGame("Bird died!");
        }

        if (!this.gameObject.activeSelf)
            return;

        if (!isActiviated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(dirX * 7, rb.velocity.y, 0);

        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY * 7, 0);

        /*
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 7, 0);
            Debug.Log("jumping");
        }
        */
    }

    void OnTriggerExit2D(Collider2D coll)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LightSaber")
        {
            // touch the wire
            Destroy(gameObject);
            EndGame("Bird died!");
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
