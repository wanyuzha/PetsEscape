using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            tutorialText.SetActive(false);
        }
    }
}
