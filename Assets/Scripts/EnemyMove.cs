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
    // �ִϸ�����
    Animator animator;
    // ������ �ٵ�
    Rigidbody2D mRigidbody;
    // ������
    public float forceJump = 150f;
    // ���� �ִ��� ����
    bool isGround;
    // �������� ����
    bool isFalling;
    // ������Ʈ
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
        // falling �Ǵ�
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

        // �ٶ� ���� ���⿡ ���� ��������Ʈ ȸ��
        if (isLookingRight)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        // �÷��̾ �i�ư�
        if (state == State.Follow)
        {

        }

        // ����
        if (state == State.Die)
        {

        }

        // �ִϸ��̼�
        animator.SetBool("Idle", isIdle);
        animator.SetBool("Ground", isGround);
        animator.SetBool("Fall", isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ����
        if (collision.contacts[0].point.normalized.y < 0)
        {
            isGround = true;
        }
    }
}
