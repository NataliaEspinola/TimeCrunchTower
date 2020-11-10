using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private bool isActive;
    private bool isShakingRight = false;
    public bool fall = false;
    public float fallDistance;

    void Update()
    {
        if (isActive)
        {
            Shake();
        }
        if (fall)
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

    private void Shake()
    {
        if (isShakingRight)
        {
            transform.Translate(new Vector2(0.2f, 0f));
        }
        else
        {
            transform.Translate(new Vector2(-0.2f, 0f));
        }
        isShakingRight = !isShakingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fall = true;
        }
    }
}
