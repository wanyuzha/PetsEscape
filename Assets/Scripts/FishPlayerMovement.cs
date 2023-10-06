using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPlayerMovement : MonoBehaviour
{
    public bool isActiviated = false;
    public GameObject dogObject;
    public GameObject birdObject;

    private bool inWater = true;
    public int health = 10;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CheckInWater", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isActiviated = true;
            dogObject.GetComponent<DogPlayerMovement>().isActiviated = false;
            birdObject.GetComponent<BirdPlayerMovement>().isActiviated = false;
        }

        if (!isActiviated)
            return;

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(dirX * 7, rb.velocity.y, 0);

        float dirY = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, dirY, 0);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            Debug.Log("fish leave water");
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

}


