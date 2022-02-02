using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenItem : MonoBehaviour
{
    public GameObject pfHeart;
    public float offsetY = 0.1f;
    public int forceY = 100;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            && GameManager.instance.state == GameManager.State.Play)
        {
            // score ++
            GameManager.instance.Scored(10);

            // spawn heart
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + offsetY);
            GameObject heart = Instantiate(pfHeart, pos, Quaternion.Euler(Vector2.zero));
            Rigidbody2D heartRigidbody = heart.GetComponent<Rigidbody2D>();
            heartRigidbody.AddForce(new Vector2(0, forceY));
            Destroy(gameObject);
        }
    }
}
