using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    private int chance = 0;
    private int increment = 4;
    private PlayerController playerController;
    public GameObject borderTopLeft;
    public GameObject borderBottomRight;
    public Transform parentObj;

    // Start is called before the first frame update
    void Start()
    {
        //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        parentObj = transform;
    }

    // Update is called once per frame
    void Update()
    {   
        if ( (PlayerController.points>0) && (PlayerController.points % increment == 0 )) //mb points > 0 ?
        { 
            EnemySpawnPoint();
        } 
    }

    void EnemySpawnPoint() 
    {
        Vector3 v3Pos1 = Camera.main.ViewportToWorldPoint(new Vector3(1.15f, 0.5f, 13f)); //сдвиг по +X
        Vector3 v3Pos2 = Camera.main.ViewportToWorldPoint(new Vector3(0.3f, 1.3f, 13f)); //сдвиг по +Y
        Vector3 v3Pos3 = Camera.main.ViewportToWorldPoint(new Vector3(-0.23f, 0.5f, 13f)); //сдвиг по -X
        Vector3 v3Pos4 = Camera.main.ViewportToWorldPoint(new Vector3(0.3f, -0.3f, 13f)); //сдвиг по -Y
        
        chance = Random.Range (1, 100); 
        switch (chance)
        {
            case var _ when chance < 25 :
                v3Pos1 = SpawnCheck(v3Pos1);
                var enem1 = Instantiate(enemyPrefab1, v3Pos1, Quaternion.identity);
                enem1.transform.parent = parentObj;
            break;
            case var _ when chance > 24 && chance < 50:
                v3Pos2 = SpawnCheck(v3Pos2);
                var enem2 = Instantiate(enemyPrefab1, v3Pos2, Quaternion.identity);
                enem2.transform.parent = parentObj;
            break;
            case var _ when chance > 49 && chance < 75 :
                v3Pos3 = SpawnCheck(v3Pos3); 
                var enem3 = Instantiate(enemyPrefab2, v3Pos3, Quaternion.identity);
                enem3.transform.parent = parentObj;
            break;
            case var _ when chance > 74 :
                v3Pos4 = SpawnCheck(v3Pos4);
                var enem4 = Instantiate(enemyPrefab2, v3Pos4, Quaternion.identity);
                enem4.transform.parent = parentObj;
            break;
        }
        increment += 4;
    }

    Vector3 SpawnCheck(Vector3 spawnVector)
    {
        Vector3 topLeft = borderTopLeft.transform.position;
        Vector3 bottomRight = borderBottomRight.transform.position;
        
        if (spawnVector.x > bottomRight.x)
            spawnVector.x = topLeft.x;
        if (spawnVector.x < topLeft.x)
            spawnVector.x = bottomRight.x;
        if (spawnVector.y > topLeft.y)
            spawnVector.y = bottomRight.y;
        if (spawnVector.y < bottomRight.y)
            spawnVector.y = topLeft.y;

        return spawnVector;
    }
}
