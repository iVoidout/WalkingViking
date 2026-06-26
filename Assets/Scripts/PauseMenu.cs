using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    private bool paused = false;
    void Update()
    {
        Debug.Log(Input.GetKeyDown(KeyCode.Escape));

        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = true;
                AudioListener.volume = 0f;
                container.SetActive(true);
                Time.timeScale = 0f;
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = false;
                AudioListener.volume = 1;
                container.SetActive(false);
                Time.timeScale = 1f;
                return;
            }
        }
    }

    public void ResumeButton()
    {
        paused = false;
        AudioListener.volume = 1f;
        container.SetActive(false);
        Time.timeScale = 1f;
    }

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
