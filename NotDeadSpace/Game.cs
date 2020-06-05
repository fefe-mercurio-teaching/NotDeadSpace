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

        Player player = new Player();
        List<Item> inventory = new List<Item>();
        List<Item> worldItems = new List<Item>();
        List<Alien> aliens = new List<Alien>();
        TileType[,] map = new TileType[10, 10];

        bool inGame = true;

        void Init()
        {
            Item tessera = new Item("Tessera d'accesso", 0, 0f);
            tessera.SetPosition(5, 5);

            player.x = 2;
            player.y = 3;

            for (int mapY = 1; mapY < map.GetLength(1) - 1; mapY++)
            {
                for (int mapX = 1; mapX < map.GetLength(0) - 1; mapX++)
                {
                    map[mapX, mapY] = TileType.Floor;
                }
            }

            aliens.Add(new Alien(6, 5));
            worldItems.Add(tessera);
        }

        bool IsThereAnItemHere(int mapX, int mapY)
        {
            foreach(Item item in worldItems)
            {
                if (item.x == mapX && item.y == mapY)
                {
                    return true;
                }
            }

            return false;
        }

        bool IsThereAnAlienHere(int mapX, int mapY)
        {
            foreach(Alien alien in aliens)
            {
                if (alien.x == mapX && alien.y == mapY)
                {
                    return true;
                }
            }

            return false;
        }

        void DrawMap()
        {
            for (int mapY = 0; mapY < map.GetLength(1); mapY++)
            {
                for (int mapX = 0; mapX < map.GetLength(0); mapX++)
                {
                    if (player.IsHere(mapX, mapY))
                    {
                        Console.Write("+");
                    }
                    else if (IsThereAnAlienHere(mapX, mapY))
                    {
                        Console.Write("!");
                    }
                    else if (IsThereAnItemHere(mapX, mapY))
                    {
                        Console.Write("o");
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
            
            Console.WriteLine();
            Console.WriteLine(string.Format("PF: {0}/{1}", player.life, player.maxLife));

            foreach(Item item in worldItems)
            {
                if (item.x == player.x && item.y == player.y)
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
                if (player.y > 0 && map[player.x, player.y - 1] == TileType.Floor)
                {
                    player.y--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (player.y < map.GetLength(1) - 1 && map[player.x, player.y + 1] == TileType.Floor)
                {
                    player.y++;
                }
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                if (player.x > 0 && map[player.x - 1, player.y] == TileType.Floor)
                {
                    player.x--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                if (player.x < map.GetLength(0) - 1 && map[player.x + 1, player.y] == TileType.Floor)
                {
                    player.x++;
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
                if (item.x == player.x && item.y == player.y)
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

        void AI()
        {
            foreach (Alien alien in aliens)
            {
                if (Math.Abs(alien.x - player.x) <= 1 && Math.Abs(alien.y - player.y) <= 1)
                {
                    player.life -= alien.damage;
                }
                else
                {
                    if (alien.x > player.x)
                    {
                        alien.x--;
                    }
                    else if (alien.x < player.x)
                    {
                        alien.x++;
                    }

                    if (alien.y > player.y)
                    {
                        alien.y--;
                    }
                    else if (alien.y < player.y)
                    {
                        alien.y++;
                    }
                }
            }
        }

        void CheckPlayer()
        {
            if (player.life <= 0)
            {
                Console.WriteLine("SEI MORTO!");
                Console.ReadKey(false);

                inGame = false;
            }
        }

        public void GameCycle()
        {
            Init();

            do
            {
                Render();

                CheckPlayer();

                Input();
                AI();

                
            } while (inGame);
        }
    }
}
