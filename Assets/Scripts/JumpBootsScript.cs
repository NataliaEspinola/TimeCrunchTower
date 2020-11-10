using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBootsScript : MonoBehaviour
{
    public float powerUpTime = 20;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlatformerPlayerStateController pm = collision.gameObject.GetComponent<PlatformerPlayerStateController>();
            pm.jumpPwrUpTime = powerUpTime;
            Object.Destroy(this.gameObject);
        }
    }
}
