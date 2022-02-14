using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Collider2D platform;
    PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // The play is on ground
        player.isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // The player leaves on the platform
        platform.isTrigger = true;
        player.isGround = false;
    }
}
