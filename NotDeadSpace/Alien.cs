using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Alien
    {
        int life = 3;
        int xp = 50;
        List<Item> items = new List<Item>();
        
        public int x;
        public int y;
        public int damage = 1;

        public Alien(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsHere(int x, int y)
        {
            return this.x == x && this.y == y;
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
