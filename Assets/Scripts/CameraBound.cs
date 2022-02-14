using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    BoxCollider2D bound;
    CameraFollow mCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        mCamera = FindObjectOfType<CameraFollow>();
        mCamera.SetBound(bound);
    }
}
