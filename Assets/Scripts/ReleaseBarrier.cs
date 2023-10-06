using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseBarrier : MonoBehaviour
{   
    
    public GameObject Block;
    public float moveDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touched");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.name == "dog")
        {
            //release the barrier in drainage
            MoveBlockUp();
        }
    }
    
    private void MoveBlockUp()
    {
        if (Block != null)
        {
            Vector3 targetPosition = Block.transform.position + Vector3.up * moveDistance;
            Block.transform.position = Vector3.Lerp(Block.transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
