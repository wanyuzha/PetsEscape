using UnityEngine;
using UnityEngine.SceneManagement;

public static class HandleScene
{
    private static int LevelCount = 3;

    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextLevel()
    {
        Debug.Log("load next level");
        if (SceneManager.GetActiveScene().buildIndex == LevelCount - 1) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadPrevLevel()
    {
        Debug.Log("load prev level");
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static bool isMaxLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == LevelCount - 1;
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
