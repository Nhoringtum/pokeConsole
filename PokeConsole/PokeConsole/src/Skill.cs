using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class Skill
    {
        public Skill(SkillsManager.SkillName name, string description, int power, int accuracy, int manaCost, ElementType elemType)
        {
            Name = name;
            Description = description;
            Power = power;
            Accuracy = accuracy;
            ManaCost = manaCost;
            ElemType = elemType;
        }

        public SkillsManager.SkillName Name
        {
            get; private set;
        }

        public string Description
        {
            get; private set;
        }
        //public int Level { get; set; }
        public int Power
        {
            get; private set;
        }
        public int Accuracy
        {
            get; private set;
        }
        public int ManaCost
        {
            get; private set;
        }
        public ElementType ElemType
        {
            get; private set;
        }
    }
}
