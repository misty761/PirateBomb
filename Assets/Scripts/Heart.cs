using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject pfEffect;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // player life ++

            // effect
            Instantiate(pfEffect, transform.position, Quaternion.Euler(Vector2.zero));

            // destroy this gameobject
            Destroy(gameObject);
        }
    }
}
