using UnityEngine;
using UnityEngine.UIElements;

public class Blinking : MonoBehaviour
{
    bool bitVisible;
    public float timeBlinking = 1f;
    float timeElapsed = 0f;
    public GameObject arrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bitVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeBlinking)
        {
            timeElapsed = 0f;
            bitVisible = !bitVisible;
        }

        if (bitVisible)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
        //print(bitVisible);
        //print(timeElapsed);
    }

}
