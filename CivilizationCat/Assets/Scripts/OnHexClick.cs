using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHexClick : MonoBehaviour
{
    private GameObject[,] _fields;
    void Start()
    {
        _fields = MapGenerationScript.fields;
        
    }

    private void OnMouseDown()
    {
        for (int i = 0; i < _fields.GetLength(0); i++)
            for (int j = 0; j < _fields.GetLength(1); j++)
                if (_fields[i, j].transform.position == gameObject.transform.position)
                {
                    Debug.Log(i + " " + j + " - " + Main.Government.isNeighbour(Main.map, 1, new int[] { i, j }, false));
                    if (Main.choosen_action != Main.Government.Action.Void)
                    {
                        Main.coords[0] = i;
                        Main.coords[1] = j;
                    }
                    InfoCanvasesScript.isNeedToUpdate = true;
                }
    }

    
}
