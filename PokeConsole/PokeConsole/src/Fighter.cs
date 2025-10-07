using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static PokeConsole.src.EntityManager;

namespace PokeConsole.src
{
    internal class Fighter : Entity
    {

        //public List<Skill> ListLearnedSkill { get; set; }

        public Fighter(int id, string name, Vector2 pos, Vector2 scale, entityType entityType, ElementType elementType) : base(id, name, pos, scale, entityType) 
        {
            //ListLearnedSkill = new List<Skill>();

            //SkillManager = new SkillManager();
            //StatsManager = new StatsManager(entityType);
            ElemType = elementType;
            IsDead = false;
        }

        //public StatsManager StatsManager { get; private set; }
        //public SkillManager SkillManager { get; private set; }

        public bool IsDead { get; private set; }
        
        public ElementType ElemType { get; private set; }
    }
}
