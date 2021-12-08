using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbItem : MonoBehaviour
{
    private bool playerTriggered;
    public bool garbageInInventory = false;
    public bool canUseFridge = false;
    public bool forkPut = false;
    
    public enum Items
    {
        Dustbin,
        Fridge,
        Dishwasher,
        Plant
    } 
    public Items item;

    private void Awake()
    {
        playerTriggered = false;
    }
    private void Update()
    {
        

        if (playerTriggered)
        {
            if (item == Items.Dustbin)
            {
                if (Input.GetKey(KeyCode.E) && PlayerController.collectedGarbage > 0)
                {
                    PlayerController.collectedGarbage--;
                    PlayerController.points++;
                    garbageInInventory = true;
                }
            }
        if (item == Items.Dishwasher)
            {
                if (Input.GetKey(KeyCode.E) && PlayerController.collectedDishes > 0)
                {
                    PlayerController.collectedDishes--;
                    PlayerController.points++;
                    forkPut = true;
                }
            }
        if (item == Items.Fridge )
            { 
                if (Input.GetKey(KeyCode.E) && PlayerController.collectedMilk > 0)
                {
                    PlayerController.collectedMilk--;
                    PlayerController.points++;
                    canUseFridge = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerTriggered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerTriggered = false;
    }
}
