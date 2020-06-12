using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Alien : Entity
    {
        int life = 3;
        int xp = 50;
        List<Item> items = new List<Item>();
        
        public int damage = 1;

        public Alien(int x, int y)
        {
            position.x = x;
            position.y = y;
        }

        public bool IsHere(int x, int y)
        {
            return position.IsHere(x, y);                   // Le due righe sono equivalenti
            //return position.x == x && position.y == y;
        }

        public void Damage(int amount)
        {
            life -= amount;
        }

        public void AddItem(Item newItem)
        {
            items.Add(newItem);
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public bool IsDead
        {
            get
            {
                return life <= 0;
            }
        }

        public int XP
        {
            get
            {
                return xp;
            }
        }

        //public bool IsDead()
        //{
        //    return life <= 0;
        //}
    }
}
