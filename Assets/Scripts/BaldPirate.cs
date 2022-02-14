using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldPirate : MonoBehaviour
{
    DetectBomb detectBomb;
    Animator animator;
    EnemyMove enemy;
    // distance
    float distX;
    public float distAction= 0.43f;
    public float distSqrtClose = 0.2f;
    float distSqrt;
    // audio
    public AudioClip audioKick;
    // kick force
    public int forceKickX = 400;
    public int forceKickY = 200;

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
        if (GameManager.instance.state == GameManager.State.Title
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Ground")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Swallow")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Pick")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Throw")) return;

        if (detectBomb.isDetectedBomb)
        {
            if (detectBomb != null && gameObject != null 
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                try
                {
                    // distance from bomb
                    distX = transform.position.x - detectBomb.collisionBomb.transform.position.x;  // + : bomb is left of whale

                    if (distX > -distAction && distX < distAction && distSqrt < distSqrtClose)
                    {
                        // look at bomb
                        if (distX > 0) enemy.isLookingRight = false;
                        else enemy.isLookingRight = true;

                        // animation
                        animator.SetTrigger("Attack");
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

    public void KickBomb()
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
                        SoundManager.instance.PlaySound(audioKick, transform.position, 1f);

                        // kick bomb
                        Bomb bomb = detectBomb.collisionBomb.GetComponent<Bomb>();
                        if (enemy.isLookingRight) bomb.mRigidbody.AddForce(new Vector2(forceKickX, forceKickY));
                        else bomb.mRigidbody.AddForce(new Vector2(-forceKickX, forceKickY));
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
