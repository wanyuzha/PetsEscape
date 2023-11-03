using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine.UI;
// using UnityEngine.SceneManagement;
using TMPro;

public class ShowTutorial : MonoBehaviour
{
    public TextMeshProUGUI content;
    // public GameObject tutorialText;
    // public GameObject level;
    // public GameObject hint;
    // Start is called before the first frame update
    void Start()
    {
        // level.GetComponentInChildren<TMP_Text>().text = string.Concat("Level ", SceneManager.GetActiveScene().buildIndex + 1);
        // if (SceneManager.GetActiveScene().buildIndex > 0)
        // {
        // hint.GetComponentInChildren<TMP_Text>().text = "Press [Enter] to continue!";
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // hint.GetComponentInChildren<TMP_Text>().text = "";
                gameObject.SetActive(false);
                HandleScene.ResumeGame();
            }
        }
    }
}
