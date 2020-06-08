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
            //Item tessera = new Item("Tessera d'accesso", 0, 0f);
            //tessera.SetPosition(5, 5);

            player.x = 2;
            player.y = 3;

            for (int mapY = 1; mapY < map.GetLength(1) - 1; mapY++)
            {
                for (int mapX = 1; mapX < map.GetLength(0) - 1; mapX++)
                {
                    map[mapX, mapY] = TileType.Floor;
                }
            }

            GenerateAlienAtRandomPosition();

            //worldItems.Add(tessera);
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
            Console.WriteLine($"PF: {player.life}/{player.maxLife} Livello: {player.Level} XP: {player.XP}/100 Danno: {player.damage}");

            foreach(Item item in worldItems)
            {
                if (item.x == player.x && item.y == player.y)
                {
                    Console.WriteLine("Qui c'è: " + item.name);
                }
            }
        }

        Alien GetAlienAtPosition(int x, int y)
        {
            foreach (Alien alien in aliens)
            {
                if (alien.IsHere(x, y))
                {
                    return alien;
                }
            }

            return null;
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

            // La nuova posizione assunta dal giocatore nel caso di spostamento
            int newX = player.x;
            int newY = player.y;
            
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                inGame = false;
            }
            else if (keyInfo.Key == ConsoleKey.W || keyInfo.Key == ConsoleKey.UpArrow)
            {
                newY--;
                //if (player.y > 0 && map[player.x, player.y - 1] == TileType.Floor)
                //{
                //    Alien alienFound = GetAlienAtPosition(player.x, player.y - 1);

                //    if (alienFound != null)
                //    {
                //        alienFound.Damage(player.damage);
                //    }
                //    else
                //    {
                //        player.y--;
                //    }
                //}
            }
            else if (keyInfo.Key == ConsoleKey.S || keyInfo.Key == ConsoleKey.DownArrow)
            {
                newY++;
                //if (player.y < map.GetLength(1) - 1 && map[player.x, player.y + 1] == TileType.Floor)
                //{
                //    Alien alienFound = GetAlienAtPosition(player.x, player.y + 1);

                //    if (alienFound != null)
                //    {
                //        alienFound.Damage(player.damage);
                //    }
                //    else
                //    {
                //        player.y++;
                //    }
                //}
            }
            else if (keyInfo.Key == ConsoleKey.A || keyInfo.Key == ConsoleKey.LeftArrow)
            {
                newX--;
                //if (player.x > 0 && map[player.x - 1, player.y] == TileType.Floor)
                //{
                //    Alien alienFound = GetAlienAtPosition(player.x - 1, player.y);

                //    if (alienFound != null)
                //    {
                //        alienFound.Damage(player.damage);
                //    }
                //    else
                //    {
                //        player.x--;
                //    }
                //}
            }
            else if (keyInfo.Key == ConsoleKey.D || keyInfo.Key == ConsoleKey.RightArrow)
            {
                newX++;
                //if (player.x < map.GetLength(0) - 1 && map[player.x + 1, player.y] == TileType.Floor)
                //{
                //    Alien alienFound = GetAlienAtPosition(player.x + 1, player.y);

                //    if (alienFound != null)
                //    {
                //        alienFound.Damage(player.damage);
                //    }
                //    else
                //    {
                //        player.x++;
                //    }
                //}
            }
            else if (keyInfo.Key == ConsoleKey.I)
            {
                PrintInventory();
            }
            else if (keyInfo.Key == ConsoleKey.R)
            {
                PickUpItems();    
            }

            // Se newX è diverso da player.x O newY è diverso da player.y
            if (player.x != newX || player.y != newY)
            {
                newX = Math.Clamp(newX, 0, map.GetLength(0) - 1); // Fa in modo che newX sia compreso tra 0 e map.GetLength(0) - 1
                newY = Math.Clamp(newY, 0, map.GetLength(1) - 1); // Come sopra

                if (map[newX, newY] == TileType.Floor)
                {
                    Alien alienFound = GetAlienAtPosition(newX, newY); // Controlla se è presente un alieno nella posizione che si sta per raggiungere
                    if (alienFound != null)
                    {
                        alienFound.Damage(player.damage); // Attacca l'alieno
                    }
                    else
                    {
                        player.x = newX; // Cambia la posizione reale del giocatore
                        player.y = newY; // Come sopra
                    }
                }
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
            List<Alien> aliensToRemove = new List<Alien>();

            foreach (Alien alien in aliens)
            {
                if (alien.IsDead)
                {
                    player.AddXP(alien.XP);
                    aliensToRemove.Add(alien);
                }
                else
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

            foreach (Alien alien in aliensToRemove)
            {
                GenerateAlienAtRandomPosition();

                // Aggiunge tutti gli oggetti che l'alieno possedeva alla lista worldItems
                foreach (Item item in alien.GetItems())
                {
                    item.SetPosition(alien.x, alien.y); // Gli oggetti sono posizionati dove si trovava l'alieno
                    worldItems.Add(item);
                }
                
                aliens.Remove(alien); // Rimuove l'alieno ucciso dalla lista
            }
        }

        void GenerateAlienAtRandomPosition()
        {
            Random random = new Random();

            int alienX = 0;
            int alienY = 0;

            // Genera una posizione casuale per il nuovo alieno facendo in modo che non sia
            // un muro e che non si trovi il giocatore lì
            do
            {
                alienX = random.Next(0, map.GetLength(0));
                alienY = random.Next(0, map.GetLength(1));
            } while (map[alienX, alienY] != TileType.Floor || player.IsHere(alienX, alienY));

            Alien newAlien = new Alien(alienX, alienY);

            // Una probabilità su 20 che l'alieno abbia la tessera d'accesso
            if (random.Next(0, 5) == 0)
            {
                newAlien.AddItem(new Item("Tessera d'accesso", 0, 0f));
            }

            aliens.Add(newAlien);
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
