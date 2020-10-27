using UnityEngine;
using System.Collections;

public class Progress : MonoBehaviour
{
    public enum ProgressType
    {
        Health,
        Mana
    }
    
    public Vector2 pos;
    public Vector2 size;
    private Texture2D foregroundTex;
    private Texture2D backgroundTex;
    private CharactersBase character;
    public ProgressType type;

    private string progressText;
    private float barDisplay; //current progress

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), backgroundTex, GUIStyle.none);
        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), foregroundTex, GUIStyle.none);
        GUI.EndGroup();
        GUI.BeginGroup(new Rect(0, 0, size.x, size.y));
        GUI.Label(new Rect(0, 0, size.x, size.y), progressText);
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
        character = this.GetComponent<CharactersBase>();
        foregroundTex = MakeTex((int) size.x, (int) size.y, GetForegroundColor());
        backgroundTex = MakeTex((int)size.x, (int)size.y, Color.black);
    }

    void Update()
    {
        GetProgressText();
        GetProgressPercentage();
    }
}