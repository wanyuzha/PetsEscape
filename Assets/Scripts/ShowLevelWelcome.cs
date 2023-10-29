using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowLevelWelcome : MonoBehaviour
{
    private bool gameStart;
    public TextMeshProUGUI level;
    public TextMeshProUGUI hint;

    // Start is called before the first frame update
    void Start()
    {
        level.text = "Level " + HandleScene.LevelNumber();
        if (!HandleScene.isFirstLevel())
        {
            hint.text = "Press [Enter] to continue!";
        }
        gameObject.SetActive(true);
        Time.timeScale = 0;
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                gameObject.SetActive(false);
                Time.timeScale = 1;
                gameStart = false;
            }
        }
    }
}
