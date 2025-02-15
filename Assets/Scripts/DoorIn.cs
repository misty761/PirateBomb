using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIn : MonoBehaviour
{
    PlayerMove player;
    Animator animator;
    bool isPlayerAtDoor;
    public AudioClip audioDoor;
    bool bOpenDoor;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        animator = GetComponent<Animator>();
        isPlayerAtDoor = false;
        bOpenDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // return
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Opening")
                || animator.GetCurrentAnimatorStateInfo(0).IsName("Closing")
                || player.animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOut")
                || player.animator.GetCurrentAnimatorStateInfo(0).IsName("DoorIn")) return;

            // The player enters door
            if (isPlayerAtDoor)
            {
                // press up button or joystick up
                if (player.isJoystickUp || Input.GetKeyDown(KeyCode.W))
                {
                    SoundManager.instance.PlaySound(audioDoor, transform.position, 1f);
                    animator.SetTrigger("Open");
                    player.DoorIn();
                    bOpenDoor = true;
                }
            }

            if (bOpenDoor && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                bOpenDoor = false;
                GameManager.instance.GoToTheNextScene();
            }
        }
        /*
        catch (Exception ex)
        {
            //Debug.LogException(ex);
        }
        */
        catch
        {
            //Debug.LogError("Exception!");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerAtDoor = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerAtDoor = false;
        }
    }
}
