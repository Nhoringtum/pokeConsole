using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    public enum MaxStatType
    {
        HP,
        MANA,
        ATTACK,
        DEFENSE,
        SPEED,
        ACCURACY,
        CRIT,
        CRITDAMAGE,
        XP
    }

    public enum ActualStat
    {
        ACTUAL_HP,
        ACTUAL_MANA,
        ACTUAL_ATTACK,
        ACTUAL_DEFENSE,
        ACTUAL_SPEED,
        ACTUAL_ACCURACY,
        ACTUAL_CRIT,
        ACTUAL_CRITDAMAGE,
        ACTUAL_XP
    }

    internal class StatsManager
    {
        //private string _path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + "StatData.json";

        public TypeComparator TypeComparator { get; private set; } = new();

        private Dictionary<MaxStatType, float> _maxStats;
        public float GetMaxStat(MaxStatType stat) => _maxStats[stat];
        public void SetMaxStat(MaxStatType stat, float value) => _maxStats[stat] = value;

        private Dictionary<ActualStat, float> _actualStats;

        public float GetActualStat(ActualStat stat) => _actualStats[stat];
        public void SetActualStat(ActualStat stat, float value) => _actualStats[stat] = value;

        private EntityManager.entityType _entType;

        public StatsManager(EntityManager.entityType entType)
        {
            _maxStats = new Dictionary<MaxStatType, float>();
            _actualStats = new Dictionary<ActualStat, float>();

            InitStatByEntityType(entType);
        }

        //require rework with StatData.json
        public void InitStatByEntityType(EntityManager.entityType entType)
        {
            _entType = entType;

            if (entType == EntityManager.entityType.SORCERER)
            {
                _maxStats[MaxStatType.HP] = 50;
                _maxStats[MaxStatType.MANA] = 100;
                _maxStats[MaxStatType.ATTACK] = 100;
                _maxStats[MaxStatType.DEFENSE] = 100;
                _maxStats[MaxStatType.SPEED] = 100;
                _maxStats[MaxStatType.ACCURACY] = 1f;
                _maxStats[MaxStatType.CRIT] = 0;
                _maxStats[MaxStatType.CRITDAMAGE] = 1f;
                _maxStats[MaxStatType.XP] = 10;
            }

            if (entType == EntityManager.entityType.KNIGHT)
            {
                _maxStats[MaxStatType.HP] = 100;
                _maxStats[MaxStatType.MANA] = 50;
                _maxStats[MaxStatType.ATTACK] = 100;
                _maxStats[MaxStatType.DEFENSE] = 150;
                _maxStats[MaxStatType.SPEED] = 80;
                _maxStats[MaxStatType.ACCURACY] = 1f;
                _maxStats[MaxStatType.CRIT] = 25;
                _maxStats[MaxStatType.CRITDAMAGE] = 1.5f;
                _maxStats[MaxStatType.XP] = 10;
            }

            if (entType == EntityManager.entityType.ENEMY)
            {
                _maxStats[MaxStatType.HP] = 100;
                _maxStats[MaxStatType.MANA] = 50;
                _maxStats[MaxStatType.ATTACK] = 100;
                _maxStats[MaxStatType.DEFENSE] = 150;
                _maxStats[MaxStatType.SPEED] = 80;
                _maxStats[MaxStatType.ACCURACY] = 1f;
                _maxStats[MaxStatType.CRIT] = 25;
                _maxStats[MaxStatType.CRITDAMAGE] = 1.5f;
                _maxStats[MaxStatType.XP] = 10;
            }

            _actualStats = Enum.GetValues(typeof(ActualStat)).Cast<ActualStat>().ToDictionary(stat => stat, stat => _maxStats[(MaxStatType)stat]);
            _actualStats[ActualStat.ACTUAL_XP] = 0;
        }

        public void TakeDamage(Fighter target, float damage, ElementType skillElem)
        {
            if (damage <= 0) throw new ArgumentOutOfRangeException("Null damage or negatif damage isn't allowed here", nameof(damage));

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(skillElem, target.ElemType);
            damage *= multiplierElem;

            if (damage > _actualStats[ActualStat.ACTUAL_HP]) damage = _actualStats[ActualStat.ACTUAL_HP];

            _actualStats[ActualStat.ACTUAL_HP] -= damage;

            if (_actualStats[ActualStat.ACTUAL_HP] <= 0)
            {
                if (_entType == EntityManager.entityType.ENEMY)
                {
                    GameManager.Instance.PlayerWin();
                }
                else
                {
                    EntityManager.Instance.GetSpecificFighter(_entType).IsDead = true;
                }
            }
        }

        public void Heal(float added)
        {
            if (added <= 0) return;
            float max = _maxStats[MaxStatType.HP];
            float cur = _actualStats[ActualStat.ACTUAL_HP];
            _actualStats[ActualStat.ACTUAL_HP] = MathF.Min(max, cur + added);
        }


        public void LevelUp()
        {
            var keys = _maxStats.Keys.ToList();

            _maxStats = Enum.GetValues(typeof(MaxStatType)).Cast<MaxStatType>().ToDictionary(stat => stat, stat => _maxStats[(MaxStatType)stat] * 1.1f);
            _actualStats = Enum.GetValues(typeof(ActualStat)).Cast<ActualStat>().ToDictionary(stat => stat, stat => _maxStats[(MaxStatType)stat]);
        }

        public void AddStat(ActualStat actualStatType, float added)
        {
            _actualStats[actualStatType] += added;
        }

    }
}
