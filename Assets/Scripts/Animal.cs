using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Animal : MonoBehaviour
{
    protected string AnimalName;
    // protected int direction = 1;

    public bool isActivated = false;
    public GameObject panel;
    public GameObject tutorialText;
    public Text textComponent;

    protected bool inWater;
    // protected bool canJump = true;
    protected bool isJumping = false;

    protected Rigidbody2D rb;
    // protected List<string> targetName;
    protected List<string> items;

    protected bool firstTry;
    protected bool firstWin;
    protected int currentSceneIndex;

    protected UnityEngine.KeyCode skillKey = KeyCode.Z;

    // protected float tutorialShowTime = 0.0f;

    public Animal()
    {
        firstTry = false;
        firstWin = true;
        // targetName = new List<string>();
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
        // Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
        tutorialText.SetActive(false);
    }

    protected void undisplayArrow()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.Find("redarrow").gameObject.SetActive(false);
        }
    }

    protected void moveX(float speed)
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        // make the character's sprite direction same as motion
        if (dirX * transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
        }
    }

    protected void moveY(float speed)
    {
        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(rb.velocity.x, dirY * speed);
    }

    protected void jump(float speed)
    {
        // if (canJump && !isJumping)
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, speed);
            //Debug.Log(string.Concat(AnimalName, " jumping"));
        }
    }

    protected void fly(float speed)
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.CompareTag("ground") || coll.gameObject.CompareTag("canCrunch"))
        {
            //Debug.Log(string.Concat(AnimalName, " on ground"));
            isJumping = false;
            // previousHeight = transform.position.y;
        }
        if (coll.gameObject.name == "LightSaber")
        {
            // touch the wire
            // Destroy(gameObject);
            EndGame(AnimalName + " died of laser!");

            /* 
             * parameter for event is unnecessary currently
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
            };
            */

            // the following code sends the bird Dead Event to the unity analytics account

            // AnalyticsService.Instance.CustomData("birdDeadEvent");
            // AnalyticsService.Instance.Flush();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        /*
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.name == "DetectJump")
        {
            Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();
            Debug.Log(colliderComponent);
            if (colliderComponent != null) Destroy(colliderComponent);
            showTutorialText("Use Space to jump!\nPress [Enter] to continue");
        }
        else 
        */
        if (coll.gameObject.CompareTag("water"))
        {
            //Debug.Log(string.Concat(AnimalName, "enter water"));
            inWater = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        /*
        if (coll.gameObject.name == "DetectJump")
        {
            tutorialText.SetActive(false);
        }
        */

        if (coll.gameObject.CompareTag("water"))
        {
            //Debug.Log(string.Concat(AnimalName, " leave water"));
            inWater = false;
        }
    }

    protected virtual void CheckInWater()
    {
        if (inWater)
        {
            EndGame(AnimalName + " died of water!");
        }
    }

    protected void showTutorialText(string str)
    {
        tutorialText.GetComponentInChildren<TMP_Text>().text = str;
        tutorialText.SetActive(true);
        Time.timeScale = 0;
        // if(tutorialShowTime == 0.0f)
        // {
        // tutorialShowTime = Time.time;
        // }
    }

    /*
        protected void unshowTutorialText()
        {
            float deltaTime = Time.time - tutorialShowTime;
            if (deltaTime > 1f && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(skillKey) || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                tutorialText.gameObject.transform.parent.SetActive(false);
                // tutorialShowTime = 0.0f;
            }
        }
    */

    protected void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
