using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace task_2_04_17
{
    class Cart : IEnumerable
    {
        [XmlArrayItem]
        public Item[] _cart = new Item[1];
        public int cur = 0;

        public int Capacity { get { return _cart.Length; } }
        public int _totalPrice
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < cur; i++)
                    sum += _cart[i].Price;
                return sum;
            }
        }
        public Cart() { }

        public Cart(int capacity)
        {   
            if (capacity > 0) _cart = new Item[capacity];
            for (int i = 0; i < capacity; i++) _cart[i] = new Item();
        }

        public void IncreaseSize()
        {
            Item[] temp_cart = _cart;
            _cart = new Item[this.Capacity + 3];
            for (int i = 0; i < this.Capacity; i++) _cart[i] = new Item();
            for (int i = 0; i < cur; i++) _cart[i] = temp_cart[i];
            Console.WriteLine("Size increased");
        }

        public void AddToCart(Item item)
        {
            if (cur< Capacity)
            {
                _cart[cur] = item;
            }
            else
            {
                this.IncreaseSize();
                _cart[cur] = item;
            }
            cur++;
        }

        public IEnumerator GetEnumerator()
        {
            return _cart.GetEnumerator();
        }

        public override string ToString()
        {
            string tostr = "Name \t Price \t Quantity \n";
            Array.Sort(_cart, delegate (Item item1, Item item2)
            {
                return (- item1.Price * item1.Quantity + item2.Price * item2.Quantity);
            });
            for (int i = 0; i < cur; i++)
            {
                tostr += _cart[i].Name + " \t " + _cart[i].Price.ToString() + " \t " + _cart[i].Quantity.ToString()+"\n";
            }
            return tostr;
        }
    }
}
