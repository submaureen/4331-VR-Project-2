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
        public int health = 5;
        public int penaltyAmount = 1;
        public int quantity = 1;
    }
}
