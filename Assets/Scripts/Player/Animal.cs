using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
// using UnityEngine.SceneManagement;
using TMPro;

public class Animal : MonoBehaviour
{
    protected string AnimalName;
    // protected int direction = 1;
    protected int health;
    public int initialHealth = 10;

    public bool isActivated = false;
    public GameObject panel;
    public GameObject tutorialText;
    public Text textComponent;

    protected bool inWater;
    // protected bool canJump = true;
    protected bool isJumping = false;
    protected bool leaveGround = false;
    protected float jumpStartY;
    protected const float JUMP_STEP_THRESHOLD = 0.1f;

    protected Rigidbody2D rb;
    private SpriteRenderer sr;
    private Color originColor;
    // protected List<string> targetName;
    protected List<string> items;

    // protected bool firstTry;
    protected bool firstWin;
    // protected int currentSceneIndex;

    protected UnityEngine.KeyCode skillKey = KeyCode.LeftShift;

    // public GameObject laser = null;

    // private string jumpFrom;

    // protected float tutorialShowTime = 0.0f;

    public Animal()
    {
        // firstTry = false;
        firstWin = true;
        // targetName = new List<string>();
        items = new List<string>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        /*
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (currentSceneIndex == 1)
            {
                firstTry = true;
            }
        */
        // Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
        tutorialText.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        originColor = sr.color;
        health = initialHealth;
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

    protected virtual void jump(float speed)
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, speed);
            //Debug.Log(string.Concat(AnimalName, isJumping));
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.CompareTag("ground") || coll.gameObject.CompareTag("canCrunch"))
        {
            //Debug.Log(string.Concat(AnimalName, " touches ground"));
            isJumping = false;
            // previousHeight = transform.position.y;
            jumpStartY = transform.position.y;
        }

    }

    // use for dog jump detection
    protected virtual void OnCollisionExit2D(Collision2D coll)
    {
        // jumpFrom = coll.gameObject.tag;
        if (coll.gameObject.CompareTag("ground") || coll.gameObject.CompareTag("canCrunch"))
        {
            isJumping = true;
            leaveGround = true;
        }
    }

    protected void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("ground") || coll.gameObject.CompareTag("canCrunch"))
        {
            leaveGround = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("water"))
        {
            //Debug.Log(string.Concat(AnimalName, "enter water"));
            inWater = true;
        }
        else if (coll.gameObject.CompareTag("Laser"))
        {
            // touch the wire
            // Destroy(gameObject);
            Damage(10);
            EndGame(AnimalName + " died of laser!");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log(coll.gameObject.name);
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

    protected void Damage(int hp)
    {
        health -= hp;
        //red flash
        sr.color = Color.red;
        Invoke("setOriginColor", 0.2f);
    }

    private void setOriginColor()
    {
        sr.color = originColor;
    }

    protected void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
