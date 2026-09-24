namespace MyList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<int> list = new MyList<int>();
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);
            list.Add(1);
            list.Add(2);

            list.Add(10);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);


            list.Insert(1, 5);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);


            list.Remove(5);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);

            list.RemoveAt(1);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);

            list.Add(6);
            list.Add(8);
            list.Add(9);
            list.Add(10);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);

            list.RemoveAll(x => x % 2 == 0);
            Console.WriteLine(list);
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Capacity);
        }
    }
}
