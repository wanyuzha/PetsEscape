using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishPlayerMovement : MonoBehaviour
{
    public bool isActiviated = false;
    public GameObject dogObject;
    public GameObject birdObject;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Triggered");
        if (other.gameObject.name == "FishGoal")
        {
            Debug.Log("Name is FishGoal");
            WinCondition.DogWin();
        }
    }



}

