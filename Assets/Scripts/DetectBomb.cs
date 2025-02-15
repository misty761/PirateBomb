using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBomb : MonoBehaviour
{
    public bool isDetectedBomb;
    public GameObject collisionBomb;

    // Start is called before the first frame update
    void Start()
    {
        isDetectedBomb = false;      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // bomb
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            isDetectedBomb = true;
            collisionBomb = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            isDetectedBomb = false;
            collisionBomb = null;
        }     
    }

}
