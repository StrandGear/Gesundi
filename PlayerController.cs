using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float playerSpeed, health, healthDownInterval = 3f, healthCoefficient, finePointsFrequency;
    private float healthCoefficientIncrement = 1, lastUpdate = 0f, lastUpdate2 = 0f, randomTime = 40f;

    public GameObject guideText, leaveMeText, GameoverUI, lyricsUI, mentalDeath, smthInWay;

    Vector2 movement;

    public static int points = 0;
    public static int collectedGarbage = 0, collectedDishes = 0, collectedWater = 0, collectedMilk = 0;
    private int enemyNear = 0, randomNum = 0, enemiesPassed = 0;

    private Animator animator;
    public Animator cameraAnimator;

    [HideInInspector]
    public  bool canMove, textActive = false;
    bool facingRight = true;

    public StatsBar statsBar;
    public EnemyCheck enemyCheck;
    public AudioManager audioManager;

    private List<string> lyrics = new List<string>() { 
        "Maybe I'll lay down for a little...",
        "Slowly I could die",
        "And in the end guess we paid the cost", 
        "..so tired of being so tired", 
        "I'm not gonna crack", 
        "You wanna find peace of mind.. Lookin for the answer",
        "Whose fault is that, if it wasn't Mum and Dad's",
        "Panic on the brain, Michael's gone insane",
        "Sweet 16, how was I supposed to know anything?",
        "Now December..Found the love we shared in September",
        "Don't believe me, just watch",
        "You look so broken when you cry",
        "I'm your demon never leaving",
        "But he refused to answer.. Because he's naked and ashamed",
        "You made it, your shit is overrated",
        "That one sin that caused the fall",
        "Here we are now, entertain us",
        "Why don't you know that you are my mind?",
        "Conversion, software version 7.0",
        "I don't think you trust In, my, Self-righteous suicide",
        "To remind myself of a time when I tried so hard",
        "But in the end, it doesn't even matter",
        "This is not a problem, it's convincin' that it's not",
        "Don't call it a problem, it's the only thing that I still got",
        "We would build a rocket ship And then we'd fly it far away",
        "I think I'm dumb or maybe I'm just .. no way",
        "Chew your meat for you Pass it back and forth",
        "I would shiver the whole night through",
        "You need something I can never give",
        "Everything is my fault",
        "And the night air feels alive",
        "I'd die for you that's easy to say",
        "I swear to God, i never fall in love",
        "I've been thinking too much",
        "What else could I write? I don't have the right",

    };

    private void Start() {
        animator = GetComponent<Animator>();
        health = 20f;
        points = 7;
        collectedGarbage = 0;
        collectedDishes = 0;
        collectedWater = 0;
        collectedMilk = 0;
        InvokeRepeating("HealthDown", 10f, healthDownInterval);
        randomNum = Random.Range(4, 30);
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        healthCoefficient *= healthCoefficientIncrement;
        if (points < -2)
            points = -2;

        statsBar.HealthBar(health);
        //DEATH
        if (health < 0)
            StartCoroutine(Die(true));
        else if ( points < -1)
            StartCoroutine(Die());
        //BAR
        if (points < 20)
            statsBar.TaskBar(points);
        else
            statsBar.TaskBar(points, points);
        //INSANITY
        if (enemyCheck.enemyEntered)
        {
            enemyNear++;
            enemiesPassed++;
        }
        else if (!enemyCheck.enemyEntered)
            enemyNear--;

        if (enemyNear >= 3)
        {
            healthCoefficientIncrement = 1.0001f;
            StartCoroutine(SetText(leaveMeText));
        }
        else
        {
            healthCoefficientIncrement = 1;
        }

        if (enemiesPassed > randomNum)
            StartCoroutine(SetText(smthInWay));
        //FINES
        if (Time.time - lastUpdate >= finePointsFrequency)
        {
            points--;
            lastUpdate = Time.time;
        }
        //LYRICS
        if (randomTime > 0)
            randomTime -= Time.deltaTime;
        else
        {
            lyricsUI.GetComponent<Text>().text = lyrics[Random.Range(0, lyrics.Count - 1)];
            StartCoroutine(SetText(lyricsUI));
            randomTime = Random.Range(40f, 70f);
            lastUpdate2 = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            rb.MovePosition(rb.position + movement * playerSpeed * Time.fixedDeltaTime);

        if ( (movement.x > 0 && !facingRight) || (movement.x < 0 && facingRight) )
        {
            Flip();
        }

        if ((movement.x != 0 || movement.y != 0) && canMove)
        {
            
            animator.SetBool("moving", true);

        }
        else 
            animator.SetBool("moving", false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate( 0f, 180f, 0f);
    }
    void HealthDown()
    {
        if (points < 15)
            health -= healthCoefficient;
        else
            health -= healthCoefficient*2;
    }

    IEnumerator Die (bool mentalBreakdown = false)
    {
        animator.SetBool("dying", true);
        cameraAnimator.SetBool("gameOver", true);
        canMove = false;
        if (mentalBreakdown)
        {
            audioManager.source.pitch = 1.5f;
            mentalDeath.SetActive(true);
        }

        yield return new WaitForSeconds(2.7f);
        mentalDeath.SetActive(false);
        GameoverUI.SetActive(true);
        PauseMenu.TheEndIsActive = true;
        Time.timeScale = 0f;
    }
    IEnumerator SetText(GameObject gameObjectUI)
    {
        textActive = true;
        gameObjectUI.SetActive(true);
        yield return new WaitForSeconds(3.8f);
        textActive = false;
        gameObjectUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item") && !textActive)
        {
            textActive = true;
            string txt = collision.gameObject.GetComponent<PickupItem>().item.ToString();
            guideText.GetComponent<Text>().text = "Press E to pick up " + txt;
            guideText.SetActive(true);
        }
        else if(collision.gameObject.CompareTag("Enemy") )
        {
            textActive = true;
            guideText.GetComponent<Text>().text = "Mmm-mmm... Something in the way";
            guideText.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        guideText.SetActive(false);
        textActive = false;

    }
}
