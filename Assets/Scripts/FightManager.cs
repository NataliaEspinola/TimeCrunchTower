using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public Character CharacterPrefab;

    public Button attackButton;
    public Button skill1Button;
    public Text currentTurnText;

    public Sprite firstBossSprite;
    public Sprite firstBossAttackSprite;

    public TimerScript timer;

    public Character[] characters;
    private int currentTurn;
    private bool computerDone;
    private System.Random rdm;

    void SetEnemy(int level)
    {
        characters[3] = Instantiate(CharacterPrefab, new Vector3(6, 3, 0), new Quaternion());
        characters[3].transform.tag = "Enemy";
        switch (level)
        {
            case 1:
                characters[3].characterName = "Kaius Riggs";
                characters[3].characterClass = Classtypes.Mage;
                characters[3].sprite = firstBossSprite;
                characters[3].attackSprite = firstBossAttackSprite;
                characters[3].fightSpeed = 5;
                characters[3].attack = 25;
                characters[3].maxHealth = 600;
                characters[3].maxMana = 200;
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;

        characters = new Character[4];
        characters[0] = Instantiate(CharacterPrefab, new Vector3(-3, -4, 0), new Quaternion());
        StateManager.SetTeamMember(characters[0], 0);
        characters[0].transform.tag = "Player";
        characters[1] = Instantiate(CharacterPrefab, new Vector3(-1, -4, 0), new Quaternion());
        StateManager.SetTeamMember(characters[1], 1);
        characters[1].transform.tag = "Player";
        characters[2] = Instantiate(CharacterPrefab, new Vector3(1, -4, 0), new Quaternion());
        StateManager.SetTeamMember(characters[2], 2);
        characters[2].transform.tag = "Player";

        SetEnemy(StateManager.currentLevel);

        System.Array.Sort(
            characters,
            delegate (Character c1, Character c2)
            { return -1 * c1.fightSpeed.CompareTo(c2.fightSpeed); }
        );
        currentTurn = 0;
        rdm = new System.Random();
        computerDone = true;
        timer.timeLeft = StateManager.timeLeft;

        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckEndGame();
        currentTurnText.text = characters[currentTurn].characterName;
        skill1Button.GetComponentInChildren<Text>().text = characters[currentTurn].Skill1Name();
        if (characters[currentTurn].tag == "Player")
        {
            PlayerTurn();
        }
        else
        {
            if (computerDone)
            {
                StartCoroutine(ComputerTurn());
            }
        }
    }

    void CheckEndGame()
    {
        if (timer.timeLeft <= 0) ComputerVictory();
        List<Character> players = new List<Character>();
        foreach (var ch in characters)
        {
            if (ch.IsAlive() && ch.tag == "Player") players.Add(ch);
        }
        if (players.ToArray().Length == 0) ComputerVictory();
        if (!GameObject.FindWithTag("Enemy").GetComponent<Character>().IsAlive()) PlayerVictory();
    }

    void ComputerVictory()
    {
        StateManager.currentLevel = 1;
        SceneManager.LoadScene("Menu");
    }

    void PlayerVictory()
    {
        timer.timeLeft += StateManager.currentLevel * 300;
        StateManager.currentLevel += 1;
        StateManager.timeLeft = timer.timeLeft;
        SceneManager.LoadScene(StateManager.NextLevel());
    }

    void PlayerTurn()
    {
        if (!characters[currentTurn].IsAlive()) NextTurn();
        if (characters[currentTurn].CanUseSkill1()) skill1Button.interactable = true;
        attackButton.interactable = true;
    }

    IEnumerator ComputerTurn()
    {
        computerDone = false;
        yield return new WaitForSeconds(2);
        if (rdm.NextDouble() >= 0.7 && characters[currentTurn].CanUseSkill1())
        {
            Skill1();
        }
        Attack();
        NextTurn();
        computerDone = true;
    }

    public void ClickAttack()
    {
        Attack();
        attackButton.interactable = false;
        skill1Button.interactable = false;
        NextTurn();
    }

    public void ClickSkill1()
    {
        Skill1();
        attackButton.interactable = false;
        skill1Button.interactable = false;
        NextTurn();
    }

    void Skill1()
    {

        Character target;
        if (characters[currentTurn].characterClass == Classtypes.Healer)
        {
            target = GetAlly();
        }
        else
        {
            target = GetEnemy();
        }
        characters[currentTurn].Skill1(target);
    }

    void Attack()
    {
        Character enemy = GetEnemy();
        characters[currentTurn].Attack(enemy);
    }

    void NextTurn()
    {
        currentTurn = (currentTurn + 1) % 4;
    }

    Character GetAlly()
    {
        if (characters[currentTurn].tag == "Enemy")
        {
            return GameObject.FindWithTag("Enemy").GetComponent<Character>();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            List<GameObject> enemiesList = new List<GameObject>();
            foreach (var ene in enemies)
            {
                if (ene.GetComponent<Character>().IsAlive())
                {
                    enemiesList.Add(ene);
                }
            }
            enemies = enemiesList.ToArray();
            return enemies[rdm.Next(enemies.Length)].GetComponent<Character>();
        }
    }

    Character GetEnemy()
    {
        if (characters[currentTurn].tag == "Player")
        {
            return GameObject.FindWithTag("Enemy").GetComponent<Character>();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            List<GameObject> enemiesList = new List<GameObject>();
            foreach (var ene in enemies)
            {
                if (ene.GetComponent<Character>().IsAlive())
                {
                    enemiesList.Add(ene);
                }
            }
            enemies = enemiesList.ToArray();
            return enemies[rdm.Next(enemies.Length)].GetComponent<Character>();
        }
    }
}
