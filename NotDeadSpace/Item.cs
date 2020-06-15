using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Item : Entity
    {
        public string name { get; private set; }
        int cost;
        float weight;

        public Item(string name, int cost, float weight)
        {
            this.name = name;
            this.cost = cost;
            this.weight = weight;

            position = new Position();
        }

        public void SetPosition(int x, int y)
        {
            position.x = x;
            position.y = y;
        }


    }
}
