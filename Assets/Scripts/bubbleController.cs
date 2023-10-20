using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BubbleController : MonoBehaviour
{
    // private Renderer rend;
    Rigidbody2D rb;
    public Tilemap waterArea;

    Vector3 posLeaveWater;
    const float HEIGHT_ABOVE_WATER = 2.5f;
    bool inWater;
    private bool initialRecord;

    // Start is called before the first frame update
    void Start()
    {
        // rend = GetComponent<Renderer>();
        // rend.enabled = false;
        gameObject.SetActive(false);
        initialRecord = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (initialRecord)
            {
                posLeaveWater = transform.position;
                Vector3Int cellPosition = waterArea.WorldToCell(posLeaveWater);
                if (waterArea.HasTile(cellPosition))
                {
                    inWater = true;
                }
                else
                {
                    inWater = false;
                }
                initialRecord = false;
            }

            rb.velocity = Vector2.zero;
            if (inWater || transform.position.y < HEIGHT_ABOVE_WATER + posLeaveWater.y)
                rb.velocity = Vector2.up;
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

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("water"))
        {
            posLeaveWater = transform.position;
            inWater = false;
        }
    }
}
