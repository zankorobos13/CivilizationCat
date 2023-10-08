using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvasesScript : MonoBehaviour
{
    public GameObject[] canvases;
    public static bool isNeedToUpdate = false;
    void Start()
    {
        GameObject[] cnvs = new GameObject[canvases.Length];
        for (int i = 0; i < canvases.Length; i++)
            canvases[i].SetActive(false);
    }


    public void Update()
    {
        if (isNeedToUpdate)
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
                for (int i = 0; i < canvases.Length; i++)
                    canvases[i].SetActive(false);
                canvases[number_of_action - 1].SetActive(true);
            }

            isNeedToUpdate = false;
        }
        
    }
}
