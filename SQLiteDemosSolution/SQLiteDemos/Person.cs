using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteDemos
{
    public class Person
    {
        public int Id { get; set; } //map as PKEY, auto generated

        //your attributes
        //auto or fully implemented
        public string Name { get; set; }
        public int Age { get; set; }
        public int Mark { get; set; }
       
    }
}
