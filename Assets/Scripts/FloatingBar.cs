using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingBar : MonoBehaviour
{
    public GameObject pfBar;
    GameObject canvas;
    Camera mCamera;
    public RectTransform bar;
    public float offsetY = 0.25f;
    public Image guage;
    public GameObject goSource;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        mCamera = Camera.main;
        bar = Instantiate(pfBar, canvas.transform).GetComponent<RectTransform>();
        guage = bar.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _pos = mCamera.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + offsetY));
        if (bar == null)
        {
            //bar = Instantiate(pfBar, canvas.transform).GetComponent<RectTransform>();
            //guage = bar.GetChild(0).GetComponent<Image>();
        }
        if (bar != null) bar.position = _pos;

        // destroy gameobject if source gameobject is destroed 
        //if (goSource == null) Destroy(gameObject);
    }
}
