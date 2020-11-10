using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerStateController : MonoBehaviour
{

    //Layers
    public LayerMask groundLayer;
    public LayerMask stairsLayer;

    //states
    public bool grounded = false;
    public bool inStairs = false;

    //power ups
    public float speedPwrUpTime = 0;
    public float jumpPwrUpTime = 0;
    public float iceCubeSlow = 0;

    public float invulnerableTime = 1; //timepo de invulnerabilidad para que las trampas no lo destruyan al tocarlo

    public void CheckPlayerGrounded()
    {
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.1f, transform.position.y - 1), new Vector2(transform.position.x + 0.1f, transform.position.y - 1.1f), groundLayer);
    }


}
