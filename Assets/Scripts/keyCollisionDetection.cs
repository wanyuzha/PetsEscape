using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollisionDetection : MonoBehaviour
{
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
        Debug.Log("hitting");
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.name == "dog")
        {
            // being picked up by dog
            Destroy(gameObject);
        }
    }
}
