using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
    // timer
    FloatingBar bombBar;
    GameObject goBar;

    // delay
    public float delayExposion = 3f;
    float timeExposion;

    // explosion effect
    public GameObject effectExplosion;

    // transform
    public Transform tfExplosion;

    // rigidbody
    public Rigidbody2D mRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
        timeExposion = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            // timer
            if (bombBar == null)
            {
                bombBar = GetComponent<FloatingBar>();
                bombBar.goSource = gameObject;
            }
            else
            {
                bombBar.guage.fillAmount = timeExposion / delayExposion;
            }
        }
        /*
        catch (Exception ex)
        {
            //Debug.LogException(ex);
        }
        */
        catch
        {
            //Debug.LogError("Exception!");
        }


        // exposion after delay
        timeExposion += Time.deltaTime;
        if (timeExposion > delayExposion)
        {
            ExplosionBomb();
        }
    }

    public void ExplosionBomb()
    {
        GameObject goExplosion = Instantiate(effectExplosion, tfExplosion.position, Quaternion.Euler(Vector2.zero));
        Explosion explosion = goExplosion.GetComponent<Explosion>();
        explosion.explosionFrom = Explosion.ExplosionFrom.Bomb;

        DestroyGo();
    }

    public void DestroyGo()
    {
        try
        {
            // destroy bar & gameobject
            goBar = bombBar.bar.gameObject;
            Destroy(goBar);
            Destroy(gameObject);
        }
        catch
        {
            Debug.Log(goBar);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioThud, transform.position, SoundManager.instance.volumeThud);
    }

}
