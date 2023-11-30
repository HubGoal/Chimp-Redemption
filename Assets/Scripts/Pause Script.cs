using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        // Pausa la música deshabilitando el AudioListener
        AudioListener.pause = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        // Reanuda la música habilitando el AudioListener
        AudioListener.pause = false;
    }
}
