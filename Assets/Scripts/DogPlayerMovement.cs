using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlayerMovement : MonoBehaviour
{
    public bool isActiviated = false;
    public GameObject birdObject;
    public GameObject fishObject;
    List<string> items = new List<string>();
    private bool isJumping = false;
    private float previousHeight;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        previousHeight = transform.position.y;
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

        if (!isActiviated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(dirX * 7, rb.velocity.y, 0);

        if (Input.GetKey("space") && !isJumping)
        {   
            rb.velocity = new Vector3(0, 4, 0);
            Debug.Log("jumping");

            //防止卡在砖块的垂直边界上，所以暂时用这个条件语句，只有跳起一定高度才让isJumping = true
            if(transform.position.y - previousHeight>0.8)
            isJumping = true;
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
            previousHeight = transform.position.y;
        }

    }
}

