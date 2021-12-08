using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CousineAI : MonoBehaviour
{
    public Vector2 size;
    private Rigidbody2D rb;
    public float spd =5f;
    Vector2 movement;
    public float duration = 1.8f; // how long obj moving
    public float movingPause = 3.5f; //pause between movings 
    public bool movingToPlayer;
    public Transform playerPos;
    private float mvpPause;

    public List<GameObject> obj_prefab = new List<GameObject>();

    void Start()
    {
        size = GetComponent<BoxCollider2D>().size;
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mvpPause = movingPause;
        InvokeRepeating("Moving", 0f, mvpPause);
        StartCoroutine(Spawn());
        this.tag = "Enemy";
        movingToPlayer = false;
    }

    void Update()
    {
        if (!movingToPlayer)
            mvpPause = 0f;
        else
            mvpPause = movingPause;
        this.GetComponent<BoxCollider2D>().offset = Vector2.zero;
    }

    void Moving()
    {
        if (!movingToPlayer)
        {
            int chance = Random.Range(1, 100);
            switch (chance)
            {
                case var _ when chance < 25:
                    movement.x = 1;
                    break;
                case var _ when chance > 24 && chance < 50:
                    movement.y = 1;
                    break;
                case var _ when chance > 49 && chance < 75:
                    movement.x = -1;
                    break;
                case var _ when chance > 74:
                    movement.y = -1;
                    break;
            }
            StartCoroutine(Mvp(movement));
        }
        else if (movingToPlayer)
            StartCoroutine(MoveToPlayer(playerPos));
    }
    
    IEnumerator MoveToPlayer( Transform playerPos)
    {
        while (movingToPlayer)
        {
            Vector2 direction = playerPos.position - transform.position;
            direction.Normalize();
            rb.MovePosition(rb.position + (direction * Time.deltaTime * spd * 2f));
            yield return null;
        }
        
    }
    IEnumerator Mvp(Vector2 movement)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
             rb.MovePosition(rb.position + movement * spd * Time.deltaTime);
             elapsedTime += Time.deltaTime;
             yield return null;
         }
     }

     IEnumerator Spawn()
     {
         int obj_chance = Random.Range(0,obj_prefab.Count); //what to spawn
         float time_chance = Random.Range(10, 30); //when to spawn
         GameObject obj_spawn = obj_prefab[obj_chance];
         Instantiate(obj_spawn, transform.position, Quaternion.identity);
         yield return new WaitForSeconds(time_chance);
         StartCoroutine(Spawn());
     }

}
