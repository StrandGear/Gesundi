using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public Vector2 size;
    public PlayerController player;
    private bool playerEntered;
    public Collider2D[] colliders;
    private float lastUpdate = 0f;
    public float destroyTime = 60f;

    public enum Items
    {
        Garbage, //banana + paper
        Fork, // plates + glasses
        Milk
    } 

    public Items item;
    private void Awake()
    {
        size = GetComponent<BoxCollider2D>().size;
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerEntered = false;
        this.tag = "Item";
    }
    void Update()
    {
        colliders = Physics2D.OverlapBoxAll(transform.position, size, 0, 0);

        if (playerEntered && Input.GetKeyDown(KeyCode.E))
        {
            if (item == Items.Garbage)
                PlayerController.collectedGarbage++;

            else if (item == Items.Fork)
                PlayerController.collectedDishes++;

            else if (item == Items.Milk)
                PlayerController.collectedMilk++;

            Destroy(gameObject);
        } 
        Destroy(gameObject, Random.Range(destroyTime, destroyTime*2));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = false;
        }
    }

}