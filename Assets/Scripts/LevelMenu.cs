using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

using Random=UnityEngine.Random;

public class LevelMenu : MonoBehaviour
{

    public static LevelMenu instance;

    public static LevelMenu GetInstance() {
        return instance;
    }

    void Awake()
    {
        print("Awake"); 
        instance = this;     
    }
    
    private void Start()
    {
    } 
    

}
