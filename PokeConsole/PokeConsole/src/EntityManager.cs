using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal sealed class EntityManager
    {
        public enum entityType
        {
            PLAYER,
            SORCERER,
            KNIGHT,
            ENEMY,
            PNJ,
            ITEM,
            PROPS
        }

        private static EntityManager _instance;
        public static EntityManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EntityManager();
                }
                return _instance;
            }
        }

        private entityType _entityType;
        public entityType GetEntityType() => _entityType;

        public EntityManager()
        {
            _entityList = new Dictionary<entityType, Entity>();
        }

        private Dictionary<entityType, Entity> _entityList;

        public void CreateEntity(entityType entityTypeEnum, string name, Vector2 pos, Vector2 scale)
        {
            Entity newEntity = new Entity(_entityList.Count, name, pos, scale, entityTypeEnum);

            _entityList.Add(entityTypeEnum, newEntity);
        }

        public void CreateFighter(entityType entityTypeEnum, string name, Vector2 pos, Vector2 scale, ElementType elemType)
        {
            if (_entityList.ContainsKey(entityTypeEnum)) return;

            Entity newEntity = new Fighter(_entityList.Count, name, pos, scale, entityTypeEnum, elemType);
            _entityList.Add(entityTypeEnum, newEntity);
        }

        public Dictionary<entityType, Entity> GetEntityDictionary()
        {
            return _entityList;
        }

        public Entity GetEntity()
        {
            throw new NotImplementedException();
        }

        public Fighter? GetSpecificFighter(entityType entityType)
        {
            if (_entityList.TryGetValue(entityType, out var knight))
                return knight as Fighter;
            else if (_entityList.TryGetValue(entityType, out var sorcerer))
                return sorcerer as Fighter;
            else if (_entityList.TryGetValue(entityType, out var enemy))
                return enemy as Fighter;

            return null;
        }
    }
}
