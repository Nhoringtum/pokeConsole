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
            Console.WriteLine("aaaaa");
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
            throw new NotImplementedException();
        }

        public void Update()
        {
            while (true)
            {
                Console.WriteLine("Hello");
            }
        }

        private void QuitGame()
        {
            throw new NotImplementedException();
        }
    }
}
