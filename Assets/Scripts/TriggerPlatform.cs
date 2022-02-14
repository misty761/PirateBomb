using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    public Collider2D platform;
    PlayerMove player;
    float distY;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // distance Y from the player
            distY = transform.position.y - player.transform.position.y;

            // The platform sets to trigger when the player is below
            if (distY > 0f)
            {
                platform.isTrigger = true;
            }

            // joystick down
            if (player.isJoystickdown || Input.GetKeyDown(KeyCode.S))
            {
                // The platform sets to trigger when the joystick is down
                platform.isTrigger = true;
            }
        } 
        else
        {
            player = FindObjectOfType<PlayerMove>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The player enters trigger after jumping
        if (distY < -0.1f) platform.isTrigger = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // The platform sets to collider when the player exit trigger
        platform.isTrigger = false;
    }
}
