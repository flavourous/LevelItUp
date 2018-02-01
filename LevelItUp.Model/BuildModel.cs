using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LevelItUp.Model
{
    public class BaseModel
    {
        public int id { get; set; }
    }

    public class BaseGameModel : BaseModel
    {
        public GameBuild Game { get; set; }
    }

    // e.g. Underrail
    public class GameBuild : BaseModel
    {
        public string Name { get; set; }
        public int MaxLevel { get; set; }
    }

    // e.g. "Dalvos Hammer Build"
    public class Build : BaseGameModel
    {
        public string Name { get; set; }
        public static Build Clone(Build m) => (Build)m.MemberwiseClone();
    }
 
    // e.g. Dalvos Strength 100 at level 1
    public class BuildLevelParameter : BaseGameModel
    {
        public Build Build { get; set; }
        public BuildParameter Parameter { get; set; }
        public int Amount { get; set; }
        public int Level { get; set; }
    }

    // e.g. "Strength" or "Gunslinger".  "Psi Empathy costs zero".
    public class BuildParameter : BaseGameModel
    {
        public String Name { get; set; }
        public String Category { get; set; }
        public BuildParameterType Type { get; set; }
        public int Cost { get; set; }
    }

    // e.g. "featy" requires level 12, 4 strength, 25 kilzor and feats feat1 and feat2
    public class BuildParameterRequiement : BaseGameModel
    {
        public String OrGroup { get; set; }

        public BuildParameter Depend { get; set; }
        public int DAmount { get; set; }

        public bool Not { get; set; }

        public BuildParameter On { get; set; } // null for level requiemnt...
        public int OAmount { get; set; }
    }

    // e.g. Attributes
    public class BuildParameterType : BaseGameModel
    {
        public String Name { get; set; }
        public int Minimum { get; set; }
    }

    // e.g. 120 Skillpoints at lv 1, any skill cant go higher than 15 at this level
    public class BuildParameterTypeLevelPoints : BaseGameModel
    {
        public int Level { get; set; }
        public int Amount { get; set; }
        public int Limit { get; set; }
        public BuildParameterType Type { get; set; }
    }
}
