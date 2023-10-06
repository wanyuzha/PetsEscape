using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleCollisionDetection : MonoBehaviour
{
    public GameObject bird;
    public GameObject dog;
    public GameObject fish;

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
        if (collision.gameObject.name == "bird")
        {
            bird.GetComponent<BirdPlayerMovement>().health++;
            Destroy(gameObject);
            Debug.Log("bird: " + bird.GetComponent<BirdPlayerMovement>().health);
        }
        if (collision.gameObject.name == "dog")
        {
            dog.GetComponent<DogPlayerMovement>().health++;
            Destroy(gameObject);
            Debug.Log("dog: " + dog.GetComponent<DogPlayerMovement>().health);
        }
        if (collision.gameObject.name == "fish")
        {
            fish.GetComponent<FishPlayerMovement>().health++;
            Destroy(gameObject);
            Debug.Log("fish: " + fish.GetComponent<FishPlayerMovement>().health);
        }
    }
}
