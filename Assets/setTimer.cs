using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTimer : MonoBehaviour
{
    public TimerScript timer;
    // Start is called before the first frame update
    void Start()
    {
        if (StateManager.currentLevel > 1)
        {
            timer.timeLeft = StateManager.timeLeft;
        }
    }
}
