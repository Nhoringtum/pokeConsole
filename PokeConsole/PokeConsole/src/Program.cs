// See https://aka.ms/new-console-template for more information

using PokeConsole.src;
using System.Numerics;
using static PokeConsole.src.SkillsManager;

class Program
{
    static void Main()
    {
        GameManager.Instance.Update();
    }
}

