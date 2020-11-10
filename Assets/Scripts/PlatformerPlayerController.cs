using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformerPlayerController : Character
{

    //Scripts
    private PlatformerPlayerStateController states;
    private PlatformerPlayerSpriteController sprites;
    
    //components
    private Rigidbody2D rb;
    public Text hpUi;

    public TimerScript timer;

    //properties
    public int speed = 5;
    public int jumpStrenght = 10;
    public int platformingHealth = 3;

    private AudioSource audioSource;
    void Start()
    {
        states = GetComponent<PlatformerPlayerStateController>();
        sprites = GetComponent<PlatformerPlayerSpriteController>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        health = 3;
    }

    private void FixedUpdate()
    {
        states.CheckPlayerGrounded();
    }

    void Update()
    {
        CheckDeath();
        UpdateUiHp();
        CheckPowerUps();
        CheckInvulnerable();
        PlayerMovement();
    }

    private void UpdateUiHp()
    {
        hpUi.text = platformingHealth.ToString();
    }
    private void CheckDeath()
    {
        if (platformingHealth <= 0 || timer.timeLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void CheckInvulnerable()
    {
        if (states.invulnerableTime > 0)
        {
            states.invulnerableTime -= Time.deltaTime;
        }
    }
    private void CheckPowerUps()
    {
        if (states.speedPwrUpTime > 0)
        {
            speed = 10;
            states.speedPwrUpTime -= Time.deltaTime;
        }
        else
        {
            speed = 5;
        }

        if (states.jumpPwrUpTime > 0)
        {
            jumpStrenght = 15;
            states.jumpPwrUpTime -= Time.deltaTime;
        }
        else
        {
            jumpStrenght = 10;
        }

        if (states.iceCubeSlow > 0)
        {
            speed /= 2;
            states.iceCubeSlow -= Time.deltaTime;
        }
    }
    private void PlayerMovement()
    {
        if (!states.inStairs)
        {
            rb.gravityScale = 2;
        }
        else
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
        }

        Vector2 distanceToMove = new Vector2(0, 0);
        //Move Right
        if (Input.GetKey("d"))
        {
            distanceToMove += (new Vector2(1, 0)) * Time.deltaTime * speed;
            sprites.isLookingRight = true;
        }
        //Move Left
        if (Input.GetKey("a"))
        {
            distanceToMove += (new Vector2(-1, 0)) * Time.deltaTime * speed;
            sprites.isLookingRight = false;
        }
        transform.Translate(distanceToMove);
        //Jump
        if ((Input.GetKeyDown("w") && states.grounded) || (Input.GetKey("w") && states.inStairs))
        {
            rb.velocity = (new Vector2(0, 1) * jumpStrenght);
        }
        if (Input.GetKey("s") && states.inStairs)
        {
            rb.velocity = (new Vector2(0, -1) * jumpStrenght);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioSource.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hardcodeado el 9 del layer, encontrar una solución dsp
        if (collision.gameObject.layer == 9)
        {
            states.inStairs = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Hardcodeado el 9 del layer, encontrar una solución dsp
        if (collision.gameObject.layer == 9)
        {
            states.inStairs = false;
        }
    }
}
