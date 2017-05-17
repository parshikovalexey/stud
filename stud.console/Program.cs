using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using stud.data;

namespace stud.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var _db = new StudDBEntities();
            Console.WriteLine(_db.AUTOS.FirstOrDefault().AUTONUMBER.ToString());
            Console.ReadLine();
        }
    }
}
