using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlatformerPlayerController pm = collision.gameObject.GetComponent<PlatformerPlayerController>();
            pm.speedPwrUpTime = 30;
            Object.Destroy(this.gameObject);
        }
    }
}
