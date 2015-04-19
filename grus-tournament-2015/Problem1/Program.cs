using System;

public class Program
{
    public static char[] _alfa = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        Console.WriteLine(DeCrypt(input, 1));
    }

    public static string DeCrypt(string input, int key)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            int number = GetNumber(input[i]);
            int newNumber = (number + key)%26;
            result = result + _alfa[newNumber];
        }
        return result;
    }

    public static int GetNumber(char c)
    {
        int number = 0;
        for (int i = 0; i < _alfa.Length; i++)
        {
            if (c == _alfa[i])
            {
                number = i;
            }
        }
        return number;
    }
}

