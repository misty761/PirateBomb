using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject pfEffect;
    public PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // player life ++
            player.AddLife();

            // effect
            Instantiate(pfEffect, transform.position, Quaternion.Euler(Vector2.zero));

            // destroy this gameobject
            Destroy(gameObject);
        }
    }
}
