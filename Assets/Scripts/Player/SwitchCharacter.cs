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
        birdScript.isActivated = true;
        arrows.Add(bird.transform.Find("redarrow").gameObject);
        dogScript.isActivated = false;
        arrows.Add(dog.transform.Find("redarrow").gameObject);
        fishScript.isActivated = false;
        arrows.Add(fish.transform.Find("redarrow").gameObject);

        arrows[0].SetActive(true);
        arrows[1].SetActive(false);
        arrows[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // cancel the activation
            if (currentCharacterIndex == 0)
            {
                birdScript.isActivated = false;
            }
            else if (currentCharacterIndex == 1)
            {
                dogScript.isActivated = false;
            }
            else if (currentCharacterIndex == 2)
            {
                fishScript.isActivated = false;
            }
            arrows[currentCharacterIndex].SetActive(false);
            // update the activation
            //Debug.Log("current index is " + currentCharacterIndex);
            currentCharacterIndex = (currentCharacterIndex + 1) % 3;
            if (currentCharacterIndex == 0)
            {
                birdScript.isActivated = true;
                // collect switch anmial event
                Analytics.AnimalSwitchEvent("Bird");
            }
            else if (currentCharacterIndex == 1)
            {
                dogScript.isActivated = true;
                Analytics.AnimalSwitchEvent("Dog");
            }
            else if (currentCharacterIndex == 2)
            {
                fishScript.isActivated = true;
                Analytics.AnimalSwitchEvent("Fish");
            }
            arrows[currentCharacterIndex].SetActive(true);
        }
    }
}
