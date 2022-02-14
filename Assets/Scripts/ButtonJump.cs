using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJump : MonoBehaviour
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
        isTouchUp = false;
    }

    public void TouchUp()
    {
        isTouchDown = false;
        isTouchUp = true;
    }
}
