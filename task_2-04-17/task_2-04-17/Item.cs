using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_2_04_17
{
    class Item
    {
        public string Name { set; get; }
        public int Price { set; get; }
        public int Quantity { set; get; }
        public Item()
        {
            Name = "";
            Price = 0;
            Quantity = 0;
        }
        public Item(string name, int price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
