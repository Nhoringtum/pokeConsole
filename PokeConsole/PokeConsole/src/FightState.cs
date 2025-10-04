using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeConsole.src
{
    internal class FightState : GameStateBase
    {
        private enum SubState
        {
            DEFAULT,
            ATTACK,
            INVENTORY,
            TEAM,
            ESCAPE
        }

        private SubState _currentSub = SubState.DEFAULT;

        public override GlobalState StateID => GlobalState.FIGHT;

        public override void Enter()
        {
            Console.WriteLine("Enter on FightState");
        }

        public override void Update()
        {
            switch (_currentSub)
            {
                case SubState.DEFAULT:
                    Console.WriteLine("Shows Default fight menu");
                    break;
                case SubState.ATTACK:
                    Console.WriteLine("Shows attack menu");
                    break;
                case SubState.INVENTORY:
                    Console.WriteLine("Shows inventory menu");
                    break;
                case SubState.TEAM:
                    Console.WriteLine("Show team menu");
                    break;
                case SubState.ESCAPE:
                    Console.WriteLine("Leave fight");
                    break;
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Exit on FightState");
        }

        private static readonly Dictionary<SubState, HashSet<SubState>> AllowedSubTransitions = new()
        {
            { SubState.DEFAULT, new() { SubState.ATTACK, SubState.INVENTORY, SubState.INVENTORY, SubState.TEAM, SubState.ESCAPE } },
            { SubState.ATTACK, new() { SubState.DEFAULT} },
            { SubState.INVENTORY, new() { SubState.DEFAULT } },
            { SubState.TEAM, new() { SubState.DEFAULT } },
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
