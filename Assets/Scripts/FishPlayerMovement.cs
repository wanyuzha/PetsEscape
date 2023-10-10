using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FishPlayerMovement : MonoBehaviour
{
    public bool isActivated = false;

    private bool inWater = true;
    public int health = 10;

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
        rb.velocity = new Vector3(dirX * 5, rb.velocity.y, 0);

        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bubble.GetComponent<Renderer>().enabled = true;
            bubble.transform.position = transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            //Debug.Log("fish leave water");
            inWater = false;
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