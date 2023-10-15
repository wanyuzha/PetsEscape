using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        

        //Debug.Log(collision.gameObject.name);

        // for level 1, bird just collide the key but not catch it
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        }


        if (collision.gameObject.name == "dog")
        {
            // being picked up by dog
            Destroy(gameObject);

        }
    }
}
