using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static FieldClass.Field[,] map;
    public static int number_of_governments = 5;
    
    void Start()
    {
        map = new FieldClass.Field[MapGenerationScript.len, MapGenerationScript.len];

        System.Random random = new System.Random();

        bool isGeneratedCorrectly;

        do
        {
            // Создание пустой карты
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = new FieldClass.Field(new Government(0));

            isGeneratedCorrectly = true;

            Government.governments = new Government[number_of_governments];

            // Расстановка государств
            for (int i = 1; i <= number_of_governments; i++)
            {
                int y = random.Next(0, map.GetLength(1));
                int x = random.Next(0, map.GetLength(0));
                if (map[x, y] == new FieldClass.Field(new Government(0)))
                {
                    Government government = new Government(i);
                    map[x, y] = new FieldClass.Field(government);
                    Government.governments[i - 1] = government;
                }
                else
                {
                    int counter = 0;
                    while (map[x, y].government.id != 0)
                    {
                        y = random.Next(0, map.GetLength(1));
                        x = random.Next(0, map.GetLength(0));
                        counter++;
                        if (counter > System.Math.Pow(map.Length, 2))
                        {
                            isGeneratedCorrectly = false;
                            break;
                        }
                    }
                    Government government = new Government(i);
                    map[x, y] = new FieldClass.Field(government);
                    Government.governments[i - 1] = government;
                }

                // Создание недопустимых точек спавна для других государств (границы с другими государствами)
                if (x != 0 && map[x - 1, y].government.id == 0)
                    map[x - 1, y].government.id = -1;
                if (y != 0 && map[x, y - 1].government.id == 0)
                    map[x, y - 1].government.id = -1;
                if (x != map.GetLength(0) - 1 && map[x + 1, y].government.id == 0)
                    map[x + 1, y].government.id = -1;
                if (y != map.GetLength(1) - 1 && map[x, y + 1].government.id == 0)
                    map[x, y + 1].government.id = -1;

                if (y % 2 == 1)
                {
                    if (x != 0  && y != map.GetLength(1) - 1 && map[x - 1, y + 1].government.id == 0)
                        map[x - 1, y + 1].government.id = -1;
                    if (x != 0 && y != 0 && map[x - 1, y - 1].government.id == 0)
                        map[x - 1, y - 1].government.id = -1;
                }
                else
                {
                    if (x != map.GetLength(0) - 1 && y != map.GetLength(1) - 1 && map[x + 1, y + 1].government.id == 0)
                        map[x + 1, y + 1].government.id = -1;
                    if (x !=  map.GetLength(0) - 1 && y != 0 && map[x + 1, y - 1].government.id == 0)
                        map[x + 1, y - 1].government.id = -1;
                }
                
            }

            // Приведение недопустимых точек спавна для других государств к нормальному виду на карте
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j].government.id == -1)
                        map[i, j].government.id = 0;

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j].government.id != 0)
                        map[i, j].landscape = FieldClass.Field.Landscape.City;

        } while (!isGeneratedCorrectly);

        
    }

    
    void Update()
    {
        
    }


    public class Government
    {
        public static Government[] governments;
        public int id;

        public Government(int id)
        {
            this.id = id;
        }
    }
}
