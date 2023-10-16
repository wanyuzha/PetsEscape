using UnityEngine.SceneManagement;

public static class HandleScene
{
    private static int LevelCount = 2;
    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static bool isMaxLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == LevelCount - 1;
    }
}
