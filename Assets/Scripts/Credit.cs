using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    Button menu;

    private void Start() {
        menu = transform.Find("Menu").GetComponent<Button>();
        menu.onClick.AddListener(Menu);
    }


    private void Menu() {
        // print("PLAY");
        SoundManager.PlaySound(SoundManager.Sound.Click);
        Loader.Load("MenuScene");
    }
}
