using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class MainMenuState : GameStateBase
    {
        private enum SubState
        {
            DEFAULT,
            NEWGAME,
            SAVE,
            SETTINGS,
        }

        private SubState _currentSub = SubState.DEFAULT;

        public override GlobalState StateID => GlobalState.MAINMENU;

        public override void Enter()
        {
            Console.WriteLine("Enter on main menu");
            Console.ReadLine();
        }

        public override void Update()
        {
            switch (_currentSub)
            {
                case SubState.DEFAULT:
                    Console.WriteLine("Show Default main menu");
                    break;
                case SubState.NEWGAME:
                    Console.WriteLine("Launch new game");
                    break;
                case SubState.SAVE:
                    Console.WriteLine("Show save menu");
                    break;
                case SubState.SETTINGS:
                    Console.WriteLine("Show settings menu");
                    break;
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Quit main menu");
        }

        private static readonly Dictionary<SubState, HashSet<SubState>> AllowedSubTransitions = new()
        {
            { SubState.DEFAULT, new() { SubState.NEWGAME, SubState.SAVE, SubState.SETTINGS } },
            { SubState.SAVE, new() { SubState.DEFAULT } },
            { SubState.SETTINGS, new() { SubState.DEFAULT } }
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
