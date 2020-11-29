using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
    public static string currentLevel;

    public static void SetLevel(string level)
    {
        currentLevel = level;
    }

    public static void GoToScene()
    {
        SceneManager.LoadSceneAsync(currentLevel);
    }
}
