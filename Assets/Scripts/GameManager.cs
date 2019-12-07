using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [Header("Level Headers")]
    public string MainMenu = "StartMenu";
    public string SettingsMenu = "SettingsMenu";
    public string FirstLevel = "Level1";
    public string NextLevel = "Level1";

    [HideInInspector]
    public string CurrentLevel;

    [Header("Level Settings")]
    public GameObject player;
    public Text MainScoreCoins;
    public Text MainScoreGems;

    public bool GameIsOver;

    private int _scoreCoins = 0;
    private int _scoreGems = 0;

    private PlayerBehaviour playerHP;

    void Awake () {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
	
	void Update ()
    {
        CurrentLevel = SceneManager.GetActiveScene().name;
        GameIsOver = false;

        if (CurrentLevel != MainMenu && CurrentLevel != SettingsMenu)
        {
           if (player == null)
           {
              player = GameObject.FindWithTag("Player");
           }

            playerHP = player.GetComponent<PlayerBehaviour>();

            MainScoreCoins = GameObject.Find("ScoreCoins").gameObject.transform.GetChild(0).GetComponent<Text>();
            MainScoreGems = GameObject.Find("ScoreGems").gameObject.transform.GetChild(0).GetComponent<Text>();
        }
        
    }

    public void GameOver()
    {
        if (!playerHP.IsAlive)
        {
            GameIsOver = true;

            SceneManager.LoadScene("Level1");
        }
    }

    public void Collect(int amount, string tag)
    {
        switch (tag)
        {
            case "Coin":
                _scoreCoins += amount;
                MainScoreCoins.text = "X  " + _scoreCoins.ToString();
                break;

            case "Gem":
                _scoreGems += amount;
                MainScoreGems.text = "X  " + _scoreGems.ToString();
                break;
        }
    }
}
