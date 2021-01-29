using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class SoundManager
{
    public enum Sound {
        Jump,
        Lose,
        Click,
        Coin
    }

    public static void PlaySound(Sound sound) {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.GetInstance().SoundAudioClip_array) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        return null;
    }

}
