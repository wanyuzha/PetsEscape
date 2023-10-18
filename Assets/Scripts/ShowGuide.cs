using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGuide : MonoBehaviour
{
    public GameObject tutorialText;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText.SetActive(false);
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            tutorialText.SetActive(false);
        }
    }

    void TaskOnClick()
    {
        tutorialText.SetActive(!tutorialText.activeInHierarchy);
    }
}
