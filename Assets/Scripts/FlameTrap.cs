using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{

    public float sizeTimer = 1;
    public float sizeScale = 1;
    private float sizeTimerStartingValue;
    private bool isEnlarging; // false = shrink, true = enlarge

    // Start is called before the first frame update
    void Start()
    {
        isEnlarging = false;
        sizeTimerStartingValue = sizeTimer;
    }

    // Update is called once per frame
    void Update()
    {
        ScaleAndMove();
    }

    private void ScaleAndMove()
    {
        sizeTimer -= Time.deltaTime;
        if (isEnlarging)
        {
            transform.localScale -= new Vector3(sizeScale, sizeScale, sizeScale) * Time.deltaTime * sizeTimer;
            transform.Translate(new Vector2(0, -Time.deltaTime * sizeTimer * sizeScale));
        }
        else
        {
            transform.localScale += new Vector3(sizeScale, sizeScale, sizeScale) * Time.deltaTime * sizeTimer;
            transform.Translate(new Vector2(0, Time.deltaTime * sizeTimer * sizeScale));
        }
        if (sizeTimer <= 0)
        {
            isEnlarging = !isEnlarging;
            sizeTimer = sizeTimerStartingValue;
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
