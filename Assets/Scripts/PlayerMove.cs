using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // speed
    public float speed = 1f;
    // looking direction
    public bool isLookingRight;
    // idle
    public bool isIdle;
    // 애니메이터
    Animator animator;
    // 리지드 바디
    Rigidbody2D mRigidbody;
    // 점프력
    public float forceJump = 100f;
    // 땅에 있는지 여부
    bool isGround;
    // 떨어지는 여부
    bool isFalling;
    // particles
    public GameObject pfParticlesRun;
    public float delayParticles = 0.7f;
    public float offsetParticlesRunX = 0.1f;
    public float offsetParticlesRunY = -0.23f;
    float timeParticles;
    public GameObject pfParticlesJump;
    public float offsetParticlesJumpY = -0.1f;
    public GameObject pfParticlesFall;
    public float offsetParticlesFallY = -0.15f;


    // Start is called before the first frame update
    void Start()
    {
        isLookingRight = true;
        isIdle = true;
        animator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        isGround = false;
        isFalling = true;
        timeParticles = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 바라 보는 방향에 따라 스프라이트 회전
        if (isLookingRight)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // falling 판단
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

        // jump
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // particles
                timeParticles = 0f;
                makeParticles(pfParticlesJump, 0f, offsetParticlesJumpY);

                mRigidbody.AddForce(new Vector2(0, forceJump));
                isGround = false;
            }
        }
        else
        {
            // 점프키를 빨리 릴리즈하면 점프를 조금만 하도록 함
            if (Input.GetKeyUp(KeyCode.Space) && mRigidbody.velocity.y > 0f)
            {
                mRigidbody.velocity = new Vector2(mRigidbody.velocity.x, 0);
            }
        }

        // A & D 동시 입력시
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            isIdle = true;
        }
        // move left
        else if (Input.GetKey(KeyCode.A))
        {
            // move
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            isLookingRight = false;
            isIdle = false;

            // particle
            makeParticles(pfParticlesRun, offsetParticlesRunX, offsetParticlesRunY);

        }
        // move right
        else if (Input.GetKey(KeyCode.D))
        {
            // move
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            isLookingRight = true;
            isIdle = false;

            // particle
            makeParticles(pfParticlesRun, -offsetParticlesRunX, offsetParticlesRunY);
        }
        else
        {
            isIdle = true;
        }

        // 애니메이션
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Ground", isGround);
        animator.SetBool("Fall", isFalling);
    }

    void makeParticles(GameObject particles, float offsetX, float offsetY)
    {
        Vector2 pos = new Vector2(transform.transform.position.x + offsetX, transform.position.y + offsetY);

        // 캐릭터 방향에따라 스프라이트 회전
        if (isLookingRight)
        {
            particles.transform.localScale = new Vector2(1, 1);    
        }
        else
        {
            particles.transform.localScale = new Vector2(-1, 1);
        }
        
        // 파티클 생성
        if (timeParticles == 0f && isGround)
        {
            Instantiate(particles, pos, Quaternion.Euler(Vector2.zero));
        }

        // 파티클 딜레이
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
        // 땅에 착지
        if (collision.contacts[0].point.normalized.y < 0)
        {
            isGround = true;

            // 파티클
            timeParticles = 0f;
            makeParticles(pfParticlesFall, 0f, offsetParticlesFallY);
        }
    }
}
