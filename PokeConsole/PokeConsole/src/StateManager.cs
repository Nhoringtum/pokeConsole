using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    public enum GlobalState
    {
        MAINMENU,
        WORLD,
        FIGHT
    }

    internal class StateManager
    {
        private IGameStateBase _currentState;

        private static readonly Dictionary<GlobalState, HashSet<GlobalState>> AllowedTransitions = new()
        {
            { GlobalState.MAINMENU, new() { GlobalState.WORLD } },
            { GlobalState.WORLD, new() { GlobalState.MAINMENU, GlobalState.FIGHT } },
            { GlobalState.FIGHT, new() { GlobalState.WORLD } },
        };

        public void ChangeState(IGameStateBase newState)
        {
            if (_currentState != null && _currentState.StateID == newState.StateID)
                return;

            if (_currentState != null && !IsTransitionAllowed(_currentState.StateID, newState.StateID))
            {
                Console.WriteLine($"Illegal transition: {_currentState.StateID} -> {newState.StateID}");
                return;
            }

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        private static bool IsTransitionAllowed(GlobalState from, GlobalState to)
        {
            return AllowedTransitions.TryGetValue(from, out var allowed) && allowed.Contains(to);
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }

}
