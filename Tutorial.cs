using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tipText;
    public bool playerEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        tipText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StopTutorial())
        {
            GetComponent<Animator>().SetBool("startAnim", false);
            tipText.SetActive(false);
            //gameObject.SetActive(false);
            Destroy(gameObject);
            //playerEntered = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("startAnim", true);
            tipText.SetActive(true);
            playerEntered = true;
        }
    }

    public virtual bool StopTutorial()
    {
        return false;
    }
}
