using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentTurn;
    public CharactersBase[] characters;
    public int latestCharacter;
    private System.Random rdm;
    private bool computerDone;
    public UnityEngine.UI.Text currentTurnText;
    public UnityEngine.UI.Button attackButton;
    private TimerScript timer;
    public string firstScene;
    public int timeBonus = 300;
    public UnityEngine.UI.Text winText;
        
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        timer = GetComponent<TimerScript>();
        timer.timeLeft = GameState.timeLeft;
        latestCharacter = 0;
        currentTurn = 0;
        characters = new CharactersBase[latestCharacter];
        rdm = new System.Random();
        computerDone = true;
        GameObject[] playerChars = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerChars.Length; i++)
        {
            CharactersBase tmp = playerChars[i].AddComponent<CharactersBase>();
            GameState.SetTeamMember(tmp, i);
        }
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckEndGame();
        currentTurnText.text = characters[currentTurn].characterName;
        if (characters[currentTurn].isControllable)
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

    public void ClickAttack()
    {
        Attack();
        attackButton.interactable = false;
        NextTurn();
    }

    void CheckEndGame()
    {
        if (timer.timeLeft <= 0) ComputerVictory();
        List<CharactersBase> players = new List<CharactersBase>();
        foreach (var ch in characters)
        {
            if (ch.IsAlive() && ch.isControllable) players.Add(ch);
        }
        if (players.ToArray().Length == 0) ComputerVictory();
        if (!GameObject.FindWithTag("Enemy").GetComponent<CharactersBase>().IsAlive()) PlayerVictory();
    }

    IEnumerator ComputerTurn()
    {
        computerDone = false;
        yield return new WaitForSeconds(2);
        Attack();
        NextTurn();
        computerDone = true;
    }

    void PlayerTurn()
    {
        if (!characters[currentTurn].IsAlive()) NextTurn();
        attackButton.interactable = true;
    }

    CharactersBase GetEnemy()
    {
        if (characters[currentTurn].isControllable)
        {
            return GameObject.FindWithTag("Enemy").GetComponent<CharactersBase>();
        }
        else
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
            List<GameObject> enemiesList = new List<GameObject>();
            foreach (var ene in enemies)
            {
                if (ene.GetComponent<CharactersBase>().IsAlive())
                {
                    enemiesList.Add(ene);
                }
            }
            enemies = enemiesList.ToArray();
            return enemies[rdm.Next(enemies.Length)].GetComponent<CharactersBase>();
        }
    }

    void Attack()
    {
        CharactersBase enemy = GetEnemy();
        characters[currentTurn].Attack(enemy);
    }

    void NextTurn()
    {
        currentTurn = (currentTurn + 1) % latestCharacter;
    }

    void ComputerVictory()
    {
        SceneManager.LoadScene(firstScene);
    }

    void PlayerVictory()
    {
        timer.timeLeft += timeBonus;
        Time.timeScale = 0;
        enabled = false;
        winText.enabled = true;
    }

    public bool Registered(CharactersBase character)
    {
        foreach (var ch in characters)
        {
            if (character == ch) return true;
        }
        return false;
    }

    public void RegisterCharacter(CharactersBase character)
    {
        if (Registered(character)) return;
        CharactersBase[] oldCharacters = characters;
        characters = new CharactersBase[latestCharacter + 1];
        Array.Copy(oldCharacters, characters, oldCharacters.Length);
        characters[latestCharacter] = character;
        Array.Sort(
            characters,
            delegate (CharactersBase c1, CharactersBase c2)
            { return -1 * c1.turnPriority.CompareTo(c2.turnPriority); });
        latestCharacter++;
    }
}
