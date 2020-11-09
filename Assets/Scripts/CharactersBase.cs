using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBase : MonoBehaviour
{    
    public string characterName;
    public string description;
    public string cls = "Generic";
    public float atk;
    public float defense;
    public float health;
    public float maxHealth;
    public float maxMana;
    public float mana;
    public string skill = "Generic";
    public bool isControllable;
    public bool isFight;
    public int distibutablePoints = 5;
    public Sprite sprite;

    public int turnPriority;

    public Vector2 pos;

    private GameManager gm;

    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        if (isFight)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            transform.position = pos;
            gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            RegisterToManager();
        }
    }

    private void Update()
    {
        if (isFight)
        {
            RegisterToManager();
        }
    }

    void RegisterToManager()
    {
        while (!gm.Registered(this))
        {
            gm.RegisterCharacter(this);
        }
    }

    public void ReceiveDamage(float damage)
    {
        health -= System.Math.Max(damage - defense, 0);
        health = System.Math.Max(health, 0);
    }

    public void Attack(CharactersBase enemy)
    {
        float damage = Random.Range(0, atk);
        enemy.ReceiveDamage(damage);
    }

    public void Skill1(CharactersBase target) {
      if (cls == "Generic"){
        float damage = Random.Range(atk, 3 * atk);
        target.ReceiveDamage(damage);
      }
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
