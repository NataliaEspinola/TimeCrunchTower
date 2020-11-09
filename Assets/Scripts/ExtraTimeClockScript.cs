using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTimeClockScript : MonoBehaviour
{
    private TimerScript timer;

    private void Start()
    {
        timer = GameObject.Find("Timer").GetComponent<TimerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer.timeLeft += 30;
            Object.Destroy(gameObject);
        }
    }
}
