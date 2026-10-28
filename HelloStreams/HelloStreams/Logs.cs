using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace HelloStreams;

public class Logs
{
    public static void ReadLogs(string path)
    {
        using StreamReader sr = new(path);
        string s;
        while ((s = sr.ReadLine()!) != null)
        {
            Console.WriteLine(s);
        }
    } 
}
