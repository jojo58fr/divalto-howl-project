using System;
using System.Collections;
using UnityEngine;

public class Player : Generic, IGenericEntity
{
    #region Variables
    public static Player instance; 

    public float speedPlayer;

    private int currentLives;
    public int maxLives = 3;

    private bool invicibility = false;
    #endregion

    public Collider2D Coll { get; set; }
    public Rigidbody2D Rb { get; set; }

    public Animator Anim
    {
        get
        {
            return animPlayer;
        }

        set
        {
            animPlayer = value;
        }
    }
    private Animator animPlayer;

    private HealthBar bar;

    // Use this for initialization
    private void Awake ()
    {
        Coll = GetComponent<Collider2D>();
        Rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();

        bar = FindObjectOfType<HealthBar>();

        if (instance != this)
        {
            instance = this;
        }

        currentLives = maxLives;

        bar.MinValue = 0;
        bar.MaxValue = maxLives;
    }

    // Update is called once per frame
    private void Update ()
    {
        Vector2 playerSize = GetComponent<SpriteRenderer>().bounds.size;

        // Here is the definition of the boundary in world point
        var distance = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + (playerSize.x / 2);
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - (playerSize.x / 2);

        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + (playerSize.y / 2);
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - (playerSize.y / 2);

        // Here the position of the player is clamped into the boundaries
        transform.position = (new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, bottomBorder, topBorder),
            transform.position.z)
        );

        if (GameController.instance.isStart && !GameController.instance.gameover)
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            //Debug.Log("Horizontal: " + Horizontal);
            //Debug.Log("Vertical: " + Vertical);
            //Debug.Log("Velocity: " + Rb.velocity + " Speed: " + speedPlayer);

            Vector2 movement = new Vector2(
              Horizontal * speedPlayer,
              Vertical * speedPlayer
            );

            Rb.velocity = movement;

            if(!animPlayer.GetBool("rightShoot") && !animPlayer.GetBool("leftShoot"))
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    //LancementAnimation
                    // Set the Bool to transition to the firing state
                    animPlayer.SetBool("leftShoot", true);
                    AudioManager.instance.Play("PlayerAttack");

                }

                if (Input.GetButtonDown("Fire2"))
                {
                    //LancementAnimation
                    // Set the Bool to transition to the firing state
                    animPlayer.SetBool("rightShoot", true);
                    AudioManager.instance.Play("PlayerAttack");
                }
            }
        }

        // Once transition has begun, reset the bool
        if (animPlayer.GetNextAnimatorStateInfo(0).IsName("Base Layer.playerIdle"))
        {
            animPlayer.SetBool("leftShoot", false);
            animPlayer.SetBool("rightShoot", false);
        }



    }

    public void TakeDamage()
    {
        if (!invicibility && currentLives >= 0)
        {
            Debug.Log("Taking Damage From Enemy...");
            StartCoroutine(FlashBlack());
            StartCoroutine(FindObjectOfType<CameraShake>().Shake(0.05f, 0.025f));
            currentLives--;
            bar.Value = currentLives;
            invicibility = true;

            if (currentLives == 0)
            {
                Death();
            }

            
        }
    }

    public void Death()
    {
        Debug.Log("Game Over !");
        FindObjectOfType<GameController>().gameover = true;

        Rb.velocity = Vector2.zero;
    }

    public void OnChildTriggerEnter2D(Collider2D collider2D, Collider2D other)
    {
        if(animPlayer.GetBool("rightShoot") || animPlayer.GetBool("leftShoot"))
        {
            //Debug.Log(other.name);
            if (other.tag == "Enemy")
            {
                other.transform.parent.GetComponent<IGenericEntity>().TakeDamage();
            }
        }

    }

    private IEnumerator FlashBlack()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();

        sprite.color = Color.black;

        yield return new WaitForSeconds(0.25f);

        sprite.color = Color.white;
        invicibility = false;
        yield return null;

    }

}
