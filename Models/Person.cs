using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }


        public Person[] GetPeople()
        {

            List<Person> people = new List<Person>();

            people.Add(new Person {
                Name = "John Doe",
                Age = 23,
                DOB = DateTime.Now
            });

            people.Add(new Person
            {
                Name = "Jane Doe",
                Age = 24,
                DOB = DateTime.Now.AddDays(20)
            });

            people.Add(new Person
            {
                Name = "John Wick",
                Age = 50,
                DOB = DateTime.Now.AddYears(-50)
            });

            people.Add(new Person
            {
                Name = "Jane Wick",
                Age = 23,
                DOB = DateTime.Now.AddDays(-100)
            });

            return people.ToArray();
        }

    }

}