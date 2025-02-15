using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionText : MonoBehaviour
{
    PlayerMove player;
    DoorIn doorToNext;
    float dist;
    bool bActive;
    public Text textHowToGoToTheNext;
    public int distActive = 13;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMove>();
        doorToNext = FindAnyObjectByType<DoorIn>();
        dist = 400;
        bActive = false;
        textHowToGoToTheNext.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bActive && doorToNext != null)
        {
            dist = player.DistanceFrom(doorToNext.gameObject);
            if (dist < distActive)
            {
                ActiveInstruction();
            }
        }
        else
        {
            if (doorToNext == null)
            {
                Destroy(gameObject);
            }
        }
        //print("dist : " + dist);
    }

    public void ActiveInstruction()
    {
        textHowToGoToTheNext.gameObject.SetActive(true);
    }
}
