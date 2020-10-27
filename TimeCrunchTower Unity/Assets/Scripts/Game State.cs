using UnityEngine;

public class CharData
{
    public  string characterName;
    public  string description;
    public  string cls = "Generic";
    public  float atk;
    public  float defense;
    public  float health;
    public  float maxHealth;
    public  float maxMana;
    public  float mana;
    public  string skill = "Generic";
    public  int distibutablePoints = 5;
    public int turnPriority;
    public Sprite sprite;
}

public static class GameState
{
    public static float timeLeft;
    public static CharData[] Team = new CharData[2];

    public static void AddTeamMember(CharactersBase ch, int index)
    {
        CharData tmp = new CharData();
        tmp.characterName = ch.characterName;
        tmp.description = ch.description;
        tmp.cls = ch.cls;
        tmp.atk = ch.atk;
        tmp.defense = ch.defense;
        tmp.health = ch.health;
        tmp.maxHealth = ch.maxHealth;
        tmp.maxMana = ch.maxMana;
        tmp.mana = ch.mana;
        tmp.skill = ch.skill;
        tmp.distibutablePoints = ch.distibutablePoints;
        tmp.turnPriority = ch.turnPriority;
        tmp.sprite = ch.sprite;

        Team[index] = tmp;
    }

    public static void SetTeamMember(CharactersBase ch, int index)
    {
        Vector2 pos;
        if (index == 0)
        {
            pos = new Vector2(1, -4);
        } else
        {
            pos = new Vector2(-2, -4);
        }
        ch.characterName = Team[index].characterName;
        ch.description = Team[index].description;
        ch.cls = Team[index].cls;
        ch.atk = Team[index].atk;
        ch.defense = Team[index].defense;
        ch.health = Team[index].health;
        ch.maxHealth = Team[index].maxHealth;
        ch.maxMana = Team[index].maxMana;
        ch.mana = Team[index].mana;
        ch.skill = Team[index].skill;
        ch.distibutablePoints = Team[index].distibutablePoints;
        ch.turnPriority = Team[index].turnPriority;
        ch.sprite = Team[index].sprite;
        ch.pos = pos;
        ch.isControllable = true;
        ch.isFight = true;
    }
}
