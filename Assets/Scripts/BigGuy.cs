using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGuy : MonoBehaviour
{
    DetectBomb detectBomb;
    Animator animator;
    EnemyMove enemy;
    // distance
    float distX;
    public float distAction = 0.43f;
    public float distSqrtClose = 0.2f;
    // picked bomb
    bool isPickedBomb;
    // throw bomb
    public GameObject pfBomb;
    public Transform tfTrow;
    public int forceThrowX = 200;
    public int forceThrowY = 190;
    float timeThrow;
    float timeThrowRandom;
    public float timeThrowMin = 1f;
    public float timeThrowMax = 2f;
    public AudioClip audioThrow;
    // player
    PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        detectBomb = gameObject.GetComponentInChildren<DetectBomb>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyMove>();
        player = FindObjectOfType<PlayerMove>();
        isPickedBomb = false;
        timeThrow = 0f;
        timeThrow = Random.Range(timeThrowMin, timeThrowMax);
    }

    // Update is called once per frame
    void Update()
    {
        // return
        if (GameManager.instance.state == GameManager.State.Title
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Ground")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Swallow")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Pick")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Throw")) return;

        // detect bomb
        if (detectBomb.isDetectedBomb)
        {
            if (detectBomb != null && gameObject != null && !animator.GetCurrentAnimatorStateInfo(0).IsName("Pick"))
            {
                try
                {
                    // distance from bomb
                    distX = transform.position.x - detectBomb.collisionBomb.transform.position.x;  // + : bomb is left of whale
                    //print(distX);
                    // pick bomb if bomb is not picked
                    if (!isPickedBomb)
                    {
                        // look at opposite side of bomb
                        if (distX > 0) enemy.isLookingRight = true;
                        else enemy.isLookingRight = false;
                        
                        // animation
                        animator.SetTrigger("Pick");

                        detectBomb.isDetectedBomb = false;
                    }       
                }
                catch
                {
                    //Debug.Log("GameObject is destroyed!");
                }
            }
        }

        // throw bomb
        if (isPickedBomb && animator.GetCurrentAnimatorStateInfo(0).IsName("RunBomb"))
        {
            timeThrow += Time.deltaTime;
            if (timeThrow > timeThrowRandom)
            {
                // calculate new timeThrowRandom
                timeThrow = 0f;
                timeThrowRandom = Random.Range(timeThrowMin, timeThrowMax);

                // player
                float distPlayerX = transform.position.x - player.transform.position.x; // + player is left of big guy
                //print(distPlayerX);

                // throw
                // player is left of big guy
                if (distPlayerX > 0)
                {
                    // look at player
                    enemy.isLookingRight = false;
                }
                // player is right of big guy
                else
                {
                    // look at player
                    enemy.isLookingRight = true;
                }

                // throw bomb
                animator.SetTrigger("Throw");
            }

        }

    }

    public void PickBomb()
    {
        if (detectBomb != null && gameObject != null)
        {
            try
            {
                // distance from bomb
                distX = transform.position.x - detectBomb.collisionBomb.transform.position.x;  // + : bomb is left of whale
                //print("distX : " + distX);
                Vector2 _vector = transform.position - detectBomb.collisionBomb.transform.position;
                float distSqrt = _vector.sqrMagnitude;
                //print("distSqrt : " + distSqrt);

                // bomb is close
                if (detectBomb.collisionBomb != null)
                {
                    // succeed to pick bomb
                    if (distX > -distAction && distX < distAction && distSqrt < distSqrtClose)
                    {
                        // destroy bomb & bar
                        Bomb bomb = detectBomb.collisionBomb.GetComponent<Bomb>();
                        bomb.DestroyGo();

                        // pick bomb
                        isPickedBomb = true;
                    }
                    // fail picking bomb
                    else
                    {
                        animator.SetTrigger("FailPick");
                    }
                }
            }
            catch
            {
                //Debug.Log("GameObject is destroyed!");
            }
        }
    }

    public void ThrowBomb()
    {
        // posion of bomb
        Vector2 posBomb = tfTrow.position;

        // intantiate bomb
        GameObject goBomb = Instantiate(pfBomb, posBomb, Quaternion.Euler(Vector2.zero));
        Rigidbody2D rbBomb = goBomb.GetComponent<Rigidbody2D>();

        // sound
        SoundManager.instance.PlaySound(audioThrow, transform.position, 1f);

        // throw
        // player is left of big guy
        if (!enemy.isLookingRight)
        {
            // throw bomb
            rbBomb.AddForce(new Vector2(-forceThrowX, forceThrowY));
        }
        // player is right of big guy
        else
        {
            // throw bomb
            rbBomb.AddForce(new Vector2(forceThrowX, forceThrowY));
        }

        isPickedBomb = false;     
    }
}
