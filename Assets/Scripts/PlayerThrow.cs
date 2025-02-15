using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PlayerMove player;
    
    // ��ź ���� ��ġ
    public float offsetBombX = 0.3f;
    public float offsetBombY = 0.3f;
    Vector2 posBomb;

    // prefab of bomb
    public GameObject pfBomb;
    
    // ��ź ���� ������ Ÿ��
    float timeThrow;
    public float delayThrow = 1f;

    // ��ź ������ ��
    public int forceThrowX = 150;
    public int forceThrowY = 100;

    // sound
    public AudioClip audioThrow;

    // button
    ButtonBomb button;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMove>();
        timeThrow = 0f;
        button = FindObjectOfType<ButtonBomb>();
    }

    // Update is called once per frame
    void Update()
    {
        // return
        if (player.life <= 0
            || player.animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOut")
            || player.animator.GetCurrentAnimatorStateInfo(0).IsName("DoorIn")
            || GameManager.instance.state != GameManager.State.Play) return;

        // throw bomb
        if (button == null)
        {
            button = FindObjectOfType<ButtonBomb>();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || button.isTouchDown)
            {
                if (player.life <= 0) return;

                // button
                button.isTouchDown = false;

                // ������ ������ ����
                if (timeThrow > delayThrow)
                {
                    timeThrow = 0f;
                }

                if (timeThrow == 0f)
                {
                    // ��ź ���� ��ġ
                    // ������ ���� ���� ��
                    if (player.isLookingRight)
                    {
                        posBomb = new Vector2(transform.position.x + offsetBombX, transform.position.y + offsetBombY);

                    }
                    // ���� ���� ���� ��
                    else
                    {
                        posBomb = new Vector2(transform.position.x - offsetBombX, transform.position.y + offsetBombY);
                    }

                    SoundManager.instance.PlaySound(audioThrow, transform.position, 1f);

                    // ��ź ����
                    GameObject goBomb = Instantiate(pfBomb, posBomb, Quaternion.Euler(Vector2.zero));
                    Rigidbody2D rbBomb = goBomb.GetComponent<Rigidbody2D>();

                    // �÷��̾ ������ ��
                    if (!player.isIdle)
                    {
                        // sound
                        //SoundManager.instance.PlaySound(audioThorw, transform.position, 0.5f);

                        // �������� ���� ���� ��
                        if (player.isLookingRight)
                        {
                            // ���������� ����
                            rbBomb.AddForce(new Vector2(forceThrowX, forceThrowY));
                        }
                        else
                        {
                            // �������� ����
                            rbBomb.AddForce(new Vector2(-forceThrowX, forceThrowY));
                        }
                    }

                }

            }
        }

        timeThrow += Time.deltaTime;
        
    }
}
