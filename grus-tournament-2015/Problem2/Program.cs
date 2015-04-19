using System;
using System.Text;

namespace Problem2
{
    class Program
    {
        static void Main(string[] args)
        {
            var offset = 13;
            var encrypted = Console.ReadLine();

            Console.WriteLine(Decrypt(encrypted, offset));

        }

        private static string Decrypt(string encrypted, int offset)
        {
            const char a_char = 'a';
            const char z_char = 'z';
            int alfabetLength = z_char - a_char + 1;

            var decrypted = new StringBuilder(encrypted.Length);

            foreach (char encr_char in encrypted)
            {
                int decr_char = ((encr_char - a_char) - offset + alfabetLength)%alfabetLength;
                decrypted.Append((char)(decr_char + a_char));
            }

            return decrypted.ToString();
        }
    }
}
