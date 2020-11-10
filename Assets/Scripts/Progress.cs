using UnityEngine;
using System.Collections;

public class Progress : MonoBehaviour
{
    public enum ProgressType
    {
        Health,
        Mana
    }
    
    private Texture2D foregroundTex;
    private Texture2D backgroundTex;
    private Character character;
    public ProgressType type;

    float sizeX;
    float sizeY;

    private float yMult;

    private string progressText;
    private float barDisplay; //current progress

    void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(character.transform.position);
        sizeX = Screen.width * 0.1f;
        sizeY = Screen.height * 0.05f;
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x - sizeX/2, Screen.height - pos.y - sizeY * yMult, sizeX, sizeY));
        GUI.Box(new Rect(0, 0, sizeX, sizeY), backgroundTex, GUIStyle.none);
        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, sizeX * barDisplay, sizeY));
        GUI.Box(new Rect(0, 0, sizeX, sizeY), foregroundTex, GUIStyle.none);
        GUI.EndGroup();

        GUI.BeginGroup(new Rect(0, 0, sizeX, sizeY));
        GUI.Label(new Rect(0, 0, sizeX, sizeY), progressText);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    Texture2D MakeTex(int width, int height, Color col)
    {
        var pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
        {
            pix[i] = col;
        }

        var result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    void GetProgressText()
    {
        switch (type)
        {
            case ProgressType.Health:
                progressText = character.health.ToString() + "/" + character.maxHealth.ToString();
                break;
            case ProgressType.Mana:
                progressText = character.mana.ToString() + "/" + character.maxMana.ToString();
                break;
            default:
                progressText = "0/0";
                break;
        }
    }

    void GetProgressPercentage()
    {
        switch (type)
        {
            case ProgressType.Health:
                barDisplay = (float) character.health / character.maxHealth;
                break;
            case ProgressType.Mana:
                barDisplay = (float)character.mana / character.maxMana;
                break;
            default:
                barDisplay = 0;
                break;
        }
    }

    Color GetForegroundColor()
    {
        Color color;
        switch (type)
        {
            case ProgressType.Health:
                color = Color.red;
                break;
            case ProgressType.Mana:
                color = Color.blue;
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }

    void Start()
    {
        if (type == ProgressType.Health)
        {
            yMult = 3.5f;
        }
        else
        {
            yMult = 2.5f;
        }

        sizeX = Screen.width * 0.1f;
        sizeY = Screen.height * 0.05f;
        character = this.GetComponent<Character>();
        foregroundTex = MakeTex((int)sizeX, (int)sizeY, GetForegroundColor());
        backgroundTex = MakeTex((int)sizeX, (int)sizeY, Color.black);
    }

    void Update()
    {
        GetProgressText();
        GetProgressPercentage();
    }
}