using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public GameObject skipTutorialCanvas;

    // Start is called before the first frame update
    void Start()
    {
        skipTutorialCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AskSkipTutorial()
    {
        skipTutorialCanvas.SetActive(true);
    }
}
