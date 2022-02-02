using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBomb : MonoBehaviour
{
    public bool isTouchDown;
    public bool isTouchUp;

    // Start is called before the first frame update
    void Start()
    {
        isTouchDown = false;
        isTouchUp = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TouchDown()
    {
        isTouchDown = true;
    }

    public void TouchUp()
    {
        isTouchUp = true;
    }
}
