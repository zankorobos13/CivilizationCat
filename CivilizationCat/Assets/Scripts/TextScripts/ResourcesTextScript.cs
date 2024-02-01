using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesTextScript : MonoBehaviour
{
    public GameObject[] resource_texts;

    private static TextMeshProUGUI population_text;
    private static TextMeshProUGUI food_text;
    private static TextMeshProUGUI materials_text;
    private static TextMeshProUGUI jewelry_text;

    void Awake()
    {
        population_text = resource_texts[0].GetComponent<TextMeshProUGUI>();
        food_text = resource_texts[1].GetComponent<TextMeshProUGUI>();
        materials_text = resource_texts[2].GetComponent<TextMeshProUGUI>();
        jewelry_text = resource_texts[3].GetComponent<TextMeshProUGUI>();

    }
    public static void UpdateTexts()
    {
        population_text.text = Main.Government.governments[0].population.ToString();
        food_text.text = Main.Government.governments[0].food.ToString();
        materials_text.text = Main.Government.governments[0].materials.ToString();
        jewelry_text.text = Main.Government.governments[0].jewelry.ToString();
    }

 
}
