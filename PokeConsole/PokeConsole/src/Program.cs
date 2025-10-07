// See https://aka.ms/new-console-template for more information

using PokeConsole.src;
using System.Numerics;

class Program
{
    static void Main()
    {
        EntityManager entityManager = new EntityManager();

        entityManager.CreateFighter(EntityManager.entityType.SORCERER, "uif", new Vector2(10, 10), new Vector2(1, 1), ElementType.ELECTRIC);
        entityManager.CreateFighter(EntityManager.entityType.KNIGHT, "fff", new Vector2(10, 10), new Vector2(1, 1), ElementType.WATER);
        entityManager.CreateFighter(EntityManager.entityType.KNIGHT, "d", new Vector2(10, 10), new Vector2(1, 1), ElementType.WATER);
        Console.WriteLine(entityManager.GetSpecificFighter(EntityManager.entityType.KNIGHT)?.ElemType);
        Console.WriteLine(entityManager.GetSpecificFighter(EntityManager.entityType.KNIGHT)?.EntityType);
        Console.WriteLine();
        Console.WriteLine(entityManager.GetSpecificFighter(EntityManager.entityType.SORCERER)?.ElemType);
        Console.WriteLine(entityManager.GetSpecificFighter(EntityManager.entityType.SORCERER)?.EntityType);
        //GameManager.Instance.Init();
        //GameManager.Instance.Update();
        //gameManager.Update();
    }
}

