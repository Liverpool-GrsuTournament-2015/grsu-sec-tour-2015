using System;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        byte[] arr = new byte[input.Length / 8];
        for (int i = 0; i < input.Length; i += 8)
        {
            string ss = input.Substring(i, 8);
            byte bv = Convert.ToByte(ss, 2);
            arr[i/8] = bv;
        }
        string res = Encoding.UTF8.GetString(arr);
        Console.WriteLine(res);
    }
}

