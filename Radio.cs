using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool radioTurnedOn;
    private bool triggered;
    private bool keyPressed;
    public PlayerController playerController;
    public float healthBonus;
    AudioSource audioData;
    

     void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioData = GetComponent<AudioSource>();
        radioTurnedOn = false;
        triggered = false;
        keyPressed = false;
        if (healthBonus == 0f)
            healthBonus = 0.1f;
    }

    void Update()
    {
            if(Input.GetKeyDown(KeyCode.E) && triggered)
            {
                keyPressed = !keyPressed;
                if (keyPressed)
                {
                    radioTurnedOn = true;
                    PlayState(radioTurnedOn);
                    float sec = Random.Range(60f, 189f); //how long music playing
                    StartCoroutine(RadioDuration(sec));
                }
                if (!keyPressed)
                {
                    radioTurnedOn = false;
                    PlayState(radioTurnedOn);
                }
            }
    }

    void PlayState(bool radioTurnedOn)
    {
        if (radioTurnedOn)
        {
            audioData.Play();
            InvokeRepeating("HealthBonus", 0.5f, 1f);
        }
        if(!radioTurnedOn)
        {
            audioData.Pause();
            CancelInvoke(); 
        }
    }

    IEnumerator RadioDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        radioTurnedOn = false;
        PlayState(radioTurnedOn);
    } 

    public void HealthBonus()
    {
        playerController.health += healthBonus;
    }


        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
