using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class LeaderBoard : MonoBehaviour
{
    //public static LeaderBoard instance;

    string LEADER_BOARD_ID = "CgkI5M2Cxf8PEAIQAQ";

    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    private void ProcessAuthentication(bool success)
    {
        Debug.Log("Google Play Games Social : " + success);
    }

    public void RankButtonClick()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, 1f);

        //print("Ranking button is clicked!");
        // get best score
        int bestScore = PlayerPrefs.GetInt("ScoreTop", 0);
        //Debug.Log("Best Score : " + bestScore);

        // post score to leaderboard)
        Social.ReportScore((long)bestScore, LEADER_BOARD_ID, (bool success) => {
            Debug.Log("Google Play Games Report Score : " + success);
        });

        // show leaderboard UI
        PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADER_BOARD_ID);
    }
}

