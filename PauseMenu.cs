using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool AlertIsActive = false;
    public static bool TheEndIsActive = false;
    public GameObject pauseUI;
    public GameObject alertUI;
    public GameObject lyricsUI;
    public GameObject mentalDeath;
    public static bool isMuted = false;
    public AudioSource audioScr;
    [HideInInspector]
    public bool cutsceneActive;
    public Transform playerTransform;
    public static bool gameRestarted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !AlertIsActive && !cutsceneActive && !TheEndIsActive) 
        {
            if(GameIsPaused || AlertIsActive)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            
        }
        if (TheEndIsActive)
        {
            lyricsUI.SetActive(false);
            mentalDeath.SetActive(false);
        }
    }

    public void Resume() 
    {
        pauseUI.SetActive(false);
        alertUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu() 
    {
        alertUI.SetActive(true);

        pauseUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        gameRestarted = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToPause()
    {
        pauseUI.SetActive(true);
        alertUI.SetActive(false);
    }

        public void Mute()
    {
        ButtonManager.isMuted = true;

    }

    public void Unmute()
    {
        ButtonManager.isMuted = false;

    }

    public void ResetPosition()
    {
        playerTransform.transform.position = Vector3.zero;
    }

    public void PlayAgain()
    {
        gameRestarted = true;
        SceneManager.LoadScene("LevelTemplate");
    }
}
