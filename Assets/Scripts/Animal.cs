using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Animal : MonoBehaviour
{
    protected string AnimalName;

    public bool isActivated;
    public GameObject panel;
    public GameObject tutorialText;
    public Text textComponent;

    protected bool inWater;
    protected bool canJump;
    protected bool isJumping = false;

    protected Rigidbody2D rb;
    protected List<string> targetName;
    protected List<string> items;

    protected bool firstTry;
    protected int currentSceneIndex;

    public Animal() {
        firstTry = false;
        targetName = new List<string>();
        items = new List<string>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
        {
            firstTry = true;
        }
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void moveX(float speed)
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        // make the character's sprite direction same as motion
        if (dirX > 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

    protected void moveY(float speed)
    {
        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY * speed, 0);
    }

    protected void jump(float speed)
    {
        if (canJump && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, speed);
            Debug.Log(string.Concat(AnimalName, " jumping"));
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "DetectJump")
        {
            Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();
            if (colliderComponent != null)
            {
                Destroy(colliderComponent);
            }
            showTutorialText("Use Space to jump");
        }
        if (coll.gameObject.CompareTag("ground"))
        {
            Debug.Log(string.Concat(AnimalName, " on ground"));
            isJumping = false;
            // previousHeight = transform.position.y;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log(string.Concat(AnimalName, "enter water"));
            inWater = true;
            isJumping = false;
        }
        if (coll.gameObject.name == "LightSaber")
        {
            // touch the wire
            Destroy(gameObject);
            EndGame(string.Concat(AnimalName, " died of laser!"));
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log(string.Concat(AnimalName, " leave water"));
            inWater = false;
            rb.gravityScale = 1;
        }

    }



    protected virtual void CheckInWater() {
        if (inWater)
        {
            EndGame(string.Concat(AnimalName, " died of water!"));
        }
    }

    protected void showTutorialText(string str)
    {
        TMP_Text myText = tutorialText.GetComponentInChildren<TMP_Text>();
        myText.text = str;
        tutorialText.SetActive(true);
        Debug.Log(myText);

    }

    protected void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
