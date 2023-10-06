using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayerMovement : MonoBehaviour
{
    public bool isActiviated;
    public GameObject dogObject;
    public GameObject fishObject;
    

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            isActiviated = true;
            dogObject.GetComponent<DogPlayerMovement>().isActiviated = false;
            fishObject.GetComponent<FishPlayerMovement>().isActiviated = false;
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Triggered");
        if(other.gameObject.name == "window")
        {
            Debug.Log("Name is window");
            WinCondition.BirdWin();
        }
    }
}
