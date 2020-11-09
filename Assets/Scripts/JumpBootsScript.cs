using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBootsScript : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlatformerPlayerController pm = collision.gameObject.GetComponent<PlatformerPlayerController>();
            pm.jumpPwrUpTime = 30;
            Object.Destroy(this.gameObject);
        }
    }
}
