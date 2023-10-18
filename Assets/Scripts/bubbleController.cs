using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleController : MonoBehaviour
{
    public Renderer rend;
    Rigidbody2D rb;

    Vector2 posLeaveWater;
    const float HEIGHT_ABOVE_WATER = 2.0f;
    bool inWater = true;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rend.enabled)
        {
            rb.velocity = Vector2.zero;
            if (inWater || transform.position.y < HEIGHT_ABOVE_WATER+ posLeaveWater.y)
                rb.velocity = Vector2.up;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            rb.velocity = Vector2.zero;
            rend.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Water"))
        {
            posLeaveWater = transform.position;
            inWater = false;
        }
    }
}
