using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloor : MonoBehaviour
{

    private float destroyTime = 2f;
    public bool beingDestroyed = false;

    private float destroySpeed;

    private void Update()
    {
        if (beingDestroyed)
        {
            transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
            destroyTime -= Time.deltaTime;
            if (destroyTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartDestroying()
    {
        beingDestroyed = true;
    }
}
