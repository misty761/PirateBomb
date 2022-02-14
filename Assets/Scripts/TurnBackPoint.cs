using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBackPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            try
            {
                EnemyMove enemy = collision.GetComponent<EnemyMove>();
                enemy.TurnBack();
            }
            catch
            {
                Debug.Log(collision);
            }
        }
    }
}
