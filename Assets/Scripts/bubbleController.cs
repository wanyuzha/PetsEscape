using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BubbleController : MonoBehaviour
{
    // private Renderer rend;
    Rigidbody2D rb;

    public float waterHeight;
    public GameObject waterArea;
    const float HEIGHT_ABOVE_WATER = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        // rend = GetComponent<Renderer>();
        // rend.enabled = false;
        gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        waterHeight = waterArea.GetComponent<DynamicWater2D>().curHeight + waterArea.GetComponent<DynamicWater2D>().bound.bottom + waterArea.transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            rb.velocity = Vector2.zero;
            if (transform.position.y < HEIGHT_ABOVE_WATER + waterHeight)
            {
                rb.velocity = Vector2.up;
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
    }
}
