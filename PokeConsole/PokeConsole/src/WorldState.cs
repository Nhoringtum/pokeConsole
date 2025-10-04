using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class WorldState : GameStateBase
    {
        private enum SubState
        {
            DEFAULT,
            MENU,
            INVENTORY,
            TEAM,
            SAVE,
            EXIT
        }

        private SubState _currentSub = SubState.DEFAULT;

        public override GlobalState StateID => GlobalState.WORLD;

        public override void Enter()
        {
            Console.WriteLine("Enter on WorldState");
        }

        public override void Update()
        {
            switch (_currentSub)
            {
                case SubState.DEFAULT:
                    Console.WriteLine("Shows world");
                    break;
                case SubState.MENU:
                    Console.WriteLine("Shows global menu");
                    break;
                case SubState.INVENTORY:
                    Console.WriteLine("Shows inventory menu");
                    break;
                case SubState.TEAM:
                    Console.WriteLine("Shows team menu");
                    break;
                case SubState.SAVE:
                    Console.WriteLine("Shows save menu");
                    break;
                case SubState.EXIT:
                    Console.WriteLine("Leave Game");
                    break;
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Exit world state");
        }

        private static readonly Dictionary<SubState, HashSet<SubState>> AllowedSubTransitions = new()
        {
            { SubState.DEFAULT, new() { SubState.MENU } },
            { SubState.MENU, new() { SubState.INVENTORY, SubState.TEAM, SubState.SAVE, SubState.DEFAULT, SubState.EXIT } },
            { SubState.INVENTORY, new() { SubState.MENU } },
            { SubState.TEAM, new() { SubState.MENU } },
            { SubState.SAVE, new() { SubState.MENU } }
        };

        private void ChangeSubState(SubState newSub)
        {
            if (_currentSub == newSub) return;

            if (!AllowedSubTransitions.TryGetValue(_currentSub, out var allowed) || !allowed.Contains(newSub))
            {
                Console.WriteLine($"Illegal sub-state transition: {_currentSub} -> {newSub}");
                return;
            }

            Console.WriteLine($"Substate: {_currentSub} -> {newSub}");

            _currentSub = newSub;
        }
    }
}
