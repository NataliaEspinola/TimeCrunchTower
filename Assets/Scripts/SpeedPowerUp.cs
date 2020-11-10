using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float powerUpTime = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformerPlayerStateController ps = collision.gameObject.GetComponent<PlatformerPlayerStateController>();
            ps.speedPwrUpTime = powerUpTime;
            Object.Destroy(this.gameObject);
        }
    }
}
