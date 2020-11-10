using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : MonoBehaviour
{
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
            rb.velocity += new Vector2(0, 1) * pc.jumpStrenght/2;
        }
    }
}