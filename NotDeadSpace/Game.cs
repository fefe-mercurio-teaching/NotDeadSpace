using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Game
    {
        int x = 0;
        int y = 0;
        List<Item> inventory = new List<Item>();
        List<Item> worldItems = new List<Item>();

        bool inGame = true;

        void Init()
        {
            Item tessera = new Item("Tessera d'accesso", 0, 0f);
            tessera.SetPosition(5, 5);

            worldItems.Add(tessera);
        }

        void Render()
        {
            Console.Clear();

            Console.WriteLine("X: " + x);
            Console.WriteLine("Y: " + y);

            foreach(Item item in worldItems)
            {
                if (item.x == x && item.y == y)
                {
                    Console.WriteLine("Qui c'è: " + item.name);
                }
            }
        }

        void Input()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                inGame = false;
            }
            else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                y++;
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                y--;
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                x--;
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                x++;
            }
            else if (keyInfo.Key == ConsoleKey.I)
            {
                PrintInventory();
            }
            else if (keyInfo.Key == ConsoleKey.R)
            {
                PickUpItems();    
            }
        }

        void PickUpItems()
        {
            // Devo usare questa lista perché non posso rimuovere oggetti da una lista durante
            // un foreach su di essa.
            List<Item> itemsToRemove = new List<Item>();

            foreach (Item item in worldItems)
            {
                if (item.x == x && item.y == y)
                {
                    inventory.Add(item);
                    itemsToRemove.Add(item); // Non posso fare worldItems.Remove perché il ciclo foreach è sulla lista worldItems

                    Console.WriteLine("- Ho raccolto l'oggetto " + item.name);
                }
            }

            foreach (Item item in itemsToRemove)
            {
                worldItems.Remove(item);
            }

            Console.ReadKey(true);
        }

        void PrintInventory()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("- Non ci sono oggetti nell'inventario");
            }
            else
            {
                foreach (Item item in inventory)
                {
                    Console.WriteLine("- " + item.name);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Premi un tasto per continuare");
            Console.ReadKey(true);
        }

        public void GameCycle()
        {
            Init();

            do
            {
                Render();
                Input();

            } while (inGame);
        }
    }
}
