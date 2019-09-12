using System.Collections;
using UnityEngine;

public class Enemy : Generic, IGenericEntity
{
    #region Variables
    public static Enemy instance;
    private int currentLives;
    public int maxLives = 3;
    public float speedEnemy;

    private bool invicibility = false;

    private float currentTime = 0f;

    private float minTime = 1.5f;
    private float maxTime = 4f;

    private float randMaxTime;
    #endregion

    public Collider2D Coll { get; set; }
    public Rigidbody2D Rb { get; set; }

    public Animator Anim
    {
        get
        {
            return animEnemy;
        }

        set
        {
            animEnemy = value;
        }
    }

    private Animator animEnemy;

    // Use this for initialization
    void Start()
    {
        Coll = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
        animEnemy = GetComponent<Animator>();



        if (instance != this)
        {
            instance = this;
        }

        currentLives = maxLives - 1;
        randMaxTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.instance.isStart && !GameController.instance.gameover && Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position) > 0.06)
        {
            Transform target = FindObjectOfType<Player>().transform;
            Transform myPosition = this.transform;

            Vector2 dir = target.transform.position - myPosition.position;

            //Move Towards Target
            myPosition.position += (target.position - myPosition.position).normalized * speedEnemy * Time.deltaTime;

        }

        // Once transition has begun, reset the bool
        if (animEnemy.GetNextAnimatorStateInfo(0).IsName("Base Layer.enemyIdle"))
        {
            animEnemy.SetBool("leftShoot", false);
            animEnemy.SetBool("rightShoot", false);
        }

        if (GameController.instance.gameover)
        {
            
        }
    }

    public void TakeDamage()
    {
        if(!invicibility)
        {
            Debug.Log("Taking Damage From Player...");
            StartCoroutine(FlashRed());
            //Destroy(gameObject);
            if (currentLives <= 0)
            {
                Death();
            }
            else
            {
                currentLives--;
                invicibility = true;
            }
        }

    }

    public void Death()
    {
        Score.Instance.Nbscore++;
        Destroy(gameObject);
    }

    public void OnChildTriggerEnter2D(Collider2D collider2D, Collider2D other)
    {

        if ( ( !animEnemy.GetBool("rightShoot") || !animEnemy.GetBool("leftShoot") ))
        {
            //Debug.Log(currentTime);
            
            if (other.tag == "Player")
            {
                if(currentTime > randMaxTime)
                {
                    currentTime = 0f;

                    if (collider2D.transform.name == "LeftDamageTrigger")
                    {
                        animEnemy.SetBool("leftShoot", true);
                    }
                    else if (collider2D.transform.name == "RightDamageTrigger")
                    {
                        animEnemy.SetBool("rightShoot", true);
                    }
                    other.transform.parent.GetComponent<IGenericEntity>().TakeDamage();
                }
                else
                {
                    currentTime += Time.deltaTime;
                }
            }
        }
    }

    private IEnumerator FlashRed()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

        sprite.color = Color.red;

        yield return new WaitForSeconds(0.25f);

        sprite.color = Color.white;
        invicibility = false;
        yield return null;

    }
}