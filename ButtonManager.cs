using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public AudioSource audioScr;
    [HideInInspector]
    public static bool isMuted = false;
    public GameObject settings;
    public GameObject credits;
    public GameObject mainUI;
    public GameObject scoreUI;

    private void Awake()
    {
        Time.timeScale = 1f;

        scoreUI.GetComponent<Text>().text = "BEST TIME: " + GameManager.bestScore;

        if (GameManager.firstTime)
            scoreUI.SetActive(false);
        else
            scoreUI.SetActive(true);
    }
    private void Update()
    {
        if (isMuted == false)
            audioScr.volume = 0.2f;
        else
            audioScr.volume = 0f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("LevelTemplate");
    }

    public void Settings()
    {
        settings.SetActive(true);
        mainUI.SetActive(false);

    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Back()
    {
        settings.SetActive(false);
        credits.SetActive(false);
        mainUI.SetActive(true);
        
    }

    public void Mute()
    {
        isMuted = true;

    }

    public void Unmute()
    {
        isMuted = false;

    }

    public void Quit()
    {
        Application.Quit();
    }
}
