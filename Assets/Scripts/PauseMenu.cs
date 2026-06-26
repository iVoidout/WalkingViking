using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    void Update()
    {
        Debug.Log(Input.GetKeyDown(KeyCode.Escape));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioListener.volume = 0f;
            container.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeButton()
    {
        AudioListener.volume = 1f;
        container.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartButton()
    {
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
}
