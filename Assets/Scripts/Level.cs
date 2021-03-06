﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

using Random=UnityEngine.Random;

public class Level : MonoBehaviour
{
    
    private const int DESTROY_POS = -10;
    private int SPAWN_POS_TRIGGER = 6;
    private const int SPAWN_POS = 9;
    [SerializeField] private float height = 2f;
    [SerializeField] static float movementSpeed = 5f;
    private float level = 0f;
    private int counter = 0;
    private float fixedDeltaTime;
    private List<PlatformController> PlatformControllerList;
    private State state;
    private int score;

    public static Level instance;

    public static Level GetInstance() {
        return instance;
    }

    public int GetScore() {
        return score/100;
    }

    public void SetScore() {
        score = score + 1000;
    }

    private enum State {
        WaitingToStart,
        Playing,
        Death
    }

    private void Player_OnStartedPlaying(object sender, EventArgs e)
    {
        print("Start");
        state = State.Playing;
    }

    private void Player_OnDied(object sender, EventArgs e)
    {
        state = State.Death;
    }

    void Awake()
    {
        print("Awake");
        SpawnInitialPlaftorm();
        instance = this;     
        Time.timeScale = 0.8f;
        // Time.fixedDeltaTime = 0.02f;
        this.fixedDeltaTime = 0.8f;
    }
    
    private void Start()
    {
        score = 0;
        state = State.WaitingToStart;
        Player.GetInstance().OnStartedPlaying += Player_OnStartedPlaying;
        Player.GetInstance().Player_OnDied += Player_OnDied;
        
    } 
  
    void Update()
    {
        if (state == State.Playing) {
            HandlePlatformMovementAndSpawn();

            int widthLast  = PlatformControllerList[PlatformControllerList.Count-1].GetWidth()/2; 
            // print("width: " + widthLast + ", xpos: " + PlatformControllerList[PlatformControllerList.Count-1].GetXPosition());
            if (PlatformControllerList[PlatformControllerList.Count-1].GetXPosition()+widthLast < SPAWN_POS_TRIGGER) {
                // print("SPAWN");
                    // print("width: " + widthLast + ", xpos: " + PlatformControllerList[PlatformControllerList.Count-1].GetXPosition());
                    score = score + 100;
                    if (level == 0) {
                        if (Random.Range(0.0f, 1.0f) > 0.5) {
                            level = level + 2f;
                        }    
                    } else if ( level == 2) {
                        if (Random.Range(0.0f, 1.0f) < 0.33) {
                            level = level - 2f;
                        } else if (Random.Range(0.0f, 1.0f) > 0.67) {
                            level = level + 2f;
                        } 
                    } else if ( level == 4) {
                        if (Random.Range(0.0f, 1.0f) < 0.33) {
                            level = level - 2f;
                        } else if (Random.Range(0.0f, 1.0f) > 0.67) {
                            level = level - 4f;
                        } 
                    }

                    int platform = Random.Range(0,12);
                    SpawnPlaftorm(SPAWN_POS, level,platform);
                    counter++;
                    if (counter % 5 == 0 && Time.timeScale < 1.6f) {
                        // movementSpeed = movementSpeed + 1f;
                        // SPAWN_POS_TRIGGER = SPAWN_POS_TRIGGER - 1;
                        // Time.timeScale = 1.1f * Time.timeScale;
                        // Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
                        Time.timeScale = this.fixedDeltaTime * 1.1f;
                        // Time.timeScale = 1.5f;
                        this.fixedDeltaTime = Time.timeScale;
                        // print("Time");
                        // print(Time.fixedDeltaTime);
                        // print(Time.timeScale);
            
                    }
            }
        }
    }
    
    private void SpawnInitialPlaftorm() {
        Transform plaformTransform;

        plaformTransform = Instantiate(GameAssets.GetInstance().platform_ground_array[0].transform, new Vector3(0,0,0), Quaternion.identity);
        plaformTransform.transform.parent = GameObject.Find("Level").transform;

        PlatformControllerList = new List<PlatformController>();
        PlatformController platformController = new PlatformController(plaformTransform, GameAssets.GetInstance().platform_ground_array[0].tilemap);
        PlatformControllerList.Add(platformController);
    }

    private void SpawnPlaftorm(float xPos , float yPos, int platform) {
        
        Transform plaformTransform;
        plaformTransform = Instantiate(GameAssets.GetInstance().platform_ground_array[platform].transform, new Vector3(20,yPos,0), Quaternion.identity);
        plaformTransform.transform.parent = GameObject.Find("Level").transform;

        PlatformController platformController = new PlatformController(plaformTransform, GameAssets.GetInstance().platform_ground_array[platform].tilemap);
        PlatformControllerList.Add(platformController);
        plaformTransform.position =  new Vector3((xPos + platformController.GetWidth()/2),yPos,0);
        // print("ADDED");
    }

    private void HandlePlatformMovementAndSpawn()
    {
        // print("Total: " + PlatformControllerList.Count);
        
        for (int i = 0; i < PlatformControllerList.Count; i++) {
            // print("index: " + i);
            // print(PlatformControllerList[i].platform.ToString());
            // print(PlatformControllerList[i].platformTilemap.ToString());
            // print(PlatformControllerList[i].GetWidth());
            int width = PlatformControllerList[i].GetWidth()/2;
            // print(width);
            // print(platformList[i].position);
            PlatformControllerList[i].Move();

            if (PlatformControllerList[i].GetXPosition()+width < DESTROY_POS) {
                // print("Destroy");
                // print("index Destroy: " + i);
                PlatformControllerList[i].DestroySelf();
                PlatformControllerList.Remove(PlatformControllerList[i]);
                i--;
            }
        } 
    }   

    private class PlatformController {
        public Transform platform;
        public Tilemap platformTilemap;

        public PlatformController(Transform platform, Tilemap platformTilemap) {
            this.platform = platform;
            this.platformTilemap = platformTilemap;
        }

        public void Move() {
            platform.position += new Vector3(-1,0,0) * Level.movementSpeed * Time.deltaTime;
        }

        public float GetXPosition() {
            return platform.position.x;
        }

        public int  GetWidth() {
            return  platformTilemap.size.x;
        }


        public void DestroySelf() {
            Destroy(platform.gameObject);
            // Destroy(platformTilemap.gameObject);
        }

    } 

    
}
