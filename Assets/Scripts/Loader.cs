using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static string targetScene;

    public static void Load(string scene)
    {
        SceneManager.LoadScene("LoadingScene");

        targetScene = scene;
    }

    public static void LoadTargetScene() {
        SceneManager.LoadScene(targetScene);
    }
}
