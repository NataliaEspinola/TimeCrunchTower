using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{

    public float minX;
    public float maxX;
    public bool dirIsRight; //true, hacia la derecha, false hacia la izquierda
    public float speed = 1;
    private List<GameObject> thingsToMove;

    private void Start()
    {
        thingsToMove = new List<GameObject>();
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (dirIsRight)
        {
            if (transform.position.x < maxX)
            {
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                foreach (GameObject thing in thingsToMove)
                {
                    thing.transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                }
            }
            else
            {
                dirIsRight = !dirIsRight;
            }
        }
        else
        {
            if (transform.position.x > minX)
            {
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                foreach (GameObject thing in thingsToMove)
                {
                    thing.transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                }
            }
            else
            {
                dirIsRight = !dirIsRight;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        thingsToMove.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        thingsToMove.Remove(collision.gameObject);
    }
}
