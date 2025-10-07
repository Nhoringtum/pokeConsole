using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class GameManager
    {
        StateManager _stateManager;
        WorldState _worldState;
        MainMenuState _mainMenuState; 
        FightState _fightState;

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
        }

        public void Update()
        {
            while (true)
            {
                _stateManager.Update();
            }
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
