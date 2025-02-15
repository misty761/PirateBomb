using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    PlayerMove player;
    
    // 폭탄 생성 위치
    public float offsetBombX = 0.3f;
    public float offsetBombY = 0.3f;
    Vector2 posBomb;

    // prefab of bomb
    public GameObject pfBomb;
    
    // 폭탄 생성 딜레이 타임
    float timeThrow;
    public float delayThrow = 1f;

    // 폭탄 던지는 힘
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

                // 던지는 딜레이 적용
                if (timeThrow > delayThrow)
                {
                    timeThrow = 0f;
                }

                if (timeThrow == 0f)
                {
                    // 폭탄 생성 위치
                    // 오른쪽 보고 있을 때
                    if (player.isLookingRight)
                    {
                        posBomb = new Vector2(transform.position.x + offsetBombX, transform.position.y + offsetBombY);

                    }
                    // 왼쪽 보고 있을 때
                    else
                    {
                        posBomb = new Vector2(transform.position.x - offsetBombX, transform.position.y + offsetBombY);
                    }

                    SoundManager.instance.PlaySound(audioThrow, transform.position, 1f);

                    // 폭탄 생성
                    GameObject goBomb = Instantiate(pfBomb, posBomb, Quaternion.Euler(Vector2.zero));
                    Rigidbody2D rbBomb = goBomb.GetComponent<Rigidbody2D>();

                    // 플레이어가 움직일 때
                    if (!player.isIdle)
                    {
                        // sound
                        //SoundManager.instance.PlaySound(audioThorw, transform.position, 0.5f);

                        // 오른쪽을 보고 있을 때
                        if (player.isLookingRight)
                        {
                            // 오른쪽으로 던짐
                            rbBomb.AddForce(new Vector2(forceThrowX, forceThrowY));
                        }
                        else
                        {
                            // 왼쪽으로 던짐
                            rbBomb.AddForce(new Vector2(-forceThrowX, forceThrowY));
                        }
                    }

                }

            }
        }

        timeThrow += Time.deltaTime;
        
    }
}
