using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // looking direction
    bool isLookingRight;
    // animator
    Animator animator;
    // spawn item
    public GameObject pfHeart;
    public float rateSpawnHeart = 0.1f;
    // player
    PlayerMove player;
    // health
    public float health;
    public float healthMax = 2f;
    FloatingBar healthBar;
    GameObject goBar;
    // fire
    public GameObject pfCannonBall;
    public float timeFireMin = 3f;
    public float timeFireMax = 10f;
    float timeFire;
    float timeFireRandom;
    public int distSqrFire = 40;
    public float offsetFireX;
    public float offsetFireY;
    public int forceFireX = 100;
    public int forceFireY = 100;
    public Transform trFire;
    public float distFireX = 3.76f;
    public float distFireY = 1.88f;
    // rigibody
    public Rigidbody2D mRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMove>();
        health = healthMax * GameManager.instance.factorStageMax;
        mRigidbody = GetComponent<Rigidbody2D>();

        // sprite(change sprite depanding on looking direction)
        isLookingRight = false;


        // fire
        ResetFire();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameManager.State.GameOver) return;

        // health bar
        UpdateHealthBar();

        try
        {
            // check distance from player
            float distX = transform.position.x - player.transform.position.x;
            float distY = transform.position.y - player.transform.position.y;
            //print("x:" + distX);
            //print("y:" + distY);

            // looking direction
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                if (distX > 0) isLookingRight = false;
                else isLookingRight = true;
            }

            if (!isLookingRight)
            {
                // player is left of cannon
                if (distX < distFireX && distX > 0f && distY > -distFireY && distY < distFireY)
                {
                    FireCannon();
                }
                else
                {
                    ResetFire();
                }
            }
            // cannon is looking to right
            else
            {
                // player is right of cannon
                if (distX > -distFireX && distX < 0f && distY > -distFireY && distY < distFireY)
                {
                    FireCannon();
                }
                else
                {
                    ResetFire();
                }
            }
        }
        /*
        catch (Exception ex)
        {
            //Debug.LogException(ex);
        }
        */
        catch
        {
            //Debug.LogError("Exception!");
        }

        // sprite
        if (!isLookingRight) transform.localScale = new Vector2(1, 1);
        else transform.localScale = new Vector2(-1, 1);

        // fire if player is close
        // cannon is looking to left

        


        // fire
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fired"))
        {
            GameObject cannonBall = Instantiate(pfCannonBall, trFire.position, Quaternion.Euler(Vector2.zero));
            Rigidbody2D rigidbodyCannonBall = cannonBall.GetComponent<Rigidbody2D>();
            int _forceFireX;
            if (!isLookingRight)
            {
                _forceFireX = -forceFireX;
            }
            else
            {
                _forceFireX = forceFireX;
            }
            rigidbodyCannonBall.AddForce(new Vector2(_forceFireX, forceFireY));
            animator.SetTrigger("Idle");
        }
    }

    void ResetFire()
    {
        timeFireRandom = UnityEngine.Random.Range(timeFireMin, timeFireMax);
        timeFire = 0f;
    }

    void FireCannon()
    {
        timeFire += Time.deltaTime;

        // fire interval
        if (timeFire > timeFireRandom)
        {
            timeFireRandom = UnityEngine.Random.Range(timeFireMin, timeFireMax);
            timeFire = 0f;

            // fire trigger
            animator.SetTrigger("Fire");
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar == null)
        {
            healthBar = GetComponent<FloatingBar>();
            healthBar.goSource = gameObject;
        } 
        else healthBar.guage.fillAmount = health / healthMax;
    }

    public void GetDamage(float damage)
    {
        if (health > 0f)
        {
            health -= damage;

            // break
            if (health <= 0f)
            {
                // score
                GameManager.instance.Scored(20);

                SpawnItem();
                goBar = healthBar.bar.gameObject;
                Destroy(goBar);
                Destroy(gameObject);
            }
        }
    }

    void SpawnItem()
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        if (random < rateSpawnHeart)
        {
            // sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioSpawnItem, transform.position, 0.1f);

            Instantiate(pfHeart, transform.position, Quaternion.Euler(Vector2.zero));
        }
    }
}
