using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashControl : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "water")
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
