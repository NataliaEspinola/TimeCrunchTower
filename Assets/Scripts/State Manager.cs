using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
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
}

public static class StateManager
{

    public static float timeLeft;

    private static Dictionary<int, string> levels = new Dictionary<int, string>()
    {
        {1, "Platformer Scene" },
        {2, "Platformer Scene 2" }
    };

    public static string NextLevel()
    {
        return levels[currentLevel];
    }

    public static CharacterData[] Team = new CharacterData[3];

    public static int currentLevel = 1;

    public static void AddTeamMember(Character ch, int index)
    {
        CharacterData tmp = new CharacterData();
        tmp.characterName = ch.characterName;
        tmp.description = ch.description;
        tmp.characterClass = ch.characterClass;
        tmp.attack = ch.attack;
        tmp.defense = ch.defense;
        tmp.maxHealth = ch.maxHealth;
        tmp.maxMana = ch.maxMana;
        tmp.fightSpeed = ch.fightSpeed;
        tmp.distibutablePoints = ch.distibutablePoints;
        tmp.sprite = ch.sprite;
        tmp.attackSprite = ch.attackSprite;

        Team[index] = tmp;
    }

    public static void SetTeamMember(Character ch, int index)
    {
        ch.characterName = Team[index].characterName;
        ch.description = Team[index].description;
        ch.characterClass = Team[index].characterClass;
        ch.attack = Team[index].attack;
        ch.defense = Team[index].defense;
        ch.maxHealth = Team[index].maxHealth;
        ch.maxMana = Team[index].maxMana;
        ch.fightSpeed = Team[index].fightSpeed;
        ch.distibutablePoints = Team[index].distibutablePoints;
        ch.sprite = Team[index].sprite;
        ch.attackSprite = Team[index].attackSprite;
    }
}
