using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldColorChangeScript : MonoBehaviour
{
    public static Color[] colors = new Color[] { Color.white, Color.green, Color.red, Color.blue, Color.yellow, Color.magenta, Color.cyan };
    private GameObject landscape;

    public GameObject plain;
    public GameObject forest;
    public GameObject mountain;
    public GameObject fortress;
    public GameObject city;

    void Start()
    {
        landscape = gameObject.transform.Find("FieldSprite").transform.Find("Landscape").gameObject;
    }

    void Update()
    {
        GameObject Child = gameObject.transform.Find("FieldSprite").gameObject;

        for (int i = 0; i < landscape.transform.childCount; i++)
            landscape.transform.GetChild(i).gameObject.SetActive(false);

        for (int i = 0; i < MapGenerationScript.fields.GetLength(0); i++)
        {
            for (int j = 0; j < MapGenerationScript.fields.GetLength(1); j++)
            {
                if (gameObject.transform.position == MapGenerationScript.fields[i, j].transform.position)
                {
                    Child.GetComponent<SpriteRenderer>().color = colors[Main.map[i, j].government.id];

                    switch (Main.map[i, j].landscape)
                    {
                        case FieldClass.Field.Landscape.Plain:
                            plain.SetActive(true);
                            break;
                        case FieldClass.Field.Landscape.Forest:
                            forest.SetActive(true);
                            break;
                        case FieldClass.Field.Landscape.Mountain:
                            mountain.SetActive(true);
                            break;
                        case FieldClass.Field.Landscape.Fortress:
                            fortress.SetActive(true);
                            break;
                        case FieldClass.Field.Landscape.City:
                            city.SetActive(true);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}