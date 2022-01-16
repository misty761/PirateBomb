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
            // idle(ÇÃ·¹ÀÌ¾î ¾Õ¿¡ ÆøÅº »ý¼º)
            if (playerMove.isIdle)
            {
                // ¿À¸¥ÂÊ º¸°í ÀÖÀ» ¶§
                if (playerMove.isLookingRight)
                {
                    posBomb = new Vector2(transform.position.x + offsetBombX, transform.position.y);
                    
                }
                // ¿ÞÂÊ º¸°í ÀÖÀ» ¶§
                else
                {
                    posBomb = new Vector2(transform.position.x - offsetBombX, transform.position.y);
                }

                // ÆøÅº »ý¼º
                Instantiate(pfBomb, posBomb, Quaternion.Euler(Vector2.zero));
            }
            // move right(¿À¸¥ÂÊÀ¸·Î ÆøÅºÀ» ´øÁü)
            else if (playerMove.isLookingRight)
            {

            }
            // move left(¿ÞÂÊÀ¸·Î ÆøÅºÀ» ´øÁü)
            else
            {

            }
        }
    }
}
