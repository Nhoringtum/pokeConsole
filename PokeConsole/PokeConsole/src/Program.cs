// See https://aka.ms/new-console-template for more information

using PokeConsole.src;

class Program
{
    static void Main()
    {
        TypeComparator comparator = new TypeComparator();
        Console.WriteLine(comparator.GetMultiplierByCompareType(ElementType.FIRE, ElementType.FIRE));
        Console.WriteLine(comparator.GetMultiplierByCompareType(ElementType.FIRE, ElementType.GRASS));
        Console.WriteLine(comparator.GetMultiplierByCompareType(ElementType.FIRE, ElementType.WATER));
        //GameManager.Instance.Init();
        //GameManager.Instance.Update();
        //gameManager.Update();
    }
}

