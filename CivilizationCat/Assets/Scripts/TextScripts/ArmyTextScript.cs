using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArmyTextScript : MonoBehaviour
{
    public GameObject[] army_texts;

    private static TextMeshProUGUI infantry_text;
    private static TextMeshProUGUI knights_text;
    private static TextMeshProUGUI siege_text;

    void Awake()
    {
        infantry_text = army_texts[0].GetComponent<TextMeshProUGUI>();
        knights_text = army_texts[1].GetComponent<TextMeshProUGUI>();
        siege_text = army_texts[2].GetComponent<TextMeshProUGUI>();

    }
    public static void UpdateTexts()
    {
        infantry_text.text = Main.Government.governments[0].infantry.ToString();
        knights_text.text = Main.Government.governments[0].knights.ToString();
        siege_text.text = Main.Government.governments[0].siege.ToString();
    }
}
