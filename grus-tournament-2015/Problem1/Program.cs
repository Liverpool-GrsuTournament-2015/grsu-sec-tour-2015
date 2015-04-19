using System;
using System.Text;

public class Program
{
    public static char[] _alfa = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

    public static void Main(string[] args)
    {
        string input = Console.ReadLine();
        Console.WriteLine(Decrypt(input, -1));
    }

    private static string Decrypt(string encrypted, int offset)
    {
        const char a_char = 'a';
        const char z_char = 'z';
        int alfabetLength = z_char - a_char + 1;

        var decrypted = new StringBuilder(encrypted.Length);

        foreach (char encr_char in encrypted)
        {
            int decr_char = ((encr_char - a_char) - offset + alfabetLength) % alfabetLength;
            decrypted.Append((char)(decr_char + a_char));
        }

        return decrypted.ToString();
    }

    public static string DeCrypt_Slow(string input, int key)
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

