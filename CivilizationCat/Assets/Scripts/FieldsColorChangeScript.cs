using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsColorChangeScript : MonoBehaviour
{
    public static Color32[] colors;
    public static int player_color_id = 5;

    void Start()
    {
        colors = new Color32[] { Color.white, new Color32(186, 255, 201, 255), new Color32(255, 180, 187, 255), new Color32(117, 200, 204, 255), new Color32(255, 254, 187, 255), new Color32(255, 224, 186, 255) };

        Color32 temp = colors[1];
        colors[1] = colors[player_color_id];
        colors[player_color_id] = temp;
    }

    public static void UpdateMap()
    {
        for (int i = 0; i < MapGenerationScript.fields.GetLength(0); i++)
        {
            for (int j = 0; j < MapGenerationScript.fields.GetLength(1); j++)
            {
                GameObject FieldSprite = MapGenerationScript.fields[i, j].transform.Find("FieldSprite").gameObject;
                GameObject Landscape = FieldSprite.transform.Find("Landscape").gameObject;

                for (int a = 0; a < Landscape.transform.childCount; a++)
                    Landscape.transform.GetChild(a).gameObject.SetActive(false);

                if (Main.map[i, j].government != null)
                    FieldSprite.GetComponent<SpriteRenderer>().color = colors[Main.map[i, j].government.id];
                else
                    FieldSprite.GetComponent<SpriteRenderer>().color = colors[0];


                switch (Main.map[i, j].landscape)
                {
                    case FieldClass.Field.Landscape.Plain:
                        Landscape.transform.Find("Plain2").gameObject.SetActive(true);
                        break;
                    case FieldClass.Field.Landscape.Forest:
                        Landscape.transform.Find("Forest2").gameObject.SetActive(true);
                        break;
                    case FieldClass.Field.Landscape.Mountain:
                        Landscape.transform.Find("Mountain1").gameObject.SetActive(true);
                        break;
                    case FieldClass.Field.Landscape.Fortress:
                        Landscape.transform.Find("Fortress1").gameObject.SetActive(true);
                        break;
                    case FieldClass.Field.Landscape.City:
                        Landscape.transform.Find("City3").gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
                
            }
        }
    }
}
