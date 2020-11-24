using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.ObjectTypes
{
    [Serializable]
    public class Step
    {
        public string ingredient;
        public string instructions;
        public int cookTime;
        public string[] models;
        public Transform position;
    }
}
