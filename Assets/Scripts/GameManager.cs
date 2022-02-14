using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // state
    public enum State
    {
        Title,
        Play,
        GameOver
    }
    public State state;
    // player
    PlayerMove player;
    // score
    int scoreCurrent;
    int scoreTop;
    public Text textScoreCurrent;
    public Text textScoreTop;
    // stage
    int stage;
    public int stageMax;
    public Text textStage;
    public float factorStageMax = 1f;
    float factorIncrement = 0.1f;
    // UI
    public GameObject uiTitle;
    public GameObject uiPlay;
    public GameObject uiGameOver;
    // development
    public bool isDevelopment = true;

    private void Awake()
    {
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("GameObject is already exist!");
            Destroy(gameObject);
        }

        // state
        state = State.Title;
        SetUI(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTop = PlayerPrefs.GetInt("ScoreTop", 0);
        textScoreTop.text = "Top : " + scoreTop;

        FirstInit();
    }

    // new game
    public void FirstInit()
    {
        stage = 1;
        textStage.text = "Stage : " + stage;
        factorStageMax = 1f;
        Init();
    }

    // continue
    private void Init()
    {
        scoreCurrent = 0;
        textScoreCurrent.text = "Score : " + scoreCurrent;
        player = FindObjectOfType<PlayerMove>();
        player.life = player.lifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        // developing
        Developing();

        CheckContinueGame();
    }

    void Developing()
    {
        if (isDevelopment)
        {
            // press enter to start game
            if (Input.GetKeyDown(KeyCode.Return) && state == State.Title)
            {
                StartGame();
            }
            
            // press keypad+ to go to the next scene
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && state != State.GameOver)
            {
                GoToTheNextScene();

                StartGame();
            }

            // player life ++
            if (Input.GetKeyDown(KeyCode.L))
            {
                player.AddLife();
            }
        }
    }

    public void Scored(int point)
    {
        if (state == State.Play)
        {
            // sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioScore, player.transform.position, 0.1f);

            // score ++
            scoreCurrent += point;
            textScoreCurrent.text = "Score : " + scoreCurrent;
        }
    }

    public void TopScore()
    {
        // new top score
        if (scoreCurrent > scoreTop)
        {
            // sound
            SoundManager.instance.PlaySound(SoundManager.instance.audioFanfare, player.transform.position, 1f);

            scoreTop = scoreCurrent;
            PlayerPrefs.SetInt("ScoreTop", scoreTop);
            textScoreTop.text = "Top : " + scoreTop;
        }
    }

    public void StartGame()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioClick, player.transform.position, 1f);

        // state
        state = State.Play;

        // UI
        SetUI(state);
    }

    void SetUI(State currentState)
    {
        if (currentState == State.Title)
        {
            uiTitle.SetActive(true);
            uiPlay.SetActive(false);
            uiGameOver.SetActive(false);
        }
        else if (currentState == State.Play)
        {
            uiTitle.SetActive(false);
            uiPlay.SetActive(true);
            uiGameOver.SetActive(false);
        }
        else if (currentState == State.GameOver)
        {
            uiTitle.SetActive(false);
            uiPlay.SetActive(false);
            uiGameOver.SetActive(true);
        }
    }

    public void NewGame()
    {
        FirstInit();

        LoadGameScene(stage);

        state = State.Title;
        SetUI(state);
    }

    public void GameOver()
    {
        // sound
        SoundManager.instance.PlaySound(SoundManager.instance.audioGameOver, player.transform.position, 1f);
        
        // state
        state = State.GameOver;

        // UI
        SetUI(state);

        // top score
        TopScore();
    }

    public void CheckContinueGame()
    {
        GoogleMobileAdsReward googleAd = GoogleMobileAdsReward.instance;

        //print("Google AD is rewared : " + googleAd.isRewarded);
        //print("Google AD is closed : " + googleAd.bCloseAD);
        
        if (googleAd.bCloseAD)
        {
            if (googleAd.isRewarded)
            {
                // continue game
                if (stage == 1)
                {
                    NewGame();
                }
                else
                {
                    LoadGameScene(stage);

                    Init();

                    StartGame();
                    player.Init();
                }
            }

            // load new AD
            googleAd.MyLoadAD();
        }
    }

    public void GoToTheNextScene()
    {
        if (stage < stageMax)
        {
            stage++;
        }
        else
        {
            factorStageMax += factorIncrement;
        }

        LoadGameScene(stage);
    }

    void LoadGameScene(int numberScene)
    {
        DestroyBar();

        textStage.text = "Stage : " + numberScene;

        LoadMyScene("Scene" + stage);

        if (stage == 1)
        {
            MyCanvas myCanvas = FindObjectOfType<MyCanvas>();
            Destroy(myCanvas.gameObject);
            Destroy(player.gameObject);
            GoogleMobileAdsReward googleAD = FindObjectOfType<GoogleMobileAdsReward>();
            Destroy(googleAD.gameObject);
            Destroy(gameObject); 
        }

        
    }

    void LoadMyScene(string scene)
    {
        // 비동기적으로 Scene을 불러오기 위해 Coroutine을 사용한다.
        StartCoroutine(LoadMyAsyncScene(scene));
    }

    IEnumerator LoadMyAsyncScene(string scene)
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void DestroyBar()
    {
        GameObject[] bars;
        bars = GameObject.FindGameObjectsWithTag("Bar");
        int len = bars.Length;
        for (int i = 0;i < len;i++)
        {
            Destroy(bars[i]);
        }
    }
}
