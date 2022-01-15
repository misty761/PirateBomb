using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // speed
    public float speed = 1f;
    // looking direction
    bool isLookingRight;
    // idle
    bool isIdle;
    // 애니메이터
    Animator animator;
    // 리지드 바디
    Rigidbody2D mRigidbody;
    // 점프력
    public float forceJump = 150f;
    // 땅에 있는지 여부
    bool isGround;
    // 떨어지는 여부
    bool isFalling;
    // 스테이트
    enum State
    {
        Wait,
        Follow,
        Die
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        isLookingRight = false;
        isIdle = true;
        animator = GetComponent<Animator>();
        mRigidbody = GetComponent<Rigidbody2D>();
        isGround = false;
        isFalling = true;
        state = State.Wait;
    }

    // Update is called once per frame
    void Update()
    {
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

        // 바라 보는 방향에 따라 스프라이트 회전
        if (isLookingRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        // 플레이어를 쫒아감
        if (state == State.Follow)
        {

        }

        // 죽음
        if (state == State.Die)
        {

        }

        // 애니메이션
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Ground", isGround);
        animator.SetBool("Fall", isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 착지
        if (collision.contacts[0].point.normalized.y < 0)
        {
            isGround = true;
        }
    }
}
