using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static PokeConsole.src.Fighter;

namespace PokeConsole.src
{
    internal class GameManager
    {
        EntityManager _entityManager;
        StateManager _stateManager;
        WorldState _worldState;
        MainMenuState _mainMenuState; 
        FightState _fightState;
        Fighter _sorcerer;
        Fighter _knight;

        private GameManager() 
        {
            Init();

            if (_stateManager == null || _worldState == null) return;

            _stateManager?.ChangeState(_worldState);
        }

        private static GameManager _instance;
        public static GameManager Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public void Init()
        {
            _stateManager = new StateManager();
            _worldState = new WorldState();
            _mainMenuState = new MainMenuState();
            _fightState = new FightState();

            _entityManager = new EntityManager();
            _entityManager.CreateFighter(EntityManager.entityType.SORCERER, "wizarDRY", new Vector2(10, 10), new Vector2(1, 1),ElementType.WATER);
            _entityManager.CreateFighter(EntityManager.entityType.KNIGHT, "shovelKnight", new Vector2(20, 20), new Vector2(1, 1),ElementType.GRASS);
            _sorcerer = _entityManager.GetSpecificFighter(EntityManager.entityType.SORCERER);
            _knight = _entityManager.GetSpecificFighter(EntityManager.entityType.KNIGHT);
        }

        public void Update()
        {
            Console.WriteLine(_knight.StatsManager.GetActualStat(ActualStat.ACTUAL_HP)); 
            _sorcerer.SkillsManager.LearnNewSkill(_sorcerer, SkillsManager.SkillName.FIREBALL);
            _sorcerer.SkillsManager.UseSkill(_sorcerer, _knight, _sorcerer.ListLearnedSkill[(int)SkillSlot.FIRST]);
            Console.WriteLine(_knight.StatsManager.GetActualStat(ActualStat.ACTUAL_HP)); 
            //while (true)
            //{
            //    _stateManager.Update();
            //}
        }

        private void QuitGame()
        {
            throw new NotImplementedException();
        }

        public void PlayerWin()
        {
            Console.WriteLine("YEY !");
        }
    }
}
