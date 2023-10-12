using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BirdPlayerMovement : MonoBehaviour
{
    public bool isActivated;
    public GameObject panel;
    public GameObject tutorialText;
    public Text textComponent;

    const int SPEED = 7;

    private Rigidbody2D rb;
    private List<string> targetName = new List<string>();
    private bool isPickupAnything = false;
    private GameObject collideObject;
    private GameObject pickupObject;
    private bool firstTry = true;
    private string currentLevel;

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
        //InvokeRepeating("CheckInWater", 1, 1);
        panel.SetActive(false);
        targetName.Add("target_pickup_for_test");
        targetName.Add("key");
        currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log(currentLevel);

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.gameObject.activeSelf)
            return;
        
        if (!isActivated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(dirX * SPEED, rb.velocity.y, 0);

        // make the character's sprite direction same as motion
        if (dirX > 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }

        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY * SPEED, 0);

        if(pickupObject!=null)
        {
            pickupObject.GetComponent<Rigidbody2D>().velocity = rb.velocity; 
        }
        /*
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, 7, 0);
            Debug.Log("jumping");
        }
        */
        
        // level 1 not trigger the pickup skill for bird
        if (Input.GetKeyDown(KeyCode.Z) && currentLevel!="Level 1")
        {
            /*
             * collideObject: only valid when there is collision otherwise it will be null
             * isPickupAnything: bool if true means something being picked up and reference caught by pickupObject
             * set the parent of pickupObject makes bird and object a whole
             */
            Debug.Log("isPickingupAnything: "+isPickupAnything);
            if (!isPickupAnything && collideObject != null)
            {
                collideObject.transform.SetParent(this.transform);
                pickupObject = collideObject;
                
                pickupObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //Physics.IgnoreCollision(this.gameObject.AddComponent<Collider>(), collideObject.GetComponent<Collider>());
                collideObject = null;
                isPickupAnything = true;
                Debug.Log(dirY);
            }
            else if(isPickupAnything)
            {
                pickupObject.transform.SetParent(null);
                pickupObject.GetComponent<Rigidbody2D>().isKinematic = false;
                pickupObject = null;
                isPickupAnything = false;
                
            }
        }
    }

/*    void OnTriggerExit2D(Collider2D coll)
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
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LightSaber")
        {
            // touch the wire
            Destroy(gameObject);
            EndGame("Bird died!");
        }
        else if (targetName.Contains(collision.gameObject.name))
        {   
            Debug.Log("try picking");
            collideObject = collision.gameObject;
            //if trying to pick up the item for the first time, show tutorial text
            if(firstTry && currentLevel != "Level 1")
            {
                TMP_Text myText = tutorialText.GetComponentInChildren<TMP_Text>();
                myText.text="Press Z to pick up the item";
                tutorialText.SetActive(true);
                firstTry=false;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("gameobject name: " + collision.gameObject.name);
        
        if(collideObject!=null)
        {
            Debug.Log("collideObject: "+ collideObject.name);
            if (collision.gameObject.name == collideObject.name)
            {   
                collideObject = null;
            }
        }

    }

 /*   void CheckInWater()
    {
        if (inWater)
        {
            health--;
        }
    }
    */

    void EndGame(string str)
    {
        textComponent.text = str;
        Time.timeScale = 0;
        panel.SetActive(true);
    }

}
