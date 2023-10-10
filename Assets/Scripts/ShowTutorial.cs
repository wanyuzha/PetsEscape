using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorialText;
    // Start is called before the first frame update
    void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index==1){
            tutorialText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            tutorialText.SetActive(false);
        }
    }
}
