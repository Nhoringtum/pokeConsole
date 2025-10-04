using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal abstract class GameStateBase
    {
        public GameStateBase() { }

        public abstract GlobalState StateID { get; }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}
