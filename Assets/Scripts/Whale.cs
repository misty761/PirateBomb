using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    DetectBomb detectBomb;
    Animator animator;
    EnemyMove enemy;
    // distance
    float distX;
    public float distAction = 0.43f;
    public float distSqrtClose = 0.2f;
    float distSqrt;
    // audio
    public AudioClip audioBite;

    // Start is called before the first frame update
    void Start()
    {
        detectBomb = gameObject.GetComponentInChildren<DetectBomb>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyMove>();
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

        if (detectBomb.isDetectedBomb)
        {
            if (detectBomb != null && gameObject != null )
            {
                try
                {
                    // distance from bomb
                    distX = transform.position.x - detectBomb.collisionBomb.transform.position.x;  // + : bomb is left of whale
                    //print("distX : " + distX);
                    Vector2 _vector = transform.position - detectBomb.collisionBomb.transform.position;
                    distSqrt = _vector.sqrMagnitude;
                    //print("distSqrt : " + distSqrt);

                    if (distX > -distAction && distX < distAction && distSqrt < distSqrtClose)
                    {
                        // look at bomb
                        if (distX > 0) enemy.isLookingRight = false;
                        else enemy.isLookingRight = true;

                        // animation
                        animator.SetTrigger("Swallow");
                    }
                    detectBomb.isDetectedBomb = false;
                }
                catch
                {
                    //Debug.Log("GameObject is destroyed!");
                } 
            }
        }

        
    }

    public void SwallowBomb()
    {
        if (detectBomb != null && gameObject != null)
        {
            try
            {
                // distance from bomb
                distX = transform.position.x - detectBomb.collisionBomb.transform.position.x;  // + : bomb is left of whale
                //print("distX : " + distX);
                Vector2 _vector = transform.position - detectBomb.collisionBomb.transform.position;
                distSqrt = _vector.sqrMagnitude;
                //print("distSqrt : " + distSqrt);

                // bomb is close
                if (detectBomb.collisionBomb != null)
                {
                    if (distX > -distAction && distX < distAction && distSqrt < distSqrtClose)
                    {
                        // sound
                        SoundManager.instance.PlaySound(audioBite, transform.position, 1f);

                        // destroy bomb & bar
                        Bomb bomb = detectBomb.collisionBomb.GetComponent<Bomb>();
                        bomb.DestroyGo();
                    }
                }
            }
            catch
            {
                //Debug.Log("GameObject is destroyed!");
            }
        }    
    }
}
