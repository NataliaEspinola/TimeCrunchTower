using System;
using UnityEngine;
public class FallingSpikeTrap : MonoBehaviour
{
    public float fallDistance;
    public float shakeTime;
    private bool lastShakeDireciton;
    private bool isActive;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            ShakeAndFall();
        }
    }

    private void ShakeAndFall()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            lastShakeDireciton = !lastShakeDireciton;
            if (lastShakeDireciton)
            {
                transform.Translate(0.1f, 0, 0);
            }
            else
            {
                transform.Translate(-0.1f, 0, 0);
            }
        }
        if (shakeTime <= 0)
        {
            if (fallDistance >= 0)
            {
                fallDistance -= 0.1f;
                transform.Translate(0, -0.1f, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformerPlayerController pc = collision.gameObject.GetComponent<PlatformerPlayerController>();
            PlatformerPlayerStateController ps = collision.gameObject.GetComponent<PlatformerPlayerStateController>();
            if (ps.invulnerableTime <= 0)
            {
                pc.platformingHealth -= 1;
                ps.invulnerableTime = 1;
            }

            Rigidbody2D rb = pc.GetComponent<Rigidbody2D>();
            rb.velocity += new Vector2(0, 1) * pc.jumpStrenght / 2;
        }
    }
}
