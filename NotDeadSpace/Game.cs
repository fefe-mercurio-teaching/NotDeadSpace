using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Game
    {
        enum TileType
        {
            Wall,
            Floor
        }

        int x = 0;
        int y = 0;
        List<Item> inventory = new List<Item>();
        List<Item> worldItems = new List<Item>();
        TileType[,] map = new TileType[10, 10];

        bool inGame = true;

        void Init()
        {
            Item tessera = new Item("Tessera d'accesso", 0, 0f);
            tessera.SetPosition(5, 5);

            x = 2;
            y = 3;

            for (int mapY = 1; mapY < map.GetLength(1) - 1; mapY++)
            {
                for (int mapX = 1; mapX < map.GetLength(0) - 1; mapX++)
                {
                    map[mapX, mapY] = TileType.Floor;
                }
            }

            worldItems.Add(tessera);
        }

        void DrawMap()
        {
            for (int mapY = 0; mapY < map.GetLength(1); mapY++)
            {
                for (int mapX = 0; mapX < map.GetLength(0); mapX++)
                {
                    if (mapX == x && mapY == y)
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        switch (map[mapX, mapY])
                        {
                            case TileType.Wall:
                                Console.Write("#");
                                break;

                            case TileType.Floor:
                                Console.Write(".");
                                break;
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        void Render()
        {
            Console.Clear();

            DrawMap();

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
            Console.WriteLine();
            Console.WriteLine("Tasti direzionali: Muoviti");
            Console.WriteLine("I: Inventario");
            Console.WriteLine("R: Raccogli oggetti");
            Console.WriteLine("ESC: Esci");
            Console.WriteLine();

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                inGame = false;
            }
            else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (y > 0 && map[x, y - 1] == TileType.Floor)
                {
                    y--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (y < map.GetLength(1) - 1 && map[x, y + 1] == TileType.Floor)
                {
                    y++;
                }
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                if (x > 0 && map[x - 1, y] == TileType.Floor)
                {
                    x--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                if (x < map.GetLength(0) - 1 && map[x + 1, y] == TileType.Floor)
                {
                    x++;
                }
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
