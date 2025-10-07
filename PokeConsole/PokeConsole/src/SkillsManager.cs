using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PokeConsole.src.EntityManager;
using static PokeConsole.src.Fighter;
using static PokeConsole.src.SkillsManager;

namespace PokeConsole.src
{
    internal class SkillsManager
    {
        public SkillsManager()
        {
            _rand = new Random();
            AllSkills = new List<Skill>();

            InitAllSkills();
        }

        public enum SkillName
        {
            FIREBALL,
            HEAL,
            TRUCDEOUF,
            BASICATTACK,
            ELEMENTATTACK,
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
        Skill _elementAttack;
        Skill _basicAttackEnnemy;

        System.Random _rand;

        //require rework with SkillsData.json
        private void InitAllSkills()
        {
            _fireBall = new Skill(SkillName.FIREBALL, "Fireball inflict damage", 20, 100, 10, ElementType.FIRE);
            _heal = new Skill(SkillName.HEAL, "Heal regain health, woah", 80, 100, 10, ElementType.WATER);
            _trucDeOuf = new Skill(SkillName.TRUCDEOUF, "On est la hein", 1000, 50, 10, ElementType.GROUND);
            _basicAttack = new Skill(SkillName.BASICATTACK, "Just deal small amount of damage", 1, 100, 0, ElementType.GROUND);
            _elementAttack = new Skill(SkillName.ELEMENTATTACK, "element attack", 20, 100, 10, ElementType.GROUND); //can be without element
            _basicAttackEnnemy = new Skill(SkillName.BASICATTACKENNEMY, "Just deal small amount of damage", 1, 100, 0, ElementType.GROUND);

            AllSkills.Add(_fireBall);
            AllSkills.Add(_heal);
            AllSkills.Add(_trucDeOuf);
            AllSkills.Add(_basicAttack);
            AllSkills.Add(_elementAttack);
            AllSkills.Add(_basicAttackEnnemy);
        }

        public void LearnNewSkill(Fighter fighter, SkillName allSkillEnum)
        {
            if (fighter is Fighter == false) return;

            //magic number, will be replaced
            if (fighter.ListLearnedSkill.Count > 4)
            {
                DeleteSkill(fighter, 0);
                fighter.ListLearnedSkill.Add(allSkillEnum, _trucDeOuf);
            }

            if (fighter.ListLearnedSkill.TryGetValue(allSkillEnum, out Skill skill) == true)
                return;

            fighter.ListLearnedSkill.Add(allSkillEnum, AllSkills[(int)allSkillEnum]);
        }

        public void DeleteSkill(Fighter fighter, SkillName skillName)
        {
            fighter.ListLearnedSkill.Remove(skillName);
        }

        public void UseSkill(Fighter caster, Fighter target, Skill skill)
        {
            if (CheckEnoughMana(caster, target, skill.Name) == false || CheckEnoughAccuracy(caster, target, skill.Name) == false) return;

            switch (skill.Name)
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

            caster.StatsManager.SetActualStat(ActualStat.ACTUAL_MANA, - skill.ManaCost);
        }

        //public void EnnemyUseSkill(Fighter caster, Fighter target)
        //{
        //    switch (TypeComparator.GetWeaknessType(target.ElemType))
        //    {
        //        case ElementType.FIRE:
        //            if (CheckEnoughMana(caster, target, (int)SkillName.WATERATTACK) == false
        //                || CheckEnoughAccuracy(caster, target, (int)SkillName.WATERATTACK) == false) return;
        //            WaterAttack(caster, target);
        //            break;
        //        case ElementType.GRASS:
        //            if (CheckEnoughMana(caster, target, (int)SkillName.FIREATTACK) == false
        //                || CheckEnoughAccuracy(caster, target, (int)SkillName.FIREATTACK) == false) return;
        //            FireAttack(caster, target);
        //            break;
        //        case ElementType.GROUND:
        //            if (CheckEnoughMana(caster, target, (int)SkillName.ELECTRICATTACK) == false
        //                || CheckEnoughAccuracy(caster, target, (int)SkillName.ELECTRICATTACK) == false) return;
        //            ElectricAttack(caster, target);
        //            break;
        //        case ElementType.ELECTRIC:
        //            if (CheckEnoughMana(caster, target, (int)SkillName.GROUNDATTACK) == false
        //                || CheckEnoughAccuracy(caster, target, (int)SkillName.GROUNDATTACK) == false) return;
        //            GroundAttack(caster, target);
        //            break;
        //        case ElementType.WATER:
        //            if (CheckEnoughMana(caster, target, (int)SkillName.GRASSATTACK) == false
        //                || CheckEnoughAccuracy(caster, target, (int)SkillName.GRASSATTACK) == false) return;
        //            GrassAttack(caster, target);
        //            break;
        //    }
        //}

        private bool CheckEnoughMana(Fighter caster, Fighter target, SkillName skillName)
        {
            if (caster.ListLearnedSkill.TryGetValue(skillName, out Skill skill) == false)
                return false;

            if (caster.StatsManager.GetActualStat(ActualStat.ACTUAL_MANA) < skill.ManaCost)
                return false;

            return true;
        }

        private bool CheckEnoughAccuracy(Fighter caster, Fighter target, SkillName skillName)
        {
            if (caster.ListLearnedSkill.TryGetValue(skillName, out Skill skill) == false)
                return false;

            int roll = _rand.Next(101);

            if (skill.Accuracy < roll)
            {
                return false;
            }

            return true;
        }

        private void FireBall(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} cast {_fireBall.Name} on {target.Name} !");

            target.StatsManager.TakeDamage(target, _fireBall.Power, _fireBall.ElemType);
        }

        private void Heal(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} heal {target.Name} !");

            caster.StatsManager.Heal(_heal.Power);
        }

        private void TrucDeOuf(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} deals too much damage on {target.Name} !");
            target.StatsManager.TakeDamage(target, _trucDeOuf.Power, _trucDeOuf.ElemType);
        }

        private void BasicAttack(Fighter caster, Fighter target)
        {
            //Console.WriteLine($"{caster.Name} deals small damage on {target.Name} !");

            target.StatsManager.TakeDamage(target, _basicAttack.Power, _basicAttack.ElemType);
        }

        private void ElementAttack(Fighter caster, Fighter target, ElementType elemType)
        {
            target.StatsManager.TakeDamage(target, _elementAttack.Power, elemType);
        }
    }
}
