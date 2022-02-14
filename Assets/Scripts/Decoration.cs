using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public Rigidbody2D mRigidbody;
    public float mass = 2f;
    // delay sound
    float delaySound = 2f;
    float time;


    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        mRigidbody.mass = mass;
        time = delaySound;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0f) time -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // delay sound(to prevent make sound at start of the game.)
        if (time <= 0f)
        {
            // sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioThud, transform.position, SoundManager.instance.volumeThud);
        }

    }

}
