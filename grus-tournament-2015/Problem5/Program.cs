using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        string str = Console.ReadLine();
        byte[] bStr = Encoding.GetEncoding("koi8r").GetBytes(str);
        string str_new = Encoding.GetEncoding(1251).GetString(bStr);
        Console.WriteLine(str_new);
    }
}

