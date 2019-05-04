using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    public GameObject LoseScreen;
    public GameObject WinScreen;

    public static void Win()
    {
        instance.WinScreen.SetActive(true);
    }

    public static void Lose()
    {
        instance.LoseScreen.SetActive(true);
    }

    public void Replay()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
