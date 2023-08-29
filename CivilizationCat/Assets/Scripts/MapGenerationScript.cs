using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerationScript : MonoBehaviour
{
    public GameObject map;
    public GameObject field;
    public static GameObject[,] fields;
    public static int len = 10;
    private float _hex_size_default_x = 1;
    private float _hex_size_default_y = 1;
    private float _map_size_x = 8;
    private float _map_size_y = 8;
    public float offset;


    void Start()
    {
        
        fields = new GameObject[len, len];

        field.transform.localScale = new Vector3(((_map_size_x - ((len - 1) * offset)) / len) * _hex_size_default_x, ((_map_size_y - ((len - 1) * offset)) / len) * _hex_size_default_y, field.transform.localScale.z);

        int i = 0;
        int j = 0;

        for (float x = 0; x < _map_size_x; x += field.transform.localScale.x + offset)
        {
            for (float y = 0; y < _map_size_y; y += field.transform.localScale.y + offset)
            {
                GameObject new_field;
                float nY = y + ((field.transform.localScale.y / 4) * (len - j));

                if (j % 2 == 1)
                {
                    new_field = Instantiate(field, new Vector3(x, nY, 0), Quaternion.identity);
                }
                else
                {
                    if (len <= 11)
                        new_field = Instantiate(field, new Vector3(x + ((field.transform.localScale.x * field.transform.Find("FieldSprite").localScale.x - (offset * (18 / len))) / 2), nY, 0), Quaternion.identity); 
                    else
                        new_field = Instantiate(field, new Vector3(x + ((field.transform.localScale.x * field.transform.Find("FieldSprite").localScale.x - (offset * (1.5f - (0.06666f * len)))) / 2), nY, 0), Quaternion.identity);
                }
               

                new_field.transform.parent = map.transform;
                fields[i, j] = new_field;
                j++;
            }
            i++;
            j = 0;
        }

        map.transform.position = new Vector2(-8.5f, -3.3f);
    }

}
