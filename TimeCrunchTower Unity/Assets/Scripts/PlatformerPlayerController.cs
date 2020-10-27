using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlatformerPlayerController : CharactersBase
{

    //Layers
    public LayerMask groundLayer;
    public LayerMask stairsLayer;
    
    //components
    private Rigidbody2D rb;
    public Text hpUi;

    //states
    private bool grounded = false;
    private bool inStairs = false;

    public TimerScript timer;

    //properties
    public int speed = 5;
    public int jumpStrenght = 10;
    public int platformingHealth = 3;
    public float invulnerableTime = 1; //timepo de invulnerabilidad para que las trampas no lo destruyan al tocarlo

    //power ups
    public float speedPwrUpTime = 0;
    public float jumpPwrUpTime = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = 3;
    }

    private void FixedUpdate()
    {
        CheckPlayerGrounded();
    }

    void Update()
    {
        CheckDeath();
        UpdateUiHp();
        CheckPowerUps();
        CheckInvulnerable();
        PlayerMovement();
    }

    private void CheckDeath()
    {
        if (platformingHealth <= 0 || timer.timeLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateUiHp()
    {
        hpUi.text = platformingHealth.ToString();
    }

    private void CheckInvulnerable()
    {
        if (invulnerableTime > 0)
        {
            invulnerableTime -= Time.deltaTime;
        }
    }

    private void CheckPowerUps()
    {
        if (speedPwrUpTime > 0)
        {
            speed = 10;
            speedPwrUpTime -= Time.deltaTime;
        }
        else
        {
            speed = 5;
        }

        if (jumpPwrUpTime > 0)
        {
            jumpStrenght = 15;
            jumpPwrUpTime -= Time.deltaTime;
        }
        else
        {
            jumpStrenght = 10;
        }
    }

    private void CheckPlayerGrounded()
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x-0.1f, transform.position.y-1), new Vector2(transform.position.x+0.1f, transform.position.y-1.1f), groundLayer);
    }

    private void PlayerMovement()
    {
        if (!inStairs)
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
        }
        //Move Left
        if (Input.GetKey("a"))
        {
            distanceToMove += (new Vector2(-1, 0)) * Time.deltaTime * speed;
        }
        transform.Translate(distanceToMove);
        //Jump
        if ((Input.GetKeyDown("w") && grounded) || (Input.GetKey("w") && inStairs))
        {
            rb.velocity = (new Vector2(0, 1) * jumpStrenght);
        }
        if (Input.GetKey("s") && inStairs)
        {
            rb.velocity = (new Vector2(0, -1) * jumpStrenght);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Hardcodeado el 9 del layer, encontrar una solución dsp
        if (collision.gameObject.layer == 9)
        {
            inStairs = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Hardcodeado el 9 del layer, encontrar una solución dsp
        if (collision.gameObject.layer == 9)
        {
            inStairs = false;
        }
    }

}
