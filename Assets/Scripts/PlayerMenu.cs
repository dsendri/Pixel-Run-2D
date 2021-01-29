using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMenu : MonoBehaviour
{
    private Rigidbody2D rb;
    private enum State {idle, running, jumping, falling, death};
    private State state = State.idle;
    private Animator anim;
    private Collider2D coll;
    private float fixedDeltaTime;
    [SerializeField] private LayerMask ground;
    public static PlayerMenu instance;
    public event EventHandler OnStartedPlaying;
    public event EventHandler Player_OnDied; 

    public static PlayerMenu GetInstance() {
        return instance;
    }

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        instance = this;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        state = State.running;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(ground)) {
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            state = State.jumping;
            if( OnStartedPlaying != null) 
            {  
                print("listener");
                OnStartedPlaying(this, EventArgs.Empty);
            }
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && coll.IsTouchingLayers(ground)) {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;
        }
        StateSwitch();
        anim.SetInteger("state", (int)state);
        // print("state: " + (int)state);
    }

    private void StateSwitch()
    {
        if (state == State.jumping) {
            if (rb.velocity.y < .1f) {
                state = State.falling;
            }
        }

        if (state == State.falling) {
            if (coll.IsTouchingLayers(ground)) {
                state = State.running;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle") {
            print("Hit");
            rb.bodyType = RigidbodyType2D.Static;
            state = State.death;
            Player_OnDied(this, EventArgs.Empty);
            
        }
    }

    private void Death() {
        // SceneManager.LoadScene("MainScene");
    }
}
