using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : Generic
{
    #region Variables
    //Instance of GameController
    public static GameController instance;
    //Script to access different player settings
    Player player;
    //The background of the game
    GameObject background;

    /// <summary>
    /// Booleans that can indicate :
    /// gameover : Death of the player
    /// retry : If the player continues
    /// isStart : If the game is launched
    /// </summary>
    public bool gameover, retry, isStart = false;

    //Game start time
    public float startTime;

    // The maximum time that can be changed between each spawn
    public float maxSpawnBetween = 1f;
    #endregion

    #region variables states


    /// <summary>
    /// Indicate the new score or not (Bool)
    /// </summary>
    private bool newScore = false;

    private GameObject gameoverPanel;
    private GameObject mainMenu;

    private Text scoreText;
    private Text scoreGOText;

    #endregion


    /// <summary>
    /// Getter of NewScore
    /// </summary>
    public bool NewScore
    {
        get
        {
            return newScore;
        }
    }


    void Start ()
    {

        #region singleton
        if (instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion


        //Initialize the game
        gameover = false;
        retry = false;
        isStart = false;
        InitGame();
    }

    private void Update()
    {
        //Debug.Log(Score.Instance.Nbscore);
    }

    /// <summary>
    /// Initialize all game variables
    /// </summary>
    void InitGame()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        background = GameObject.Find("ScrollingBackground");
        background.GetComponent<ParallaxScrolling>().Speed = 0.5f;
        newScore = false;
        AudioManager.instance.StopAll();
        AudioManager.instance.Play("Game");
        Score.Instance.Reset();

        gameoverPanel = GameObject.Find("GameoverPanel");
        mainMenu = GameObject.Find("MainMenu");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreGOText = GameObject.Find("ScoreGOText").GetComponent<Text>();

        gameoverPanel.SetActive(false);
        mainMenu.SetActive(true);

    }

    void FixedUpdate ()
    {
        if(!isStart)
            startTime = Time.time;

        if (isStart && !gameover)
        {


        }
        else if (gameover)
        {
            background.GetComponent<ParallaxScrolling>().Speed = 0f;
            gameoverPanel.SetActive(true);
            //SavePlayer();

        }

        UpdateScore();

    }

    /// <summary>
    /// Button to start the game
    /// </summary>
    public void StartGame()
    {
        isStart = true;
        retry = false;
        background.GetComponent<ParallaxScrolling>().Speed = 2f;

        mainMenu.SetActive(false);
    }

    public void UpdateScore()
    {
        scoreText.text = Score.Instance.Nbscore.ToString();
        scoreGOText.text = Score.Instance.Nbscore.ToString();
    }

    /// <summary>
    /// Restart game
    /// </summary>
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}