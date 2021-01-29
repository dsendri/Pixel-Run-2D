using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;

public class FinalScore : MonoBehaviour
{
    private Text score;
    private Text highScore;
    Button playAgain;
    Button menu;

    private void Start() {
        Hide();

        #if UNITY_IOS
        string gameId = "3986050";
        #elif UNITY_ANDROID
        string gameId = "3986051";
        #endif

        bool testMode = false;
        Advertisement.Initialize(gameId, testMode);


        playAgain = transform.Find("TryAgain").GetComponent<Button>();
        playAgain.onClick.AddListener(TryAgain);

        menu = transform.Find("Menu").GetComponent<Button>();
        menu.onClick.AddListener(Menu);

        Player.GetInstance().Player_OnDied += Player_OnDied;
        score = transform.Find("Score").GetComponent<Text>();
        highScore = transform.Find("HighScore").GetComponent<Text>();
    }

    private void Player_OnDied(object sender, EventArgs e)
    {
       Show();
       Score.TrySetNewHighscore(Level.GetInstance().GetScore());
       score.text = "SCORE: " + Level.GetInstance().GetScore().ToString();
       print("HIGHSCORE: " + Score.GetHighscore());
       highScore.text = "HIGHSCORE: " + Score.GetHighscore().ToString();
    }

    private void TryAgain() {
        SoundManager.PlaySound(SoundManager.Sound.Click);
        ShowAd();
        Loader.Load("MainScene");
    }

    private void Menu() {
        SoundManager.PlaySound(SoundManager.Sound.Click); 
        ShowAd();
        Loader.Load("MenuScene");
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void ShowAd() {

        if (CountGame.GameCount == 3) {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
            CountGame.GameCount = 0;
        }
        
    }
}
