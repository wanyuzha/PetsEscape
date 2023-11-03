using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BubbleController : MonoBehaviour
{
    // private Renderer rend;
    Rigidbody2D rb;

    public float waterHeight;
    // public GameObject waterArea;
    const float HEIGHT_ABOVE_WATER = 2f;
    // private float mass = 0;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        // rend = GetComponent<Renderer>();
        // rend.enabled = false;
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        scale = transform.localScale.y / 4f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (transform.position.y < scale * HEIGHT_ABOVE_WATER + waterHeight)
            {
                rb.velocity = Vector2.up * scale;
                // rb.AddForce(Vector3.up * mass, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
        /*
            else
            {
                Rigidbody2D collideObjectRB = collision.collider.GetComponent<Rigidbody2D>();
                mass = collideObjectRB.mass;
                //Debug.Log(mass);
            }
        */
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "canCrunch")
        {
            float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            // Debug.Log(mass);
            rb.AddForce(10f * Vector2.up * mass, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "water")
        {
            waterHeight = collision.gameObject.GetComponent<DynamicWater2D>().bound.top + collision.gameObject.transform.position.y;
        }
    }
}
