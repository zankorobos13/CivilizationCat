using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldColorChangeScript : MonoBehaviour
{
    public static Color[] colors = new Color[] { Color.white, Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan };

    void Update()
    {
        GameObject Child = gameObject.transform.Find("FieldSprite").gameObject;

        for (int i = 0; i < MapGenerationScript.fields.GetLength(0); i++)
        {
            for (int j = 0; j < MapGenerationScript.fields.GetLength(1); j++)
            {
                if (gameObject.transform.position == MapGenerationScript.fields[i, j].transform.position)
                {
                    Child.GetComponent<SpriteRenderer>().color = colors[Main.map[i, j].government.id];

                }
            }
        }
    }
}