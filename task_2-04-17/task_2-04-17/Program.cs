using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.IO;

namespace task_2_04_17
{
    class Program
    {
        static void Main(string[] args)
        {
            string inp = "";
            Cart cart = new Cart();
            string nm = "";
            int pr = 0;
            int qnt = 0;
            string[] splt;
            Console.WriteLine("Input items to cart: 'Name' 'Price' 'Quantity' - separated with whitespaces");
            do
            {
                try
                {
                    nm = "";
                    pr = 0;
                    qnt = 0;
                    inp = Console.ReadLine();

                    splt = inp.Split(new char[0]);

                    nm = splt[0];
                    pr = int.Parse(splt[1]);
                    qnt = int.Parse(splt[2]);

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    splt[i] = Regex.Replace(splt[i], @"\s*", "");
                    //}
                    if ((pr < 0) || (qnt <= 0)) throw new Exception();
                    cart.AddToCart(new Item(nm, pr, qnt));
                    Console.WriteLine("esc to stop");
                }
                catch (Exception) { Console.WriteLine("Incorrect input, try again."); }
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            cart.AddToCart(new Item("itm1", 1, 4));
            cart.AddToCart(new Item("itm2", 2, 5));
            cart.AddToCart(new Item("itm3", 3, 1));

            Console.WriteLine(cart.ToString());

            //XmlSerializer ser = new XmlSerializer(typeof(Cart));
            //using (StreamWriter sw = new StreamWriter("seialized_cart.xml"))
            //{
            //    ser.Serialize(sw, cart);
            //}

            //using (StreamReader sr = new StreamReader("seialized_cart.xml"))
            //{
            //    cart = (Cart)ser.Deserialize(sr);
            //}
            //Console.WriteLine(cart.ToString());

            Console.ReadLine();
        }
    }
}
