using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public Image Selections;
    public Character[] Team = new Character[3];
    private int positionTeam = 0;
    public Button plusAtk;
    public Button plusDef;
    public Button play;
    public Character[] AllCharacters = new Character[5];
    public Image image1, image2, image3;
    private Image[] imagesTeam = new Image[3];
    public Text nameText, desText, classText, atkText, defText, healthText, manaText, skillText, pointsText;
    private Character selectedChar;
    public string nextSceneName;

    void Start()
    {
        imagesTeam[0] = image1;
        imagesTeam[1] = image2;
        imagesTeam[2] = image3;
        plusAtk.interactable = false;
        plusDef.interactable = false;
        play.interactable = false;
    }

    void Update()
    {
        if (!(Team[0] is null) && !(Team[1] is null) && !(Team[2] is null)) play.interactable = true;
        if (!(selectedChar is null))
        {
            ShowInf();
            if (selectedChar.distibutablePoints <= 0)
            {
                plusAtk.interactable = false;
                plusDef.interactable = false;
            }
            else
            {
                plusAtk.interactable = true;
                plusDef.interactable = true;
            }
        }
    }
    private void showSelectedTeam(int positionTeam, int index)
	{
        imagesTeam[positionTeam].sprite = AllCharacters[index].sprite;
        Team[positionTeam] = AllCharacters[index];
    }
    public void addTeam(int index)
	{
        selectedChar = AllCharacters[index];
        showSelectedTeam(positionTeam, index);
        positionTeam++;
        if (positionTeam == 3) { positionTeam = 0; }
    }

    public void buttonBerserker()
    {
        selectedChar = AllCharacters[0];
        addTeam(0);
    }
    public void buttonMage()
    {
        selectedChar = AllCharacters[1];
        addTeam(1);
    }
    public void buttonArcher()
    {
        selectedChar = AllCharacters[2];
        addTeam(2);
    }
    public void buttonHealer()
    {
        selectedChar = AllCharacters[3];
        addTeam(3);
    }
    public void buttonHero()
    {
        selectedChar = AllCharacters[4];
        addTeam(4);
    }
    public void PlusAtk()
    {
        selectedChar.attack += 1;
        MinusPoints();
    }
    public void PlusDef()
    {
        selectedChar.defense += 1;
        MinusPoints();
    }
    private void MinusPoints()
    {
        selectedChar.distibutablePoints -= 1;
    }

    private void ShowInf()
    {
        nameText.text = selectedChar.characterName;
        desText.text = selectedChar.description;
        classText.text = selectedChar.characterClass.ToString();
        atkText.text = selectedChar.attack.ToString();
        defText.text = selectedChar.defense.ToString();
        healthText.text = selectedChar.maxHealth.ToString();
        manaText.text = selectedChar.maxMana.ToString();
        skillText.text = selectedChar.Skill1Name();
        pointsText.text = selectedChar.distibutablePoints.ToString();
        
    }
    public void FirstInfTeam()  
    {
        selectedChar = Team[0];
    }
    public void SecondInfTeam()
    {
        selectedChar = Team[1];
    }
    public void ThirdInfTeam()
    {
        selectedChar = Team[2];
    }

    public void StartGame()
    {
        /*
        GameState.AddTeamMember(Team[0], 0);
        GameState.AddTeamMember(Team[1], 1);
        SceneManager.LoadScene(nextSceneName);
        */
        StateManager.AddTeamMember(Team[0], 0);
        StateManager.AddTeamMember(Team[1], 1);
        StateManager.AddTeamMember(Team[2], 2);
        SceneManager.LoadScene(nextSceneName);
    }
}

