using UnityEngine;

public class Main : MonoBehaviour
{
    // id игрока = 1, governments[0]

    public static FieldClass.Field[,] map;
    public static int number_of_governments = 5; // Максимум - 5
    
    public static Government.Action choosen_action = Government.Action.Void;
    public static int[] coords = new int[] { -1, -1 };
    
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
                    if (map[i, j].government.id == 0)
                        map[i, j].government = null;

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    if (map[i, j].government != null)
                        map[i, j].landscape = FieldClass.Field.Landscape.City;

            
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    double rand = random.NextDouble();
                    if (map[i, j].government == null && rand < 0.15)
                        map[i, j].landscape = FieldClass.Field.Landscape.Mountain;
                    else if (map[i, j].government == null && rand > 0.15 && rand < 0.45)
                        map[i, j].landscape = FieldClass.Field.Landscape.Forest;
                }
            }
                
                    


        } while (!isGeneratedCorrectly);

        FieldsColorChangeScript.UpdateMap();
    }

    
    void Update()
    {
        if (choosen_action != Government.Action.Void)
        {
            switch (choosen_action)
            {
                case Government.Action.Colonize:
                    if (coords[0] != -1)
                    {
                        if (Government.governments[0].DoAction(map, Government.Action.Colonize, coords))
                            Debug.Log("УСПЕХ!");
                        else
                            Debug.LogError("ОШИБКА!!!");
                        coords[0] = -1;
                        coords[1] = -1;
                    }

                    break;
                case Government.Action.FoundCity:
                    break;
                case Government.Action.AppointGovernor:
                    break;
                case Government.Action.TakeTaxes:
                    break;
                case Government.Action.GiveBribe:
                    break;
                case Government.Action.Attack:
                    break;
                case Government.Action.Rob:
                    break;
                case Government.Action.BoostArmy:
                    break;
                case Government.Action.Research:
                    break;
                case Government.Action.Diplomacy:
                    break;
                default:
                    break;
            }
        }
    }


    public class Government
    {
        public static Government[] governments;
        public int id;

        public enum Action
        {
            Void,
            
            Colonize,
            FoundCity,
            AppointGovernor,
            TakeTaxes,
            GiveBribe,
            Attack,
            Rob,
            BoostArmy,
            Research,
            Diplomacy
        }

        public static Action[] actions = new Action[]
        {
            Action.Void,

            Action.Colonize,
            Action.FoundCity,
            Action.AppointGovernor,
            Action.TakeTaxes,
            Action.GiveBribe,
            Action.Attack,
            Action.Rob,
            Action.BoostArmy,
            Action.Research,
            Action.Diplomacy
        };

        public Government(int id)
        {
            this.id = id;
        }

        // Для игрока
        public bool DoAction(FieldClass.Field[,] map, Action action, int[] coords)
        {
            switch (action)
            {
                case Action.Colonize:
                    if (isNeighbour(map, id, coords) && true) // Потом дописать ресурсы
                    {
                        map[coords[0], coords[1]].government = new Government(id);
                        FieldsColorChangeScript.UpdateMap();
                        return true;
                    }
                    break;
                case Action.FoundCity:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.AppointGovernor:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.TakeTaxes:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.GiveBribe:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.Attack:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.Rob:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.BoostArmy:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.Research:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                case Action.Diplomacy:
                    FieldsColorChangeScript.UpdateMap();
                    break;
                default:
                    break;
            }

            
            return false;
            
        }

        public static bool isNeighbour(FieldClass.Field[,] map, int number_of_target_government, int[] coords, bool isFindGovernment = false)
        {
            int[,] neighbours = FindNeighbours(map, number_of_target_government, isFindGovernment);

            for (int i = 0; i < neighbours.GetLength(0); i++)
                if (neighbours[i, 0] == coords[0] && neighbours[i, 1] == coords[1])
                    return true;
                

            return false;
        }

        public static int[,] FindNeighbours(FieldClass.Field[,] map, int number_of_target_government, bool isFindGovernment = false)
        {
            int[,] neighbours = new int[map.Length, 2];

            for (int i = 0; i < neighbours.GetLength(0); i++)
            {
                neighbours[i, 0] = -1;
                neighbours[i, 1] = -1;
            }

            int counter = 0;

            if (isFindGovernment)
            {
                ////////////////////////////////////////////////////////
                
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j].government != null && map[i, j].government.id == 1)
                        {
                            if (i < map.GetLength(0) - 1 && map[i + 1, j].government != null && map[i + 1, j].government.id != number_of_target_government)
                            {
                                neighbours[counter, 0] = i + 1;
                                neighbours[counter, 1] = j;
                                counter++;
                            }
                            if (i > 0 && map[i - 1, j].government != null && map[i - 1, j].government.id != number_of_target_government)
                            {
                                neighbours[counter, 0] = i - 1;
                                neighbours[counter, 1] = j;
                                counter++;
                            }
                            if (j < map.GetLength(1) - 1 && map[i, j + 1].government != null && map[i, j + 1].government.id != number_of_target_government)
                            {
                                neighbours[counter, 0] = i;
                                neighbours[counter, 1] = j + 1;
                                counter++;
                            }
                            if (j > 0 && map[i, j - 1].government != null && map[i, j - 1].government.id != number_of_target_government)
                            {
                                neighbours[counter, 0] = i;
                                neighbours[counter, 1] = j - 1;
                                counter++;
                            }

                            if (i % 2 == 0 && j < map.GetLength(1) - 1)
                            {
                                if (i < map.GetLength(0) - 1 && map[i + 1, j + 1].government != null && map[i + 1, j + 1].city_id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                                if (i > 0 && map[i - 1, j + 1].government != null && map[i - 1, j + 1].city_id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i - 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                            }
                            else if (i % 2 == 1 && j > 0)
                            {
                                if (i < map.GetLength(0) - 1 && map[i + 1, j - 1].government != null && map[i + 1, j - 1].city_id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j - 1;
                                    counter++;
                                }
                                if (i > 0 && map[i - 1, j - 1].government != null && map[i - 1, j - 1].city_id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i - 1;
                                    neighbours[counter, 1] = j - 1;
                                    counter++;
                                }
                            }
                        }

                    }
                }

                ////////////////////////////////////////////////////////

            }
            else
            {
                ////////////////////////////////////////////////////////

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j].government != null && map[i, j].government.id == 1)
                        {
                            if (i < map.GetLength(0) - 1 && map[i + 1, j].government == null)
                            {
                                neighbours[counter, 0] = i + 1;
                                neighbours[counter, 1] = j;
                                counter++;
                            }
                            if (i > 0 && map[i - 1, j].government == null)
                            {
                                neighbours[counter, 0] = i - 1;
                                neighbours[counter, 1] = j;
                                counter++;
                            }
                            if (j < map.GetLength(1) - 1 && map[i, j + 1].government == null)
                            {
                                neighbours[counter, 0] = i;
                                neighbours[counter, 1] = j + 1;
                                counter++;
                            }
                            if (j > 0 && map[i, j - 1].government == null)
                            {
                                neighbours[counter, 0] = i;
                                neighbours[counter, 1] = j - 1;
                                counter++;
                            }

                            if (j % 2 == 0 && i < map.GetLength(0) - 1)
                            {
                                if (j < map.GetLength(0) - 1 && map[i + 1, j + 1].government == null)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                                if (j > 0 && map[i + 1, j - 1].government == null)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j - 1;
                                    counter++;
                                }
                            }
                            else if (j % 2 == 1 && i > 0)
                            {
                                if (j < map.GetLength(0) - 1 && map[i - 1, j + 1].government == null)
                                {
                                    neighbours[counter, 0] = i - 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                                if (j > 0 && map[i - 1, j - 1].government == null)
                                {
                                    neighbours[counter, 0] = i - 1;
                                    neighbours[counter, 1] = j - 1;
                                    counter++;
                                }
                            }
                        }

                    }
                }

                ////////////////////////////////////////////////////////
            }

            int new_len = 0;

            for (int i = 0; i < neighbours.GetLength(0); i++)
            {
                if (neighbours[i, 0] == -1)
                {
                    new_len = i;
                    break;
                }
            }

            int[,] new_neighbours = CutArray(neighbours, new_len, 2);



            return new_neighbours;
        }

        private static int[,] CutArray(int[,] array, int new_len, int data_len)
        {
            int[,] new_array = new int[new_len, data_len];

            for (int i = 0; i < new_len; i++)
            {
                for (int j = 0; j < data_len; j++)
                {
                    new_array[i, j] = array[i, j];
                }

            }

            return new_array;
        }
    }
}
