using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerSpriteController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite jumpingSprite;
    public Sprite idleSprite;

    public bool isLookingRight = true; //false es izquierda, true es derecha

    private PlatformerPlayerStateController states;

    public void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        states = GetComponent<PlatformerPlayerStateController>();
    }

    private void Update()
    {
        UpdateSpriteDirection();
        UpdateJumpingSprite();
    }

    public void UpdateSpriteDirection()
    {
        if (isLookingRight)
        {
            spriteRenderer.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            spriteRenderer.transform.localScale = new Vector2(-1, 1);
        }
    }

    public void UpdateJumpingSprite()
    {
        if (states.grounded)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else
        {
            spriteRenderer.sprite = jumpingSprite;
        }
    }
}
