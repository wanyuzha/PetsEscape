using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleController : MonoBehaviour
{
    public Renderer rend;
    Rigidbody2D rb;

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
}
