using UnityEngine;
using UnityEngine.SceneManagement;

public static class HandleScene
{
    private static int levelCount = 3;
    private static int firstLevel = 5;

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        // Debug.Log("load next level");
        if (LevelNumber() < levelCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public static void LoadPrevLevel()
    {
        // Debug.Log("load prev level");
        if (LevelNumber() > 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    /*
        public static void LoadFirstNonTutorialLevel()
        {
            SceneManager.LoadScene(firstLevel);
        }
    */

    public static bool isMaxLevel()
    {
        return LevelNumber() == levelCount;
    }

    public static bool isFirstLevel()
    {
        return LevelNumber() == 1;
    }

    public static int LevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex - firstLevel + 1;
    }

    public static void LoadLevelNumber(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber + firstLevel - 1);
    }

    public static GameObject FindSiblingGameObject(string name)
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }

        return null;
    }
}
