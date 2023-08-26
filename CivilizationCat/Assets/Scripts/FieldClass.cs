using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldClass : MonoBehaviour
{
    public class Field
    {
        public Main.Government government = null;
        public int city_id;

        public Field(Main.Government government)
        {
            this.government = government;
        }
    }
}
