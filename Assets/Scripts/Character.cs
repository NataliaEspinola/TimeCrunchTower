using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Classtypes
{
    Hero,
    Berserker,
    Archer,
    Healer,
    Mage
}

public class Character : MonoBehaviour
{
    public string characterName;
    public string description;
    public Classtypes characterClass;
    public int attack;
    public int defense;
    public float maxHealth;
    public float maxMana;
    public int fightSpeed;
    public int distibutablePoints = 5;

    public Sprite sprite;
    public Sprite attackSprite;

    public float health;
    public float mana;

    private SpriteRenderer showSprite;

    private Dictionary<Classtypes, string> Skill1Names = new Dictionary<Classtypes, string>()
    {
        { Classtypes.Berserker, "Neverending Rage" },
        { Classtypes.Hero, "Holy Smite" },
        { Classtypes.Archer, "Quickshot" },
        { Classtypes.Healer, "Mend" },
        { Classtypes.Mage, "Fire Storm" }
    };

    public string Skill1Name()
    {
        return Skill1Names[characterClass];
    }

    void Start()
    {
        health = maxHealth;
        mana = maxMana;
        showSprite = gameObject.AddComponent<SpriteRenderer>();
        showSprite.sprite = sprite;
    }

    private void Update()
    {
    }

    public void ReceiveDamage(float damage)
    {
        health -= System.Math.Max(damage - defense, 0);
        health = System.Math.Max(health, 0);
    }

    public void HealDamage(float heal)
    {
        health += heal;
        health = System.Math.Min(health, maxHealth);
    }

    IEnumerator AttackAnimate()
    {
        showSprite.sprite = attackSprite;
        yield return new WaitForSeconds(0.5f);
        showSprite.sprite = sprite;
    }


    public void Attack(Character enemy)
    {
        StartCoroutine(AttackAnimate());
        float damage = Random.Range(0, attack);
        enemy.ReceiveDamage(damage);
    }

    public bool CanUseSkill1()
    {
        switch (characterClass)
        {
            case Classtypes.Berserker:
                if (health > maxHealth * 0.1) return true;
                break;
            case Classtypes.Hero:
                if (mana >= 5) return true;
                break;
            case Classtypes.Archer:
                if (mana >= 3) return true;
                break;
            case Classtypes.Healer:
                if (mana >= 10) return true;
                break;
            case Classtypes.Mage:
                if (mana >= 20) return true;
                break;
            default:
                return false;
        }
        return false;
    }

    public void Skill1(Character target)
    {
        StartCoroutine(AttackAnimate());
        float damage;
        switch (characterClass)
        {
            case Classtypes.Berserker:
                damage = Random.Range(attack, 3 * attack);
                health -= maxHealth * 0.1f;
                target.ReceiveDamage(damage);
                break;
            case Classtypes.Hero:
                damage = Random.Range(attack, 2 * attack);
                mana -= 5;
                target.ReceiveDamage(damage);
                break;
            case Classtypes.Archer:
                damage = Random.Range(attack, 1.5f * attack);
                mana -= 3;
                target.ReceiveDamage(damage);
                break;
            case Classtypes.Healer:
                damage = Random.Range(attack, 2 * attack);
                mana -= 10;
                target.HealDamage(damage);
                break;
            case Classtypes.Mage:
                damage = Random.Range(1.5f * attack, 3 * attack);
                mana -= 20;
                target.ReceiveDamage(damage);
                break;
        }
    }
    
    
    public bool IsAlive()
    {
        return health > 0;
    }
}
