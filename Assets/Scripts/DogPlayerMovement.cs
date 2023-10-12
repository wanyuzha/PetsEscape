using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DogPlayerMovement : MonoBehaviour
{
    public bool isActivated = false;
    List<string> items = new List<string>();
    private bool isJumping = false;
    private bool firstTry = false;
    // private float previousHeight;

    private bool inWater = false;

    public GameObject panel;
    public GameObject door;
    public GameObject tutorialText;
    public Text textComponent; 

    Rigidbody2D rb;

    private GameObject collideObject;
    public List<string> targetName = new List<string>();

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
        // previousHeight = transform.position.y;
        InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);

        targetName.Add("target_pickup_for_test");
        targetName.Add("Chair");
        targetName.Add("obstacle");

    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        if (dirX > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1); 
        }
        rb.velocity = new Vector2(dirX * 6, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 6);
            Debug.Log("dog jumping");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            crunch();
        }
    }

    void crunch()
    {
        if (collideObject != null)
        {
            if (Vector2.Distance(collideObject.transform.position, transform.position) < 2){
                Destroy(collideObject);
                collideObject = null;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        
        if(collision.gameObject.name == "DetectJump")
        {
            Collider2D colliderComponent = collision.gameObject.GetComponent<Collider2D>();
            if(colliderComponent!=null)
            {
                Destroy(colliderComponent);
            }
            showTutorialText("Use Space to jump");
        }
        if (collision.gameObject.name == "key")
        {
            items.Add("key");
            //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(currentSceneIndex + 1);
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("dog on ground");
            isJumping = false;
            // previousHeight = transform.position.y;
        }
        if (targetName.Contains(collision.gameObject.name))
        {
            Debug.Log("try biting");
            collideObject = collision.gameObject;
            //if trying to pick up the item for the first time, show tutorial text
            if (firstTry)
            {
                showTutorialText("Press Z to crunch an item nearby");
                firstTry = false;
            }

        }

    }

/*     void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("dog leave water");
            inWater = false;
        }

    } */

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name == "door")
        {   
            Debug.Log("Dog entered Door");
            Collider2D colliderComponent = coll.gameObject.GetComponent<Collider2D>();
            if(colliderComponent!=null)
            {
                Destroy(colliderComponent);
            }
            showTutorialText("Use key to open the door\npress any key to continue");
        }
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("dog enter water");
            inWater = true;
        }

    }

    void CheckInWater()
    {
        if (inWater)
        {
            EndGame("Dog died");
        }
    }

    void showTutorialText(string str)
    {       
        TMP_Text myText = tutorialText.GetComponentInChildren<TMP_Text>();
        myText.text=str;
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