using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathMenu : MonoBehaviour
{
    public GameObject container;
    private bool paused = false;
    
    public void RestartButton()
    {
        paused = false;
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToDesktop()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void MainMenuButton()
    {
        paused = false;
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
