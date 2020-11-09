using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCamaraMovement : MonoBehaviour
{

    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (playerTransform.position.y > 0)
        {
            transform.position = new Vector3(0, playerTransform.position.y, -1);
        }
        
    }
}
