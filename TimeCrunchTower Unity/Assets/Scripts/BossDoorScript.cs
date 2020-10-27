using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorScript : MonoBehaviour
{
    public string nextSceneName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject timerObject = GameObject.FindWithTag("Timer");
            GameState.timeLeft = timerObject.GetComponent<TimerScript>().timeLeft;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
