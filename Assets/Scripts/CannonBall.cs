using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject pfExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject goExplosion = Instantiate(pfExplosion, transform.position, Quaternion.Euler(Vector2.zero));
        Explosion explosion = goExplosion.GetComponent<Explosion>();
        explosion.explosionFrom = Explosion.ExplosionFrom.CannonBall;
        
        Destroy(gameObject);
    }
}
