using System.Numerics;
using System.Collections;

namespace TestSpeicher
{
    public class Program

    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>(2);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(5);


            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);


            ////Class Person
            //Person person1 = new Person();
            //person1.Age = 20;
            //person1.Name = ["Roman", "Vainer"];

            //Person person2 = person1;
            //person2.Age = 30;
            //person2.Name[1] = "";

            //Console.WriteLine($"[CLASS]\nPerson 1: {person1.Age}\nPerson 2: {person2.Age}\n" +
            //$"Person 1: {person1.Name[0]} {person1.Name[1]}\nPerson 2: {person2.Name[0]} {person2.Name[1]}\n\"");

            ////Struct Person
            //StructPerson sPerson1 = new StructPerson();
            //sPerson1.Age = 20;
            //sPerson1.Name = ["Roman", "Vainer"];

            //StructPerson sPerson2 = sPerson1;
            //sPerson2.Age = 30;
            //sPerson2.Name[1] = "";

            //Console.WriteLine($"[STRUCT]\nPerson 1: {sPerson1.Age}\nPerson 2: {sPerson2.Age}\n" +
            //$"Person 1: {sPerson1.Name[0]} {sPerson1.Name[1]}\nPerson 2: {sPerson2.Name[0]} {sPerson2.Name[1]}\n\"");

            //User user = new("Rom", 46);

            //Console.WriteLine($"user = {user.Name}, {user.Age}");
            //ChangeWert(ref user);
            //Console.WriteLine($"user = {user.Name}, {user.Age}");

            //var (age, name, istSchuller) = Test();
            //Console.WriteLine($"Name: {name}\nAge {age}\nistSchuler {istSchuller}");
        }


        //public static void ChangeWert(ref User element)
        //{
        //    element = new User("Bob", 20);
        //}

        //public static (int, string, bool) Test()
        //{
        //    return (45, "Roma", true);
        //}

        public struct StructPerson()
        {
            public int Age { get; set; }
            public string[] Name { get; set; } = new string[2];
        }

        public class Person
        {
            public int Age { get; set; }
            public string[] Name { get; set; } = new string[2];

        }
    }
}

public class User(int age)
{
    public int Age { get; set; } = age;
}
