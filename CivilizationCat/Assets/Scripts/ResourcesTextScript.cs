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

    // Start is called before the first frame update
    void Awake()
    {
        population_text = resource_texts[0].GetComponent<TextMeshProUGUI>();
        food_text = resource_texts[1].GetComponent<TextMeshProUGUI>();
        materials_text = resource_texts[2].GetComponent<TextMeshProUGUI>();
        jewelry_text = resource_texts[3].GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    public static void UpdateTexts()
    {
        population_text.text = Main.Government.governments[0].GetPopulation().ToString();
        food_text.text = Main.Government.governments[0].GetFood().ToString();
        materials_text.text = Main.Government.governments[0].GetMaterials().ToString();
        jewelry_text.text = Main.Government.governments[0].GetJewelry().ToString();
    }

 
}
