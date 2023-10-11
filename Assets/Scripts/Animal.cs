using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Animal : MonoBehaviour
{
    private string AnimalName;

    public bool isActivated;
    public GameObject panel;
    public GameObject tutorialText;
    public Text textComponent;

    private bool inWater;
    private bool canJump;
    private bool isJumping = false;

    private Rigidbody2D rb;
    private List<string> targetName;
    private List<string> items;

    private bool firstTry;

    public Animal() {
        firstTry = false;
        targetName = new List<string>();
        items = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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

    void moveX(float speed)
    {
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
    }

    void moveY(float speed)
    {
        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY * speed, 0);
    }

    void jump(float speed)
    {
        if (canJump && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, speed);
            Debug.Log(string.Concat(AnimalName, " jumping"));
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
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

    private void OnTriggerEnter2D(Collider2D coll)
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

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log(string.Concat(AnimalName, " leave water"));
            inWater = false;
            rb.gravityScale = 1;
        }

    }



    public virtual void CheckInWater() {
        if (inWater)
        {
            EndGame(string.Concat(AnimalName, " died of water!"));
        }
    }

    public virtual void UseSkill() { }

    void showTutorialText(string str)
    {
        TMP_Text myText = tutorialText.GetComponentInChildren<TMP_Text>();
        myText.text = str;
        tutorialText.SetActive(true);
        Debug.Log(myText);

    }

    void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }
}
