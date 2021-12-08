using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sink : MonoBehaviour
{
    private bool playerEntered;
    public Bottles bottles;

    void Start()
    {
        playerEntered = false;
    }

    void Update()
    {
        if (playerEntered && Input.GetKeyDown(KeyCode.E))
        {
            bottles.FillingBottles();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerEntered = false;
    }
}
