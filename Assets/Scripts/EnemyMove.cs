using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    // speed
    public float speed = 0.9f;
    // particles
    public GameObject pfParticlesRun;
    public float delayParticles = 0.7f;
    public float offsetParticlesRunX = 0.1f;
    public float offsetParticlesRunY = -0.28f;
    float timeParticles;
    public GameObject pfParticlesJump;
    public float offsetParticlesJumpY = -0.15f;
    public GameObject pfParticlesFall;
    public float offsetParticlesFallY = -0.23f;
    // looking direction
    public bool isLookingRight = false;
    // idle
    bool isIdle;
    public int distSqrMove = 40;
    // animator
    Animator animator;
    // rigidbody
    public Rigidbody2D mRigidbody;
    // mass
    public float mass = 2f;
    // ground
    bool isGround;
    // falling
    bool isFalling;
    // spawn item
    public GameObject pfHeart;
    public float rateSpawnHeart = 0.1f;
    // player
    PlayerMove player;
    // turn back
    public float timeTurnMin = 1f;
    public float timeTurnMax = 10f;
    float timeTurn;
    float timeTurnRandom;
    // jump
    public float timeJumpMin = 5f;
    public float timeJumpMax = 10f;
    float timeJump;
    float timeJumpRandom;
    public int forceJumpMin = 200;
    public int forceJumpMax = 400;
    // health
    public float health;
    public float healthMax = 1f;
    FloatingBar healthBar;
    GameObject goBar;
    // attack
    public int power = 1;
    // state
    enum State
    {
        Wait,
        Move,
        Die
    }
    State state;
    // score
    int point;
    bool isSpriteReverse;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLookingRight) isSpriteReverse = false;
        else isSpriteReverse = true;
        isIdle = true;
        animator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        isGround = true;
        isFalling = true;
        state = State.Wait;
        health = healthMax * GameManager.instance.factorStageMax;
        speed = speed * GameManager.instance.factorStageMax;
        power = (int)(power * GameManager.instance.factorStageMax);
        mRigidbody.mass = mass;
        player = FindObjectOfType<PlayerMove>();
        timeTurn = 0f;
        timeTurnRandom = Random.Range(timeTurnMin, timeTurnMax);
        timeJump = 0f;
        timeJumpRandom = Random.Range(timeJumpMin, timeJumpMax);
        CalculatePoint();
    }

    void CalculatePoint()
    {
        point = (int)(healthMax + speed) * 10;
        if (point < 10) point = 10;
        //print(point);
    }

    // Update is called once per frame
    void Update()
    {
        // health bar
        UpdateHealthBar();

        // falling
        if (!isGround)
        {
            if (mRigidbody.velocity.y > 0)
            {
                isFalling = false;
            }
            else
            {
                isFalling = true;
            }
        }
        else
        {
            isFalling = false;
        }

        // sprite(바라보는 방향에 따라 스프라이트 회전)
        if (state != State.Die)
        {
            if (isLookingRight)
            {
                if (!isSpriteReverse) transform.localScale = new Vector2(-1, 1);
                else transform.localScale = new Vector2(1, 1);
            }
            else
            {
                if (!isSpriteReverse) transform.localScale = new Vector2(1, 1);
                else transform.localScale = new Vector2(-1, 1);
            }
        }

        if (player != null)
        {
            // distance from player
            Vector2 _vector = player.transform.position - transform.position;
            int distSqr = (int)_vector.sqrMagnitude;

            // Wait
            if (state == State.Wait)
            {
                // move if player is close
                if (distSqr < distSqrMove)
                {
                    state = State.Move;
                }
                else
                {
                    state = State.Wait;
                }
            }
            // Move
            else if (state == State.Move
                && GameManager.instance.state != GameManager.State.Title
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ground")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isIdle = false;

                if (isLookingRight)
                {
                    transform.Translate(speed * Time.deltaTime * Vector2.right);

                    // particles
                    MakeParticles(pfParticlesRun, -offsetParticlesRunX, offsetParticlesRunY);
                }
                else
                {
                    transform.Translate(speed * Time.deltaTime * Vector2.left);

                    // particles
                    MakeParticles(pfParticlesRun, offsetParticlesRunX, offsetParticlesRunY);
                }
            }
            // Die
            else if (state == State.Die)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    SpawnItem();
                    DestroyGo();
                }
            }
        }
        else
        {
            player = FindObjectOfType<PlayerMove>();
        }


        // turn back
        timeTurn += Time.deltaTime;
        if (timeTurn > timeTurnRandom)
        {
            TurnBack();
        }

        // jump
        timeJump += Time.deltaTime;
        if (timeJump > timeJumpRandom && state == State.Move)
        {
            timeJump = 0f;
            timeJumpRandom = Random.Range(timeJumpMin, timeJumpMax);

            // particles
            timeParticles = 0f;
            MakeParticles(pfParticlesJump, 0f, offsetParticlesJumpY);

            // add force at rigidbody
            int forceJump = Random.Range(forceJumpMin, forceJumpMax);
            mRigidbody.AddForce(new Vector2(0, forceJump));
        }

        // animator
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Ground", isGround);
        animator.SetBool("Fall", isFalling);
    }

    public void DestroyGo()
    {
        goBar = healthBar.bar.gameObject;
        Destroy(goBar);
        Destroy(gameObject);
    }

    public void TurnBack()
    {
        timeTurn = 0f;
        timeTurnRandom = Random.Range(timeTurnMin, timeTurnMax);
        isLookingRight = !isLookingRight;
    }

    void MakeParticles(GameObject particles, float offsetX, float offsetY)
    {
        Vector2 pos = new Vector2(transform.transform.position.x + offsetX, transform.position.y + offsetY);

        // sprite(캐릭터 방향에따라 스프라이트 회전)
        if (isLookingRight)
        {
            particles.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            particles.transform.localScale = new Vector2(-1, 1);
        }

        // particles
        if (timeParticles == 0f && isGround)
        {
            Instantiate(particles, pos, Quaternion.Euler(Vector2.zero));
        }
        // particles delay time
        if (timeParticles < delayParticles)
        {
            timeParticles += Time.deltaTime;
        }
        else
        {
            timeParticles = 0f;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Die) return;
        //print("x:" + collision.contacts[0].point.normalized.x);
        //print("y:" + collision.contacts[0].point.normalized.y);
        float distX = transform.position.x - collision.transform.position.x;
        float distY = transform.position.y - collision.transform.position.y;

        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            // set ground
            if (collision.contacts[0].point.normalized.y < 0f)
            {
                // sound
                SoundManager.instance.PlaySound(SoundManager.instance.audioThud, transform.position, SoundManager.instance.volumeThud);

                isGround = true;

                // paticles
                timeParticles = 0f;
                MakeParticles(pfParticlesFall, 0f, offsetParticlesFallY);
            }

            // collision with Player
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //print("distY : " + disY);
                // enemy attack player
                if (distY > -0.3f)
                {
                    //print("distX : " + distX);

                    // look at player
                    // player is left of enemy
                    if (distX > 0f)
                    {
                        isLookingRight = false;
                    }
                    // player is right of enemy
                    else
                    {
                        isLookingRight = true;
                    }
                    animator.SetTrigger("Attack");
                    player.GetDamage(power);
                }
                // play hit top of enemy(attack from player)
                else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                {
                    GetDamage(player.power);
                }
            }
            // collision with ground
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                
            }
            else
            {
                TurnBack();
            }
        }
        // collision with wall
        else
        {
            TurnBack();
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGround = false;
            //print(isGround);
        }
    }

    public void GetDamage(float damage)
    {
        if (state != State.Die)
        {
            health -= damage;

            animator.SetFloat("Health", health);
            animator.SetTrigger("Damaged");

            // die
            if (health <= 0f)
            {
                state = State.Die;
                Collider2D col = GetComponent<Collider2D>();
                col.isTrigger = true;
                mRigidbody.simulated = false;

                // player is scored
                GameManager.instance.Scored(point);
            }
        } 
    }

    void SpawnItem()
    {
        float random = Random.Range(0f, 1f);
        if (random < rateSpawnHeart)
        {
            // sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioSpawnItem, transform.position, 0.08f);

            Instantiate(pfHeart, transform.position, Quaternion.Euler(Vector2.zero));
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

}
