using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlayerMovement : MonoBehaviour
{
    public bool isActiviated = false;
    public GameObject birdObject;
    public GameObject fishObject;
    List<string> items = new List<string>();

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKey("space"))
        {
            rb.velocity = new Vector3(0, 3, 0);
            Debug.Log("jumping");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "key")
        {
            items.Add("key");
        }
    }
}

