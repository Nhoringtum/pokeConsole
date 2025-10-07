using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class GameManager
    {
        private GameManager() 
        {
            stateManager.ChangeState(worldState);
        }

        StateManager stateManager = new StateManager();
        WorldState worldState = new WorldState();
        MainMenuState mainMenuState = new MainMenuState();
        FightState fightState = new FightState();

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
        }

        public void Update()
        {
            while (true)
            {
                stateManager.Update();
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
