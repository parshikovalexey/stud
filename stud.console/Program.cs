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
            var list = _db.NOTES.ToList();
           
            Console.ReadLine();
        }
    }
}
