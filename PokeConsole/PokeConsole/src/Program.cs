// See https://aka.ms/new-console-template for more information

using PokeConsole.src;

class Program
{
    static void Main()
    {
        GameManager.Instance.Init();
        GameManager.Instance.Update();
    }
}

