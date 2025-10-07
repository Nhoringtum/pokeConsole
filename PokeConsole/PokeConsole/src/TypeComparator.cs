using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    public enum EfficacityState
    {
        ULTRAEFFECTIVE,
        EFFECTIVE,
        NEUTRAL,
        RESISTED,
        NODAMAGED
    }

    public enum ElementType
    {
        FIRE,
        GRASS,
        GROUND,
        ELECTRIC,
        WATER
    }

    internal class TypeComparator
    {
        public TypeComparator()
        {
            InitializeTypeMultipliers();
        }

        private const float _noDamagedMultiplier = 0f;
        private const float _resistMultiplier = 0.5f;
        private const float _neutralMultiplier = 1f;
        private const float _effectiveMultiplier = 2f;
        private const float _ultraEffectiveMultiplier = 3f;

        private readonly Dictionary<(ElementType from, ElementType to), float> _typeMultiplier = new();

        public void InitializeTypeMultipliers()
        {
            //------------------------------------ FIRE ------------------------------------

            _typeMultiplier[(ElementType.FIRE, ElementType.FIRE)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.FIRE, ElementType.GRASS)] = _effectiveMultiplier;
            _typeMultiplier[(ElementType.FIRE, ElementType.GROUND)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.FIRE, ElementType.ELECTRIC)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.FIRE, ElementType.WATER)] = _resistMultiplier;

            //------------------------------------ GRASS ------------------------------------

            _typeMultiplier[(ElementType.GRASS, ElementType.FIRE)] = _resistMultiplier;
            _typeMultiplier[(ElementType.GRASS, ElementType.GRASS)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.GRASS, ElementType.GROUND)] = _effectiveMultiplier;
            _typeMultiplier[(ElementType.GRASS, ElementType.ELECTRIC)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.GRASS, ElementType.WATER)] = _neutralMultiplier;

            //------------------------------------ GROUND -----------------------------------

            _typeMultiplier[(ElementType.GROUND, ElementType.FIRE)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.GROUND, ElementType.GRASS)] = _resistMultiplier;
            _typeMultiplier[(ElementType.GROUND, ElementType.GROUND)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.GROUND, ElementType.ELECTRIC)] = _effectiveMultiplier;
            _typeMultiplier[(ElementType.GROUND, ElementType.WATER)] = _neutralMultiplier;

            //----------------------------------- ELECTRIC ----------------------------------

            _typeMultiplier[(ElementType.ELECTRIC, ElementType.FIRE)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.ELECTRIC, ElementType.GRASS)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.ELECTRIC, ElementType.GROUND)] = _resistMultiplier;
            _typeMultiplier[(ElementType.ELECTRIC, ElementType.ELECTRIC)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.ELECTRIC, ElementType.WATER)] = _effectiveMultiplier;

            //------------------------------------ WATER ------------------------------------

            _typeMultiplier[(ElementType.WATER, ElementType.FIRE)] = _effectiveMultiplier;
            _typeMultiplier[(ElementType.WATER, ElementType.GRASS)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.WATER, ElementType.GROUND)] = _neutralMultiplier;
            _typeMultiplier[(ElementType.WATER, ElementType.ELECTRIC)] = _resistMultiplier;
            _typeMultiplier[(ElementType.WATER, ElementType.WATER)] = _neutralMultiplier;

            //------------------------------------       ------------------------------------
        }

        public float GetMultiplierByCompareType(ElementType skillType, ElementType targetType)
        {
            return _typeMultiplier.TryGetValue((skillType, targetType), out var mult) ? mult : 1f;
        }


        public List<ElementType> GetWeaknessTypes(ElementType target)
        {
            var weaknesses = new List<ElementType>();

            foreach (ElementType type in Enum.GetValues(typeof(ElementType)))
            {
                if (_typeMultiplier.TryGetValue((type, target), out var mult) && mult > 1f)
                {
                    weaknesses.Add(type);
                }
            }

            return weaknesses;
        }


        public ElementType GetWeaknessType(ElementType target)
        {
            throw new NotImplementedException();
        }
    }
}
