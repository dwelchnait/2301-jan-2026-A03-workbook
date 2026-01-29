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

        //typically, you will find that C# coders will place an underscore
        //  at the start of the data member names
        //this allows for the property to be called the same name but without
        //  the underscore
        //this helps in associating the data member to the supporting property
        //  in one's mind

        private string _Name;
        private int _Age;


        //properties

        //are associated with a single piece of data.
        //Properties can be implemented by:
        //  a) fully implemented property
        //  b) auto implemented property

        //A property does not need to store data
        //  this type of property is referred to as a read-only
        //  this property typically uses existing data values
        //      within the instance to return a computed value
        //A property must have a get, the set is optional

        //fully implemented properties usually has additional logic
        //  to execute for control over the data: such as validation
        //fully implemented properties will have a declared data
        //  member to store the data into

        //auto implemented properties do not have additional logic
        //Auto implemented properties do not have a declared
        //  data member instead the o/s will create on the property's
        //  behalf a storage area that is accessible ONLY by the property

        ///<summary>
        ///Property: Name
        ///datatype: string
        ///validation: there must be a character in the string
        ///a property will always have a getter (accessor)
        ///a property may or may not have a setter (mutator)
        /// no mutator the property is consider "read-only" and is
        ///         usually returning a computed field
        /// has a mutator, the property will at some point save the data
        ///     to storage
        /// the mutator may be public (default) or private
        ///     public: accessible by outside users of the class
        ///     private: accessible ONLY within the class, usually
        ///                 via the constructor or a method
        /// !!!!! a property DOES NOT have ANY declared incoming parameters !!!!!!
        /// </summary>
        ///

        //fully implemented propety version for Name is accociated with the
        //  data member _Name
        public string Name
        {
            //accessor (getter)
            //returns the string associated with this property
            get { return _Name; }

            //mutator (setter)
            //the incoming data value is accessed within the property
            //  using the keyword: value
            //the incoming value to the instance's property  will be stored
            //  in the data member _Name.

            //it is within the set that the validation of the data
            //  is done to determine if the data is acceptable
            //if all processing of the string is done via the property
            //  it will ensure that good data is within the associated string
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    //classes do not output directly to the program user
                    //classes throw Exceptions that are handled by the program and displayed to the 
                    //  human user
                    //there are many types of exceptions within C#
                    // general, catchall, exception: Exception
                    // specific exceptions:
                    //      ArgumentNullException (used when missing a value)
                    //      ArgumentException ( used when the value supplied is incorrect)
                    throw new ArgumentNullException("Name", "Name cannot be empty or just blanks");
                }
                else
                {
                    //it is a very good practice to remove leading and trailing spaces on strings
                    //  so that only the required and important characters are stored.
                    //to do this sanitization use .Trim()
                    //this does not remove embedded blanks
                    _Name = value.Trim();
                }

            }
        }

        //auto implemented property version for Name
        //public string Name { get; set; }

        ///<summary>
        ///Property: Age
        ///validation: the value must be 0 or greater
        ///datatype: int
        ///</summary>
        ///

        public int Age
        {
            get { return _Age; }
            set
            {
                if (value < 0)
                    // {
                    // a throw cause execution to stop and exit the property or method
                    throw new ArgumentException($"The age {value} is invalid. Age must be 0 or greater", "Age");
                // }
                // else
                // {
                _Age = value;
                // }
            }
        } //eop end of property

        //create an auto implemented property for Wage data
        //the o/s will create the storage area for saving the data within the class instance (object)

        //one option on the setter is to make it private
        //if private:
        //  changes to this data will ONLY be done via a constructor or a method
        //  one will NOT be able to directly change the data via the property
        //  DO NOT make your getter private

        //if you validation is NOT in the property, EVERY time you alter the data
        //  you NEED to consider if validation is required.
        //if validation is required, that validation MUST be placed in your code
        //  WHEREVER the alternation is done

        public decimal Wage { get; private set; }


        //methods
        //constructor

        //constructor ensure that when an class instance (object) is created, the contents
        //  of the object will be as "expected" (in a known state)

        //your class does not technically need a coded constructor
        //if you code a constructor for your class you are responsible for coding ALL constructors
        //if you do not code a constructor then the system will assign the software datatype defaults
        //  to your variables (data members/auto-implemented properties)

        //syntax: accesslevel constructorname([list of parameters]) { .... }
        //NOTE: NO return datatype
        //      the constructorname MUST be the class name

        //Default
        //simulates the "system defaults"
        public Person()
        {
            //if there is no code within this constructor, the actions for setting
            //  your internal fields will be using the system defaults for the datatype

            //optionally!!!!!
            // you could assign values to your initial fields within this constructor typically
            //      using literal values
            //Why?
            // your internal fields may have validation attached to the data for the field
            // this validation is usually within the property
            // you would wish to have valid data values for your internal fields

            //BEST PRACTICE: always wherever possible save and access your instance data
            //                  via the properties, specially, when the property contains validation
            Name = "Unknown";

            //Age and Wage
            //the default values is 0 (fine)
            //HOWEVER, if you wish you could code the initial value
            Age = 0;
            Wage = 0.0m; //m is need as the literal needs to be a decimal not double
        }

        //Greedy
        //this is the constructor typically used to assign values to a instance at the time of
        //    creation
        //the list of parameters may or may not contain default parameter values
        //if you have assigned default parameter values then those parameters MUST be at the end of
        //  the parameter list
        //in this example wage is a default parameter (it has an assigned value if the value
        //  is not included on the coded constructor in the user program
        public Person(string name, int age, decimal wage = 0.00m)
        {
            Name = name;
            Age = age;

            //if wage was NOT on the call statement then the value of the parameter
            //  defaults to 0.00
            //if wage was supplied on the call statement then the supplied value is the
            //  contents of the parameter

            //one could add validation to the constructor, especially if the property has
            //  a private set OR 
            //  the property is an auto-implemented property that has restrictions
            //example: Wage
            //         must be 0 or greater
            if (wage < 0)
                throw new ArgumentException($"The wage {wage} is invalid. Wage must be 0 or greater", "Wage");
            Wage = wage;
        }


        //Methods (aka Behaviours)

        //syntax of a general method
        // accesslevel [override] returndatatype methodname ([list of parameters])
        // {
        //     coding block
        //  }

        // realize that to use a method or any item within your instance you must have the instance
        //  if you have the instance then you have the data within the instance ALREADY
        //  THEREFORE, if the data is within the instance you DO NOT need to pass the data into your method

        //we would like to dump the data within the instance to "somewhere"
        public override string ToString()
        {
            //each instance has a ToString() method as part of its construction
            //you can overide the default coding of the supplied method
            //to use your own code, create the method with the special keyword "override"
            //in this example the delimiter for the value will be a ; due to the fact
            //  I wish to use a comma , in the Wage display
            return $"{Name};{Age};{Wage.ToString("#,##0.00")}";
        }

        public decimal CalculateBonus(decimal yearsofservice)
        {
            //Notice: Wage is not passed in, as it is already known within the instance
            // yearsofservice is NOT stored within the instance THERFORE it MUST be a parameter
            return Wage * yearsofservice;
        }

        //this method will allow the alteration of the Wage data
        //if the setter on Wage was NOT private then this method would NOT be needed
        public void ChangeWage(decimal wage)
        {
            if (wage < 0)
                throw new ArgumentException($"The wage {wage} is invalid. Wage must be 0 or greater", "Wage");
            Wage = wage;
        }
    }
}
