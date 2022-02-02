using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);

        // start game
        GameManager.instance.StartGame();
    }

    public void ContinueGame()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);

        // show AD
        GoogleMobileAdsReward.instance.UserChoseToWatchAd();
    }

    public void NewGame()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);

        // start new game(load scene1)
        GameManager.instance.NewGame();
    }
}
