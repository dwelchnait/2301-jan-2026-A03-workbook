using System;
using System.Collections.Generic;
using System.Text;


//a namespace is use to indicate organization with our code
//all items within the same namespace can be referenced without
//  addition using clauses

namespace LearningObjects
{
    //the access level of your class definition
    //public
    //  can be use by any code that has access to the namespace
    //private
    //  ?????
    //internal 
    //  can be use by any other code within the same structure
    //      as the class definition
    public class Person
    {
        //data members
        //fields, attributes, data members
        //hold a piece of data
        //data is valuable
        //securing access by making them private
        //access and modification will be done via
        // other components of the class

        private string Name;
        private int Age;
        private decimal Wage;

        //properties

        //methods
        //constructor


        //behaviours (aka a method)
    }
}
