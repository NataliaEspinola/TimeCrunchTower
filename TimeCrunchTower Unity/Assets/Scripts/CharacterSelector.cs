using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public Image[] Selections;
    public CharactersBase[] Team = new CharactersBase[2];
    private int positionTeam = 0;
    public Button btnWhite;
    public Button btnGreen;
    public Button btnBlue;
    public Button plusAtk;
    public Button plusDef;
    public Button play;
    public CharactersBase green;
    public CharactersBase blue;
    public CharactersBase white;
    public CharactersBase[] AllCharacters = new CharactersBase[3];
    public Sprite blueSprite, whiteSprite, greenSprite;
    public Image image1, image2;
    public Text nameText, desText, classText, atkText, defText, healthText, manaText, skillText, pointsText;
    private CharactersBase selectedChar;
    public string nextSceneName;

    void Start()
    {
        //green = initializeCharacter("Green", "description", 10, 10, 10, 10);
        //blue = initializeCharacter("Blue", "description", 10, 10, 10, 10);
        //white = initializeCharacter("White", "description", 10, 10, 10, 10);
        Team[0] = null;
        Team[1] = null;
        AllCharacters[0] = white;
        AllCharacters[1] = green;
        AllCharacters[2] = blue;
        plusAtk.interactable = false;
        plusDef.interactable = false;
        play.interactable = false;
    }

    void Update()
    {
        if (!(Team[0] is null) && !(Team[1] is null)) play.interactable = true;
        if (!(selectedChar is null))
        {
            ShowInf();
            if (selectedChar.distibutablePoints <= 0)
            {
                plusAtk.interactable = false;
                plusDef.interactable = false;
            } else
            {
                plusAtk.interactable = true;
                plusDef.interactable = true;
            }
        }
    }
    
    public void addTeam(int index) // añade al team el personaje seleccionado
    {
        selectedChar = AllCharacters[index];
        if (positionTeam == 0)
        {
            Team[0] = AllCharacters[index];
            if (index == 0)
            {
                image1.sprite = whiteSprite;

            }
            else if (index == 1)
            {
                image1.sprite = greenSprite;
            }
            else
            {
                image1.sprite = blueSprite;
            }
            positionTeam = 1;
        }
        else
        {
            Team[1] = AllCharacters[index];
            if (index == 0)
            {
                image2.sprite = whiteSprite;

            }
            else if (index == 1)
            {
                image2.sprite = greenSprite;
            }
            else
            {
                image2.sprite = blueSprite;
            }
            positionTeam = 0;
        }

    }
    public void buttonWhite()
    {
        addTeam(0);
    }
    public void buttonGreen()
    {
        addTeam(1);
    }
    public void buttonBlue()
    {
        addTeam(2);
    }
    public void PlusAtk()
    {
        Debug.Log(selectedChar.characterName);
        selectedChar.atk += 1;
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

    //nameText, desText, classText, skillText, atkText, defText, healthText, manaText;
    private void ShowInf()
	{
        nameText.text = selectedChar.characterName;
        desText.text = selectedChar.description;
        classText.text = selectedChar.cls;
        atkText.text = selectedChar.atk.ToString();
        defText.text = selectedChar.defense.ToString();
        healthText.text = selectedChar.maxHealth.ToString();
        manaText.text = selectedChar.maxMana.ToString();
        skillText.text = selectedChar.skill;
        pointsText.text = selectedChar.distibutablePoints.ToString();
    }
    public void BlueInf()
	{
        selectedChar = AllCharacters[2];
    }
    public void GreenInf()
	{
        selectedChar = AllCharacters[1];
    }
    public void WhiteInf()
	{
        selectedChar = AllCharacters[0];
    }
    public void FirstInfTeam()
	{
        selectedChar = Team[0];
    }
    public void SecondInfTeam()
    {
        selectedChar = Team[1];
    }

    public void StartGame()
    {
        GameState.AddTeamMember(Team[0], 0);
        GameState.AddTeamMember(Team[1], 1);
        SceneManager.LoadScene(nextSceneName);
    }
}
