using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dancing : MonoBehaviour
{
    private Radio radio;
    bool inRadioRange;
    bool keyPressed;
    public float healthBonus;
    PlayerController playerController;
    public GameObject talkingUI;
    int firstTimeEntered = 3;
    private Animator animator;
    private bool canDance;
    public float textDuration;

    private void Start() {
        radio = transform.parent.GetComponent<Radio>();
        inRadioRange = false;
        keyPressed = false;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        animator = playerController.GetComponent<Animator>();
        if (healthBonus == 0f)
            healthBonus = 0.001f;
        talkingUI.gameObject.GetComponent<UnityEngine.UI.Text>().text = " ";
        canDance = false;
    }

    private void Update()
    {
        if (inRadioRange && radio.radioTurnedOn)
        {
            if (Input.GetKey(KeyCode.F))
                keyPressed = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                canDance = true;
            }
        }
        else
        {
            canDance = false;
            keyPressed = false;
        }
        
        if (keyPressed)
        {
            InvokeRepeating("HealthBonus", 0f, 0.8f);
            
        }
        else
        {
            CancelInvoke();
        }

        if (canDance)
            animator.SetBool("dancing", true);

        if (Input.GetKeyUp(KeyCode.F))
        {
            canDance = false;
            animator.SetBool("dancing", false);
        }

        if (inRadioRange && Input.GetKey(KeyCode.F) && !radio.radioTurnedOn)
        {
            talkingUI.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Who even dances without music?";
            StartCoroutine(TextActive(5f));
        }
    }

    void HealthBonus()
    {
        playerController.health += healthBonus;
    }

    IEnumerator TextActive(float time)
    {
        talkingUI.SetActive(true);
        yield return new WaitForSeconds(time);
        talkingUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRadioRange = true;

            if ( firstTimeEntered == 0)
            {
                talkingUI.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Press  F to Pay ... Dance";
                StartCoroutine(TextActive(textDuration));
            }
            else
            {
                talkingUI.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Hold  F to dance";
                StartCoroutine(TextActive(textDuration));
                
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRadioRange = false;
            firstTimeEntered --;
        }
    }
}
