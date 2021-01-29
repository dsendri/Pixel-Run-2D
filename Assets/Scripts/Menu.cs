using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Menu : MonoBehaviour
{
    private Text score;
    Button play;
    Button credit;
    Button reset;

    private void Start() {
        play = transform.Find("PLAY").GetComponent<Button>();
        play.onClick.AddListener(Play);

        credit = transform.Find("CREDIT").GetComponent<Button>();
        credit.onClick.AddListener(Credit);

        reset = transform.Find("RESET").GetComponent<Button>();
        reset.onClick.AddListener(Reset);
    }

    private void Play() {
        // print("PLAY");
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load("MainScene");
    }

    private void Credit() {
        // print("PLAY");
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load("CreditScene");
    }

    private void Reset() {
        // print("PLAY");
        Score.ResetHighscore();
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load("MainScene");
    }

}
