using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnActionButtonsClick : MonoBehaviour
{
    public static void OnClick(int number_of_action)
    {
        Debug.Log(number_of_action);
        Main.choosen_action = Main.Government.actions[number_of_action];
        InfoCanvasesScript.isNeedToUpdate = true;
    }
}
