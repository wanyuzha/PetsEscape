using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    public GameObject bird;
    public GameObject dog;
    public GameObject fish;

    private BirdPlayerMovement birdScript;
    private DogPlayerMovement dogScript;
    private FishPlayerMovement fishScript;
    private List<GameObject> arrows = new List<GameObject>();

    private int currentCharacterIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        birdScript = bird.GetComponent<BirdPlayerMovement>();
        dogScript = dog.GetComponent<DogPlayerMovement>();
        fishScript = fish.GetComponent<FishPlayerMovement>();
        birdScript.isActiviated = true;
        arrows.Add(bird.transform.Find("redarrow").gameObject);
        dogScript.isActiviated = false;
        arrows.Add(dog.transform.Find("redarrow").gameObject);
        fishScript.isActiviated = false;
        arrows.Add(fish.transform.Find("redarrow").gameObject);

        arrows[0].SetActive(true);
        arrows[1].SetActive(false);
        arrows[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // cancel the activation
            if (currentCharacterIndex == 0)
            {
                birdScript.isActiviated = false;
            }
            else if (currentCharacterIndex == 1)
            {
                dogScript.isActiviated = false;
            }
            else if (currentCharacterIndex == 2)
            {
                fishScript.isActiviated = false;
            }
            arrows[currentCharacterIndex].SetActive(false);
            // update the activation
            Debug.Log("current index is "+currentCharacterIndex);
            currentCharacterIndex = (currentCharacterIndex + 1) % 3;
            if(currentCharacterIndex == 0)
            {
                birdScript.isActiviated = true;
            }
            else if(currentCharacterIndex == 1)
            {
                dogScript.isActiviated = true;
            }
            else if(currentCharacterIndex == 2)
            {
                fishScript.isActiviated = true;
            }
            arrows[currentCharacterIndex].SetActive(true);
        }
    }
}
