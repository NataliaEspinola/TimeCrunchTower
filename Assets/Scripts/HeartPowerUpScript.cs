using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUpScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlatformerPlayerController pm = collision.gameObject.GetComponent<PlatformerPlayerController>();
            if (pm.platformingHealth < 3)
            {
                pm.platformingHealth += 1;
            }
            
            Object.Destroy(this.gameObject);
        }
    }

}
