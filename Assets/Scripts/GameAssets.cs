using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

    public static GameAssets GetInstance() {
        return instance;
    }

    public void Awake() {
        instance = this;
    }

    public Platform[] platform_ground_array;
    public SoundAudioClip[] SoundAudioClip_array;


    [System.Serializable]
    public class Platform {
        public Transform transform;
        public Tilemap tilemap;
    }

    [System.Serializable]
    public class  SoundAudioClip {

        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
