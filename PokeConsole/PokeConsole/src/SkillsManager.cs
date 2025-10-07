using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokeConsole.src.EntityManager;

namespace PokeConsole.src
{
    internal class SkillsManager
    {
        public SkillsManager()
        {
            rand = new Random();
            AllSkills = new List<Skill>();

            InitAllSkills();
        }

        public enum SkillName
        {
            FIREBALL,
            HEAL,
            TRUCDEOUF,
            BASICATTACK,
            WATERATTACK,
            ELECTRICATTACK,
            GROUNDATTACK,
            GRASSATTACK,
            FIREATTACK,
            BASICATTACKENNEMY
        }

        public List<Skill> AllSkills
        {
            get; private set;
        }
        //require rework with SkillsData.json
        Skill _fireBall;
        Skill _heal;
        Skill _trucDeOuf;
        Skill _basicAttack;
        Skill _waterAttack;
        Skill _electricAttack;
        Skill _groundAttack;
        Skill _grassAttack;
        Skill _fireAttack;
        Skill _basicAttackEnnemy;

        public TypeComparator TypeComparator { get; private set; } = new();

        System.Random rand;

        //require rework with SkillsData.json
        private void InitAllSkills()
        {
            _fireBall = new Skill(SkillName.FIREBALL, "Fireball inflict damage", 20, 100, 10, ElementType.FIRE);
            _heal = new Skill(SkillName.HEAL, "Heal regain health, woah", 80, 100, 10, ElementType.WATER);
            _trucDeOuf = new Skill(SkillName.TRUCDEOUF, "On est la hein", 1000, 50, 10, ElementType.GROUND);
            _basicAttack = new Skill(SkillName.BASICATTACK, "Just deal small amount of damage", 1, 100, 0, ElementType.GROUND);
            _waterAttack = new Skill(SkillName.WATERATTACK, "bloup bloup", 10, 50, 10, ElementType.WATER);
            _electricAttack = new Skill(SkillName.ELECTRICATTACK, "kirua hairstyle", 10, 50, 10, ElementType.ELECTRIC);
            _groundAttack = new Skill(SkillName.GROUNDATTACK, "hound ground", 10, 50, 10, ElementType.GROUND);
            _grassAttack = new Skill(SkillName.GRASSATTACK, "smoke this plant bro", 10, 50, 10, ElementType.GRASS);
            _fireAttack = new Skill(SkillName.FIREBALL, "smoke this plant bro", 10, 50, 10, ElementType.FIRE);
            _basicAttackEnnemy = new Skill(SkillName.BASICATTACKENNEMY, "Just deal small amount of damage", 1, 100, 0, ElementType.GROUND);

            AllSkills.Add(_fireBall);
            AllSkills.Add(_heal);
            AllSkills.Add(_trucDeOuf);
            AllSkills.Add(_basicAttack);
            AllSkills.Add(_waterAttack);
            AllSkills.Add(_electricAttack);
            AllSkills.Add(_groundAttack);
            AllSkills.Add(_grassAttack);
            AllSkills.Add(_fireAttack);
            AllSkills.Add(_basicAttackEnnemy);
        }

        public void LearnNewSkill(Fighter fighter, int allSkillIndex)
        {
            if (fighter is Fighter == false) return;

            fighter.ListLearnedSkill.Add(AllSkills[allSkillIndex]);
        }

        public void DeleteSkill(Fighter fighter, int index)
        {
            fighter.ListLearnedSkill.RemoveAt(index);
        }

        public void UseSkill(Fighter caster, Fighter target, int index)
        {
            if (caster.EntityType == entityType.ENEMY) EnnemyUseSkill(caster, target);

            else
            {
                if (CheckEnoughMana(caster, target, index) == false || CheckEnoughAccuracy(caster, target, index) == false) return;

                switch (caster.ListLearnedSkill[index].Name)
                {
                    case SkillName.FIREBALL:
                        FireBall(caster, target);
                        break;
                    case SkillName.HEAL:
                        Heal(caster, target);
                        break;
                    case SkillName.TRUCDEOUF:
                        TrucDeOuf(caster, target);
                        break;
                    case SkillName.BASICATTACK:
                        BasicAttack(caster, target);
                        break;
                    default:
                        break;
                }
            }

            caster.StatsManager.SetActualStat(ActualStat.ACTUAL_MANA, -caster.ListLearnedSkill[index].ManaCost);
        }

        public void EnnemyUseSkill(Fighter caster, Fighter target)
        {
            switch (TypeComparator.GetWeaknessType(target.ElemType))
            {
                case ElementType.FIRE:
                    if (CheckEnoughMana(caster, target, (int)SkillName.WATERATTACK) == false
                        || CheckEnoughAccuracy(caster, target, (int)SkillName.WATERATTACK) == false) return;
                    WaterAttack(caster, target);
                    break;
                case ElementType.GRASS:
                    if (CheckEnoughMana(caster, target, (int)SkillName.FIREATTACK) == false
                        || CheckEnoughAccuracy(caster, target, (int)SkillName.FIREATTACK) == false) return;
                    FireAttack(caster, target);
                    break;
                case ElementType.GROUND:
                    if (CheckEnoughMana(caster, target, (int)SkillName.ELECTRICATTACK) == false
                        || CheckEnoughAccuracy(caster, target, (int)SkillName.ELECTRICATTACK) == false) return;
                    ElectricAttack(caster, target);
                    break;
                case ElementType.ELECTRIC:
                    if (CheckEnoughMana(caster, target, (int)SkillName.GROUNDATTACK) == false
                        || CheckEnoughAccuracy(caster, target, (int)SkillName.GROUNDATTACK) == false) return;
                    GroundAttack(caster, target);
                    break;
                case ElementType.WATER:
                    if (CheckEnoughMana(caster, target, (int)SkillName.GRASSATTACK) == false
                        || CheckEnoughAccuracy(caster, target, (int)SkillName.GRASSATTACK) == false) return;
                    GrassAttack(caster, target);
                    break;
            }
        }

        private bool CheckEnoughMana(Fighter caster, Fighter target, int index)
        {
            foreach (Skill casterskill in caster.ListLearnedSkill)
            {
                if (casterskill.Name == (SkillName)index)
                {
                    if (caster.StatsManager.GetActualStat(ActualStat.ACTUAL_MANA) < casterskill.ManaCost)
                    {
                        //Console.WriteLine($"{caster.Name} not enough mana for {caster.ListLearnedSkill[index].Name} ! Cast BasicAttack instead !");
                        BasicAttack(caster, target); //Launch basic if no mana
                        return false;
                    }
                    return true;
                }
            }
            BasicAttack(caster, target); //Launch basic if not found
            return false;
        }

        private bool CheckEnoughAccuracy(Fighter caster, Fighter target, int index)
        {
            foreach (Skill casterskill in caster.ListLearnedSkill)
            {
                if (casterskill.Name == (SkillName)index)
                {
                    if (casterskill.Accuracy < rand.Next(101))
                    {
                        //Console.WriteLine($"{caster.Name} Miss {caster.ListLearnedSkill[index].Name} !");
                        return false; //Gros L
                    }

                    return true;
                }
            }
            BasicAttack(caster, target); //Launch basic if not found
            return false;

        }

        private void FireBall(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_fireBall.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_fireBall.Power * multiplierElem);
        }

        private void Heal(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} heal {target.Name} !");

            caster.StatsManager.Heal(_heal.Power);
        }

        private void TrucDeOuf(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} deals too much damage on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);
            target.StatsManager.TakeDamage(_trucDeOuf.Power * multiplierElem);
        }

        private void BasicAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} deals small damage on {target.Name} !");

            target.StatsManager.TakeDamage(_basicAttack.Power);
        }

        private void WaterAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_waterAttack.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_waterAttack.Power * multiplierElem);
        }

        private void ElectricAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_electricAttack.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_electricAttack.Power * multiplierElem);
        }

        private void GroundAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_groundAttack.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_groundAttack.Power * multiplierElem);
        }

        private void GrassAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_grassAttack.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_grassAttack.Power * multiplierElem);
        }

        private void FireAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_fireAttack.Name} on {target.Name} !");

            float multiplierElem = TypeComparator.GetMultiplierByCompareType(caster.ElemType, target.ElemType);

            target.StatsManager.TakeDamage(_fireAttack.Power * multiplierElem);
        }
    }
}
