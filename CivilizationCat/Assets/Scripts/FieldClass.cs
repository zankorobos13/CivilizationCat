using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldClass : MonoBehaviour
{
    public class Field
    {
        public enum Landscape
        {
            Plain,
            Forest,
            Mountain,
            Fortress,
            City
        }

        public Main.Government government = null;
        public int city_id;
        public Landscape landscape;

        public Field(Main.Government government)
        {
            this.government = government;
        }
    }
}
