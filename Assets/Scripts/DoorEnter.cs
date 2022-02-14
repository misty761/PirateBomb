using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    PlayerMove player;
    Animator animator;
    Animator animatorDoorOut;
    bool isPlayerAtDoor;
    public AudioClip audioDoor;
    bool bOpenDoor;
    public GameObject doorOut;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        animator = GetComponent<Animator>();
        animatorDoorOut = doorOut.GetComponent<Animator>();
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
                || player.animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOut")) return;

            if (bOpenDoor && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                bOpenDoor = false;

                // move door out
                player.transform.position = doorOut.transform.position;
                SoundManager.instance.PlaySound(audioDoor, doorOut.transform.position, 1f);
                animatorDoorOut.SetTrigger("Open");
                player.DoorOut();
            }

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
