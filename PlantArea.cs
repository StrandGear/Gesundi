using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantArea : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public WaterProgress waterProgress;
    public int NumbersOfEnemies;
    public GameObject player;
    public GameObject[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        NumbersOfEnemies = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int randomNum = Random.Range(0, gameObjects.Count - 1);
        int num = 0;
        if (NumbersOfEnemies < gameObjects.Count)
            num = NumbersOfEnemies;
        else
            num = gameObjects.Count;

        foreach(GameObject obj in gameObjects)
            obj.GetComponent<CousineAI>().playerPos = player.transform;

        if (waterProgress.pouring == true)
        {
            foreach (GameObject collider in colliders)
                collider.GetComponent<BoxCollider2D>().isTrigger = true;

            foreach(GameObject enemy in gameObjects)
                enemy.GetComponent<CousineAI>().movingToPlayer = true;
         
        }
        else if (waterProgress.pouring == false)
        {
            foreach (GameObject collider in colliders)
                collider.GetComponent<BoxCollider2D>().isTrigger = false;

            foreach (GameObject enemy in gameObjects)
                enemy.GetComponent<CousineAI>().movingToPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameObjects.Remove(collision.gameObject);
        }
    }
}
