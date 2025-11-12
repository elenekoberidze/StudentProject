using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProject.Models
{
    public abstract class Person
    {
        public string Name { get; protected set; }
        protected Person(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            Name = name.Trim();
        }
        public abstract void PrintInfo();
    }
}
