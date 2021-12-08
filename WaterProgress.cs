using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProgress : MonoBehaviour
{
    public PlayerController player;
    bool playerTriggered;
    public float pouringDuration = 10f;
    public ProgressBar progressBar;
    public int maxWater = 20;
    public int plantWater;
    public float waterDownInterval;
    private bool canPressE;
    float lastUpdate = 0f;
    public float finePointsFrequency;
    public Bottles bottles;
    public bool pouring;
    public EnemyCheck enemyCheck;
    public bool canWater = false;
    private bool pouringFinished = false;
    public GameObject playerObj;
    private Vector2 playerFixedPos;

private void Start() {
    playerTriggered = false;
    plantWater = maxWater;
    InvokeRepeating("WaterDown", 0f, waterDownInterval);
    canPressE = true;
    pouring = false;
}

private void Update()
{
    if (plantWater > maxWater)
    {
        plantWater = maxWater;
        canPressE = false;
    }
    else if (plantWater < maxWater)
        canPressE = true;

    if (plantWater < 0)
        plantWater = 0;

    if (plantWater == 0)
       {
            if (Time.time - lastUpdate >= finePointsFrequency)
            {
                PlayerController.points --;
                lastUpdate = Time.time;
            } 
       }
    progressBar.TaskProgressBar(plantWater, maxWater);

    if ( playerTriggered && Input.GetKeyDown(KeyCode.E) && PlayerController.collectedWater > 0 && canPressE)
    {
            canWater = true;
            canPressE = false;
            playerFixedPos = player.transform.position;
        StartCoroutine(Pouring());
        }
}

    IEnumerator Pouring()
    {
        float ellapsedTime = 0f;
        while (ellapsedTime < pouringDuration)
        {
            if (!enemyCheck.enemyEntered)
            {
                ellapsedTime += Time.deltaTime;
                pouringFinished = true;
                player.transform.position = playerFixedPos;
            }
            else
            {
                ellapsedTime = pouringDuration;
                pouringFinished = false;
            }
            playerObj.GetComponent<Animator>().SetBool("pouring", true);
            pouring = true;
            player.GetComponent<PlayerController>().canMove = false;
            ellapsedTime += Time.deltaTime;
            

            yield return null;
        }
        if (pouringFinished)
        {
            player.GetComponent<PlayerController>().canMove = true;
            bottles.EmptyingBottles();
            PlayerController.collectedWater--;
            plantWater += 3;
            PlayerController.points++;
            canPressE = true;
            pouring = false;
            playerObj.GetComponent<Animator>().SetBool("pouring", false);
        }
        else
        {
            player.GetComponent<PlayerController>().canMove = true;
            canPressE = true;
            pouring = false;
            playerObj.GetComponent<Animator>().SetBool("pouring", false);
        }

    }

    void WaterDown()   
    {
        plantWater -= 1;
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
