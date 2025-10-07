using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static PokeConsole.src.EntityManager;

namespace PokeConsole.src
{
    internal class Entity
    {
        public Entity(int id, string name, Vector2 pos, Vector2 scale, entityType entityType)
        {
            ID = id;
            Name = name;
            Pos = pos;
            Scale = scale;
            EntityType = entityType;
        }

        public int ID { get; private set;}
        public string Name { get; private set;}
        public Vector2 Pos { get; private set;}
        public Vector2 Scale { get; private set;}

        public entityType EntityType { get; private set;}

        //public bool IsActive { get; private set; } = true;
        //public void Deactivate() => IsActive = false;
        //public void Activate() => IsActive = true;
    }
}
