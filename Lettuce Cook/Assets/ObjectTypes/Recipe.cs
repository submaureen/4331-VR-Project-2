﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ObjectTypes
{
    [Serializable]
    public class Recipe
    {
        public string name;
        public Step[] steps;

        public GameObject finalFood;

    }
}
