using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private enum State {idle, running, jumping, falling, death};
    private State state = State.idle;
    private Animator anim;
    private Collider2D coll;
    private float fixedDeltaTime;
    [SerializeField] private LayerMask ground;

    public static Player instance;
    public event EventHandler OnStartedPlaying;
    public event EventHandler Player_OnDied; 

    public static Player GetInstance() {
        return instance;
    }

    private void Controller_OnJump(object sender, EventArgs e)
    {
        Jump();
        // SoundManager.PlaySound(SoundManager.Sound.Jump);
    }

    private void Controller_OnShortJump(object sender, EventArgs e)
    {
        ShortJump();
        // SoundManager.PlaySound(SoundManager.Sound.Jump);
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
        JumpButton.GetInstance().OnClickJump += Controller_OnJump;
        ShortJumpButton.GetInstance().OnClickJump += Controller_OnShortJump;
        state = State.running;
        CountGame.Increment();
    }

    private void Update()
    {
            
        if( (Input.GetKeyDown(KeyCode.Space))) {
            Jump();
        } else if((Input.GetKeyDown(KeyCode.UpArrow)) && coll.IsTouchingLayers(ground)) {
            ShortJump();
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
            // print("Hit");
            rb.bodyType = RigidbodyType2D.Static;
            state = State.death;
            Player_OnDied(this, EventArgs.Empty);
            
        } else if (collision.tag == "Coin") {
            print("Coin");
            Level.GetInstance().SetScore();
        }
    }

    private void Death() {
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        // SceneManager.LoadScene("MainScene");
    }

    private void Jump() {
        if (coll.IsTouchingLayers(ground)) {
            SoundManager.PlaySound(SoundManager.Sound.Jump);
            rb.velocity = new Vector2(rb.velocity.x, 14f);
            state = State.jumping;
            if( OnStartedPlaying != null) 
            {  
                // print("listener");
                OnStartedPlaying(this, EventArgs.Empty);
            }
        }
    }

    private void ShortJump() {
        if (coll.IsTouchingLayers(ground)) {
            SoundManager.PlaySound(SoundManager.Sound.Jump);
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            state = State.jumping;
        }
    }
}
