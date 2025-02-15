using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    PlayerMove player;
    public float offsetY = 0.1f;
    Animator animator;
    public AudioClip audioDoor;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        animator = GetComponent<Animator>();

        //SoundManager.instance.PlaySound(audioDoor, transform.position, 1f);
        MoveStart();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveStart()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + offsetY);
        player.transform.position = pos;

        SoundManager.instance.PlaySound(audioDoor, transform.position, 1f);
        animator.SetTrigger("Open");
        
        player.DoorOut();
    }
}
