using UnityEngine;
using System.Collections.Generic;


public class Main : MonoBehaviour
{
    // id игрока = 1, governments[0]
   
    public static FieldClass.Field[,] map;
    public static int number_of_governments = 5; // Максимум - 5
    
    public static Government.Action choosen_action = Government.Action.Void;
    public static int[] coords = new int[] { -1, -1 };
    public static bool is_coords_choosen = false;
    
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

        UptadeFrame();
    }

    
    void Update()
    {
        if (choosen_action != Government.Action.Void)
        {
            switch (choosen_action)
            {
                case Government.Action.TakeTaxes:
                    coords = new int[] { -1, -1 };
                    is_coords_choosen = false;
                    break;
                case Government.Action.GiveBribe:
                    coords = new int[] { -1, -1 };
                    is_coords_choosen = false;
                    break;
                case Government.Action.BoostArmy:
                    coords = new int[] { -1, -1 };
                    is_coords_choosen = false;
                    break;
                case Government.Action.Research:
                    coords = new int[] { -1, -1 };
                    is_coords_choosen = false;
                    break;
                case Government.Action.Diplomacy:
                    coords = new int[] { -1, -1 };
                    is_coords_choosen = false;
                    break;
            }
        }
        


        if (is_coords_choosen)
        {
            
            
            // coords = new int[] { -1, -1 }; делать только после выполнения действия ((не важно?) удачного или неудачного)
            // choosen_action = Government.Action.Void; делать только после выполнения действия ((не важно?) удачного или неудачного) 
                
            
            if (Government.governments[0].DoAction(map, choosen_action, coords))
                Debug.Log("УСПЕХ!");
            else
                Debug.LogError("ОШИБКА!!!");

            choosen_action = Government.Action.Void;
            
            InfoCanvasesScript.isNeedToUpdate = true;
            coords = new int[] { -1, -1 };
            is_coords_choosen = false;

            UptadeFrame();
            //choosen_action = Government.Action.Void; // Не уверен в правильности
        }
    }

    public static void UptadeFrame()
    {
        FieldsColorChangeScript.UpdateMap();
        ResourcesTextScript.UpdateTexts();
        ArmyTextScript.UpdateTexts();
    }

    public class Government
    {
        public static Government[] governments;
        public int id;

        // Ресурсы

        public float population;
        public float food;
        public float materials;
        public float jewelry;

        // Армия

        public float infantry;
        public float knights;
        public float siege;



        // Прописать нормальные цены

        private static Dictionary<string, float> base_food_costs = new Dictionary<string, float> {
            {"Colonize", 2 },
            {"FoundCity", 0 },
            {"AppointGovernor", 0 },
            {"TakeTaxes", 0 },
            {"GiveBribe", 0 },
            {"Attack", 0 },
            {"Rob", 0 },
            {"BoostArmy", 0 },
            {"Research", 0 },
            {"Diplomacy", 0 },

        };

        private static Dictionary<string, float> base_materials_costs = new Dictionary<string, float> {
            {"Colonize", 0 },
            {"FoundCity", 0 },
            {"AppointGovernor", 0 },
            {"TakeTaxes", 0 },
            {"GiveBribe", 0 },
            {"Attack", 0 },
            {"Rob", 0 },
            {"BoostArmy", 0 },
            {"Research", 0 },
            {"Diplomacy", 0 },

        };

        private static Dictionary<string, float> base_jewelry_costs = new Dictionary<string, float> {
            {"Colonize", 0 },
            {"FoundCity", 0 },
            {"AppointGovernor", 0 },
            {"TakeTaxes", 0 },
            {"GiveBribe", 0 },
            {"Attack", 0 },
            {"Rob", 0 },
            {"BoostArmy", 0 },
            {"Research", 0 },
            {"Diplomacy", 0 },

        };

        private Dictionary<string, float>[] costs = new Dictionary<string, float>[] { base_food_costs, base_materials_costs, base_jewelry_costs };


        


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

            population = 100;
            food = 100;
            materials = 50;
            jewelry = 0;

            infantry = 1;
            knights = 0;
            siege = 1;
        }

        // Для игрока
        public bool DoAction(FieldClass.Field[,] map, Action action, int[] coords)
        {
            // Потом дописать ресурсы!!!
            switch (action)
            {
                case Action.Colonize:
                    if (isNeighbour(map, id, coords) && food >= costs[0]["Colonize"] && materials >= costs[1]["Colonize"] && jewelry >= costs[2]["Colonize"])
                    {
                        map[coords[0], coords[1]].government = governments[id - 1];
                        food -= costs[0]["Colonize"];
                        materials -= costs[1]["Colonize"];
                        jewelry -= costs[2]["Colonize"];

                        return true;
                    }
                    break;
                case Action.FoundCity:
                    break;
                case Action.AppointGovernor:
                    break;
                case Action.TakeTaxes:
                    break;
                case Action.GiveBribe:
                    break;
                case Action.Attack:
                    Debug.LogWarning("ATTACK");
                    if (isNeighbour(map, id, coords, true) && food >= costs[0]["Attack"] && materials >= costs[1]["Attack"] && jewelry >= costs[2]["Attack"])
                    {
                        Debug.LogWarning("ATTACK TRUE");
                        FieldClass.Field enemy_field = map[coords[0], coords[1]];
                        Government enemy_government = enemy_field.government;

                        FieldClass.Field.Landscape landscape = enemy_field.landscape;
                        float enemy_infantry = enemy_government.infantry;
                        float enemy_knights = enemy_government.knights;
                        float enemy_siege = enemy_government.siege;

                        float strenght;
                        float enemy_strenght;

                        // Подумать над формулой для рыцарей

                        switch (landscape)
                        {
                            case FieldClass.Field.Landscape.Plain:
                                strenght = (3 * infantry) + (6 * knights) + (0 * siege);
                                enemy_strenght = (3 * enemy_infantry) + (6 * enemy_knights) + (0 * enemy_siege);
                                break;
                            case FieldClass.Field.Landscape.Forest:
                                strenght = (3 * infantry) + (5 * knights) + (0 * siege);
                                enemy_strenght = (5 * enemy_infantry) + (5 * enemy_knights) + (0 * enemy_siege);
                                break;
                            case FieldClass.Field.Landscape.Mountain:
                                strenght = (3 * infantry) + (0 * knights) + (3 * siege);
                                enemy_strenght = (7 * enemy_infantry) + (0 * enemy_knights) + (1 * enemy_siege);
                                break;
                            default: // Форт или город
                                strenght = (3 * infantry) + (0 * knights) + (27 * siege);
                                enemy_strenght = (9 * enemy_infantry) + (0 * enemy_knights) + (9 * enemy_siege);
                                break;
                        }

                        Debug.LogWarning(landscape);
                        Debug.LogWarning(strenght);
                        Debug.LogWarning(enemy_strenght);
                        float win_chance = 0.5f + (((strenght - enemy_strenght) / (strenght + enemy_strenght)) * 1.2f);
                        System.Random random = new System.Random();
                        win_chance += (float)random.NextDouble()/5f - 0.1f; // Элемент рандома

                        if (win_chance > 1)
                            win_chance = 1;
                        if (win_chance < 0)
                            win_chance = 0;

                        Debug.LogWarning(win_chance);

                        food -= costs[0]["Attack"];
                        materials -= costs[1]["Attack"];
                        jewelry -= costs[2]["Attack"];

                        // Расчёт потерь войск

                        // Для игрока

                        float losses = 0.5f - (win_chance / 2);

                        float infantry_losses = infantry * losses;
                        float knights_losses = knights * losses;
                        float siege_losses = siege * losses;

                        // Округление
                        if (random.NextDouble() < infantry_losses % 1)
                            infantry_losses = (float)System.Math.Ceiling(infantry_losses);
                        else
                            infantry_losses = (float)System.Math.Floor(infantry_losses);

                        if (random.NextDouble() < knights_losses % 1)
                            knights_losses = (float)System.Math.Ceiling(knights_losses);
                        else
                            knights_losses = (float)System.Math.Floor(knights_losses);

                        if (random.NextDouble() < siege_losses % 1)
                            siege_losses = (float)System.Math.Ceiling(siege_losses);
                        else
                            siege_losses = (float)System.Math.Floor(siege_losses);

                        infantry -= infantry_losses;
                        knights -= knights_losses;
                        siege -= siege_losses;


                        // Для врага

                        losses = 0.5f - ((1 - win_chance) / 2);

                        infantry_losses = enemy_infantry * losses;
                        knights_losses = enemy_knights * losses;
                        siege_losses = enemy_siege * losses;

                        // Округление
                        if (random.NextDouble() < infantry_losses % 1)
                            infantry_losses = (float)System.Math.Ceiling(infantry_losses);
                        else
                            infantry_losses = (float)System.Math.Floor(infantry_losses);

                        if (random.NextDouble() < knights_losses % 1)
                            knights_losses = (float)System.Math.Ceiling(knights_losses);
                        else
                            knights_losses = (float)System.Math.Floor(knights_losses);

                        if (random.NextDouble() < siege_losses % 1)
                            siege_losses = (float)System.Math.Ceiling(siege_losses);
                        else
                            siege_losses = (float)System.Math.Floor(siege_losses);

                        governments[enemy_government.id - 1].infantry -= infantry_losses;
                        governments[enemy_government.id - 1].knights -= knights_losses;
                        governments[enemy_government.id - 1].siege -= siege_losses;

                        if (random.NextDouble() < win_chance)
                        {
                            map[coords[0], coords[1]].government = governments[id - 1];
                            return true;
                        }
                    }
                    break;
                case Action.Rob:
                    break;
                case Action.BoostArmy:
                    break;
                case Action.Research:
                    break;
                case Action.Diplomacy:
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

                            if (j % 2 == 0 && i < map.GetLength(0) - 1)
                            {
                                if (j < map.GetLength(0) - 1 && map[i + 1, j + 1].government != null && map[i + 1, j + 1].government.id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                                if (j > 0 && map[i + 1, j - 1].government != null && map[i + 1, j - 1].government.id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i + 1;
                                    neighbours[counter, 1] = j - 1;
                                    counter++;
                                }
                            }
                            else if (j % 2 == 1 && i > 0)
                            {
                                if (j < map.GetLength(0) - 1 && map[i - 1, j + 1].government != null && map[i - 1, j + 1].government.id != number_of_target_government)
                                {
                                    neighbours[counter, 0] = i - 1;
                                    neighbours[counter, 1] = j + 1;
                                    counter++;
                                }
                                if (j > 0 && map[i - 1, j - 1].government != null && map[i - 1, j - 1].government.id != number_of_target_government)
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
