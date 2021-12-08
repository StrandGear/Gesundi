using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Animator cameraAnimator;
    public Animator UIAnimator;
    public GameObject UIobject; //text of hero
    public  float time = 5f;
    private int countDown = 1;
    public GameObject statBar;
    public GameObject CutsceneUI;
    public PauseMenu pauseMenu;
    public GameObject player;
    public GameObject bottlesUI;
    public GameObject scoreUI;
    private bool activateText = false;
    public static string score;
    private float timer;
    public static bool firstTime = true;
    public static float bestTime = 0f;
    public static string bestScore;

    private void Awake() {
        
        Time.timeScale = 1f;
        PauseMenu.TheEndIsActive = false;
        if (PauseMenu.gameRestarted)
        {
            StartCoroutine(TextOnReplay());
            player.GetComponent<PlayerController>().canMove = true;
            scoreUI.SetActive(true);
            pauseMenu.cutsceneActive = false;
            CutsceneUI.SetActive(false);
        }
        else
        {
            pauseMenu.cutsceneActive = true;
            statBar.SetActive(false);
            scoreUI.SetActive(false);
            CutsceneUI.SetActive(true);
            cameraAnimator.SetBool("gameStarts", true);
            UIAnimator.SetBool("gameStarts", true);
            player.GetComponent<PlayerController>().canMove = false;
            bottlesUI.SetActive(false);
            StartCoroutine(Timer());
        }
        firstTime = false;
    }

    void Update()
    {
        if (activateText)
        {
            TextActive(countDown);
            
        }
        if (Input.GetKeyDown(KeyCode.E) && activateText)
        {
            countDown ++;
            TextActive(countDown);
            if (countDown == 6) {
                cameraAnimator.SetBool("gameStarts", false);
                UIAnimator.SetBool("gameStarts", false);
                player.GetComponent<PlayerController>().canMove = true;
                CutsceneUI.SetActive(false);
                statBar.SetActive(true);
                pauseMenu.cutsceneActive = false;
                bottlesUI.SetActive(true);
                scoreUI.SetActive(true);
                Time.timeScale = 1f;
                
            }

        }

        //timer
        if (player.GetComponent<PlayerController>().canMove)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);
            int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
            score = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
            scoreUI.GetComponent<Text>().text = score;
            if (bestTime < timer)
            {
                bestScore = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
                bestTime = timer;
            }
        }
    }

    private void TextActive(int textNumber) {
        switch (textNumber)
        {
            case 1:
                UIobject.SetActive(true);
                UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Today parents make party.. ugh... I hate such things..";
                break;
            case 2:
                UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "While my comrades will bully cs:go losers I will have \"fun\" with small.. ";
                break;
            case 3:
                UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "..stupid cousins and get questions when will I have girldriend.";
                break;
            case 4:
                UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Pretending to have boyfriend didn't work last time. No escape..";
                break;
            case 5:
                UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Okay I have to clean rooms.";
                break;
            default:
                UIobject.SetActive(false);
                break;
        }

    }

    IEnumerator Timer()
    {
            yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
        activateText = true;
    }

    IEnumerator TextOnReplay()
    {
        UIobject.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Here we go again";
        UIobject.SetActive(true);
        yield return new WaitForSeconds(time);
        UIobject.SetActive(false);

    }
}
