using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{

    public Sprite explodedBombSprite;


    public float explosionXScale = 5;
    public float explosionYScale = 5;
    public float explodeTimer = 3;
    public bool tryingToBlow = false;
    private bool exploded = false;
    private float timeToDelete = 1;

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (exploded)
        {
            timeToDelete -= Time.deltaTime;
            if (timeToDelete <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (tryingToBlow)
        {
            GetReadyToBlow();
        }
        
    }

    void GetReadyToBlow()
    {
        explodeTimer -= Time.deltaTime;
        if (explodeTimer <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        animator.enabled = false;
        spriteRenderer.sprite = explodedBombSprite;
        Collider2D[] explodedObjects = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(explosionXScale, explosionYScale), 0);
        foreach (Collider2D c2d in explodedObjects)
        {
            if (c2d.gameObject.CompareTag("Player"))
            {
                DamagePlayer(c2d);
            }
            else if (c2d.gameObject.CompareTag("BreakableFloor"))
            {
                BreakableFloor(c2d);
            }
        }
        exploded = true;
    }

    private void DamagePlayer(Collider2D playerCollider)
    {
        GameObject player = playerCollider.gameObject;
        PlatformerPlayerController pc = player.GetComponent<PlatformerPlayerController>();
        PlatformerPlayerStateController ps = player.GetComponent<PlatformerPlayerStateController>();
        AudioSource playerAudioSource = player.GetComponent<AudioSource>();
        if (ps.invulnerableTime <= 0)
        {
            pc.platformingHealth -= 1;
            ps.invulnerableTime = 1;
        }

        Rigidbody2D rb = pc.GetComponent<Rigidbody2D>();
        rb.velocity += new Vector2(0, 1) * pc.jumpStrenght / 2;
        playerAudioSource.Play();
    }

    private void BreakableFloor(Collider2D c2d)
    {
        BreakableFloor bf = c2d.gameObject.GetComponent<BreakableFloor>();
        bf.StartDestroying();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.enabled = true;
            tryingToBlow = true;
        }
    }
}
