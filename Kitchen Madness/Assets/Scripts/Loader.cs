using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader  
{
    public enum Scene
    {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }
    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene( Scene.LoadingScene.ToString());
    }

    public static void LoadGameScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
