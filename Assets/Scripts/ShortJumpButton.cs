﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ShortJumpButton : MonoBehaviour
{

    public static ShortJumpButton instance;
    public event EventHandler OnClickJump;
    Button Jump;

    public static ShortJumpButton GetInstance() {
        return instance;
    }

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {   
        Jump = GetComponent<Button>();
        // Jump.onClick.AddListener(JumpClick);

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>( );
          
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry( );
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener( ( data ) => { JumpClick( (PointerEventData)data ); } );
        eventTrigger.triggers.Add(pointerDownEntry);
    }

    void JumpClick( PointerEventData data ) {
        OnClickJump(this, EventArgs.Empty);
    }

}