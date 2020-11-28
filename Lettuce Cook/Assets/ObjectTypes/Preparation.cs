using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ObjectTypes
{
    [Serializable]
    public class Preparation
    {
        public string name;
        public PrepStep[] steps;
    }

}
