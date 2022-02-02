using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.GetDamage(1);
            StartPoint startPoint = FindObjectOfType<StartPoint>();
            if (player.life > 0) startPoint.MoveStart();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyMove enemy = collision.GetComponent<EnemyMove>();
            enemy.DestroyGo();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Bomb bomb = collision.GetComponent<Bomb>();
            bomb.DestroyGo();
        }
        else
        {
            Destroy(collision);
        }
    }
}
