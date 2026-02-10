using System;
using System.Collections.Generic;
using System.Text;

namespace LinqExploration
{
    public class ProjectedPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Bonus { get; set; }
        public ProjectedPerson()
        {
        }
        public ProjectedPerson(string name, int age, decimal bonus)
        {
            Name = name;
            Age = age;
            Bonus=bonus;
        }
    }
}
