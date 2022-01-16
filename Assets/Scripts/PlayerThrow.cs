using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PlayerMove playerMove;
    public float offsetBombX;
    public GameObject pfBomb;
    Vector2 posBomb;
    float timeThrow;
    public float delayThrow = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        timeThrow = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // idle(�÷��̾� �տ� ��ź ����)
            if (playerMove.isIdle)
            {
                // ������ ���� ���� ��
                if (playerMove.isLookingRight)
                {
                    posBomb = new Vector2(transform.position.x + offsetBombX, transform.position.y);
                    
                }
                // ���� ���� ���� ��
                else
                {
                    posBomb = new Vector2(transform.position.x - offsetBombX, transform.position.y);
                }

                // ��ź ����
                Instantiate(pfBomb, posBomb, Quaternion.Euler(Vector2.zero));
            }
            // move right(���������� ��ź�� ����)
            else if (playerMove.isLookingRight)
            {

            }
            // move left(�������� ��ź�� ����)
            else
            {

            }
        }
    }
}
