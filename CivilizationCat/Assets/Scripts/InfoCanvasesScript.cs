using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoCanvasesScript : MonoBehaviour
{
    public GameObject[] button_info_canvases;
    public GameObject field_info_canvas;
    public static bool isNeedToUpdate = false;
    void Start()
    {
        field_info_canvas.SetActive(false);
        for (int i = 0; i < button_info_canvases.Length; i++)
            button_info_canvases[i].SetActive(false);
    }


    public void Update()
    {
        if (isNeedToUpdate && Main.coords[0] == -1)
        {
            int number_of_action = 0;

            for (int i = 0; i < Main.Government.actions.Length; i++)
            {
                if (Main.Government.actions[i] == Main.choosen_action)
                {
                    number_of_action = i;
                    break;
                }
            }

            if (number_of_action != 0)
            {
                field_info_canvas.SetActive(false);
                for (int i = 0; i < button_info_canvases.Length; i++)
                    button_info_canvases[i].SetActive(false);

                button_info_canvases[number_of_action - 1].SetActive(true);
            }
        }
        else if (isNeedToUpdate && Main.coords[0] != -1)
        {
            for (int i = 0; i < button_info_canvases.Length; i++)
                button_info_canvases[i].SetActive(false);
            field_info_canvas.SetActive(true);

            GameObject header_text = field_info_canvas.transform.Find("Header_text").gameObject;
            GameObject main_info_text = field_info_canvas.transform.Find("Main_info_text").gameObject;

            header_text.GetComponent<TextMeshProUGUI>().text = "���� (" + Main.coords[0] + "; " + Main.coords[1] + ")";
            FieldClass.Field field = Main.map[Main.coords[0], Main.coords[1]];
            string landscape = "";

            switch (field.landscape)
            {
                case FieldClass.Field.Landscape.Plain:
                    landscape = "�������";
                    break;
                case FieldClass.Field.Landscape.Forest:
                    landscape = "���";
                    break;
                case FieldClass.Field.Landscape.Mountain:
                    landscape = "����";
                    break;
                case FieldClass.Field.Landscape.Fortress:
                    landscape = "��������";
                    break;
                case FieldClass.Field.Landscape.City:
                    landscape = "�����";
                    break;
                default:
                    break;
            }

            if (field.government == null)
            {
                header_text.GetComponent<TextMeshProUGUI>().color = Color.white;
                main_info_text.GetComponent<TextMeshProUGUI>().text = "�����������\n���������: " + landscape;
            }
            else
            {
                header_text.GetComponent<TextMeshProUGUI>().color = FieldsColorChangeScript.colors[field.government.id];
                main_info_text.GetComponent<TextMeshProUGUI>().text = "�����������: " + field.government.id + "\n���������: " + landscape;
            }
        }

        isNeedToUpdate = false;
    }
}
