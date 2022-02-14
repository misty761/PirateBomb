using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionForceX = 150;
    public int explosionForceY = 150;
    public float damage;
    public enum ExplosionFrom
    {
        Bomb,
        CannonBall
    }
    public ExplosionFrom explosionFrom;

    // sound
    public AudioClip audioExplosion;

    // time
    public float timeExplosion = 0.01f;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        // sound
        SoundManager.instance.PlaySound(audioExplosion, transform.position, 1f);

        damage = 0f;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // distance
        Vector3 _vec = collision.ClosestPoint(transform.position);
        Vector2 dist = collision.transform.position - _vec;
        float distSqr = dist.sqrMagnitude;
        damage = (1f - distSqr * 3f) / 0.9f;
        if (explosionFrom == ExplosionFrom.CannonBall) damage = 0.32f * GameManager.instance.factorStageMax;
        if (damage < 0f) damage = 0f;
        //print("damage : " + damage);

        if (time < timeExplosion)
        {
            // enemy
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyMove enemy = collision.GetComponent<EnemyMove>();

                if (enemy != null)
                {
                    // 적을 움직이게 만듦
                    // 적이 오른쪽에 있을 때
                    if (dist.x > 0)
                    {
                        enemy.mRigidbody.AddForce(new Vector2(damage * explosionForceX, damage * explosionForceY));
                    }
                    // 적이 왼쪽에 있을 때
                    else
                    {
                        enemy.mRigidbody.AddForce(new Vector2(-damage * explosionForceX, damage * explosionForceY));
                    }

                    if (explosionFrom == ExplosionFrom.Bomb)
                    {
                        enemy.GetDamage(damage);
                    }
                }
            }
            // cannon
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Cannon"))
            {
                Cannon cannon = collision.GetComponent<Cannon>();

                // 적을 움직이게 만듦
                // 적이 오른쪽에 있을 때
                if (dist.x > 0)
                {
                    cannon.mRigidbody.AddForce(new Vector2(damage * explosionForceX, damage * explosionForceY));
                }
                // 적이 왼쪽에 있을 때
                else
                {
                    cannon.mRigidbody.AddForce(new Vector2(-damage * explosionForceX, damage * explosionForceY));
                }

                cannon.GetDamage(damage);
            }
            // player
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerMove player = collision.GetComponent<PlayerMove>();

                // 플레이어를 움직이게 만듦
                // 플레이어가 오른쪽에 있을 때
                if (dist.x > 0)
                {
                    player.mRigidbody.AddForce(new Vector2(damage * explosionForceX, damage * explosionForceY));
                }
                // 플레이어가 왼쪽에 있을 때
                else
                {
                    player.mRigidbody.AddForce(new Vector2(-damage * explosionForceX, damage * explosionForceY));
                }

                player.GetDamage(damage);
            }
            // decoration
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Decoration"))
            {
                Decoration decoration = collision.GetComponent<Decoration>();

                // decoration을 움직이게 만듦
                // decoration이 오른쪽에 있을 때
                if (dist.x > 0)
                {
                    decoration.mRigidbody.AddForce(new Vector2(damage * explosionForceX, damage * explosionForceY));
                }
                // decoration이 왼쪽에 있을 때
                else
                {
                    decoration.mRigidbody.AddForce(new Vector2(-damage * explosionForceX, damage * explosionForceY));
                }
            }
            // bomb
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
            {
                Bomb bomb = collision.GetComponent<Bomb>();
                bomb.ExplosionBomb();
            }

        }

        
    }

}