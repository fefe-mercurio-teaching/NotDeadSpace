using System;
using System.Collections.Generic;
using System.Text;

namespace NotDeadSpace
{
    class Item
    {
        public string name { get; private set; }
        int cost;
        float weight;

        public int x { get; private set; }
        public int y { get; private set; }

        public Item(string name, int cost, float weight)
        {
            this.name = name;
            this.cost = cost;
            this.weight = weight;

            x = y = 0;
        }

        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


    }
}
