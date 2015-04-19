using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem9
{
    class Program
    {
        private static double fromPercents = 0.01;

        private static Dictionary<char, double> letFreq = new Dictionary<char, double>
        {
            {'E', 12.70*fromPercents},
            {'T', 9.06*fromPercents},
            {'A', 8.17*fromPercents},
            {'O', 7.51*fromPercents},
            {'I', 6.97*fromPercents},
            {'N', 6.75*fromPercents},
            {'S', 6.33*fromPercents},
            {'H', 6.09*fromPercents},
            {'R', 5.99*fromPercents},
            {'D', 4.25*fromPercents},
            {'L', 4.03*fromPercents},
            {'C', 2.78*fromPercents},
            {'U', 2.76*fromPercents},
            {'M', 2.41*fromPercents},
            {'W', 2.36*fromPercents},
            {'F', 2.23*fromPercents},
            {'G', 2.02*fromPercents},
            {'Y', 1.97*fromPercents},
            {'P', 1.93*fromPercents},
            {'B', 1.49*fromPercents},
            {'V', 0.98*fromPercents},
            {'K', 0.77*fromPercents},
            {'X', 0.15*fromPercents},
            {'J', 0.15*fromPercents},
            {'Z', 0.07*fromPercents},
            {'Q', 0.01*fromPercents}
        };

        static string TestText = @"FUBSWRJUDSKB LV WKH SUDFWLFH DQG VWXGB RI WHFKQLTXHV IRU, DPRQJ RWKHU WKLQJV, VHFXUH FRPPXQLFDWLRQ 
LQ WKH SUHVHQFH RI DWWDFNHUV. 
FUBSWRJUDSKB KDV EHHQ XVHG IRU 
KXQGUHGV, LI QRW WKRXVDQGV, RI 
BHDUV, EXW WUDGLWLRQDO 
FUBSWRVBVWHPV ZHUH GHVLJQHG DQG 
HYDOXDWHG LQ D IDLUOB DG KRF 
PDQQHU. IRU HADPSOH, WKH YLJHQHUH 
HQFUBSWLRQ VFKHPH ZDV WKRXJKW WR 
EH VHFXUH IRU GHFDGHV DIWHU LW ZDV 
LQYHQWHG, EXW ZH QRZ NQRZ, DQG 
WKLV HAHUFLVH GHPRQVWUDWHV, WKDW 
LW FDQ EH EURNHQ YHUB HDVLOB.";
        private static string TestText2 = @"PDEO WNPEYHA EJYHQZAO W HEOP KB 
NABANAJYAO, XQP EPO OKQNYAO NAIWEJ 
QJYHAWN XAYWQOA EP DWO 
EJOQBBEYEAJP EJHEJA YEPWPEKJO. 
LHAWOA DAHL PK EILNKRA PDEO 
WNPEYHA XU EJPNKZQYEJC IKNA 
LNAYEOA YEPWPEKJO";

        static void Main(string[] args)
        {

            string text = Console.ReadLine();
            var textBuilder = new StringBuilder(text);
            var decryptor = new SimpleCipher();
            decryptor.LoadFreq(letFreq);

            Console.WriteLine(decryptor.SimpleDecryption(textBuilder) ?? string.Empty);
        }
    }

    /// <summary>
    /// Класс представляющий набор метод для рассшифровки некоторых шифров замены
    /// </summary>
    internal class SimpleCipher
    {
        private Dictionary<char, long> lettersFreq = null;
        private Dictionary<char, double> lettersFreqD = null;
        const char a_char = 'A';
        const char z_char = 'Z';
        const int alfabetLength = z_char - a_char + 1;

        public void LoadFreq(Dictionary<char, double> letfreq)
        {
            lettersFreqD = letfreq;
        }

        /// <summary>
        /// Метод расшифровывает текст, который передается параметром secretL
        /// </summary>
        /// <param name="secretL">Зашифрованный текст</param>
        /// <returns></returns>
        public string SimpleDecryption(StringBuilder secretL)
        {
            var decryptedText = new List<string>();

            for (int offset = 0; offset <= alfabetLength; offset++)
            {
                var decryptedTempText = new StringBuilder(secretL.Length);
                foreach (var encr_char in secretL.ToString())
                {
                    if (!Char.IsLetter(encr_char)) decryptedTempText.Append(encr_char);
                    else
                    {
                        int decr_char = (encr_char - a_char - offset + alfabetLength) % alfabetLength;
                        decryptedTempText.Append((char)(decr_char + a_char));
                    }
                }

                //проверка частот букв
                if (checkLettersFreq(decryptedTempText))
                {
                    decryptedText.Add(decryptedTempText.Insert(0, offset + "\n").ToString());
                    break;
                }
            }

            return decryptedText.FirstOrDefault();
        }

        /// <summary>
        /// Метод проверяет частотные характеристики текста. Если большинство частот
        /// не сильно разнится с частотами в словаре lettersFreq - возвращает TRUE.
        /// </summary>
        /// <param name="text">Проверяемый текст</param>
        /// <returns></returns>
        private bool checkLettersFreq(StringBuilder text)
        {
            double count = 0;
            Dictionary<char, long> tempDict = new Dictionary<char, long>();
            for (int i = 0; i < text.Length; i++)
            {
                if (!Char.IsLetter(text[i])) continue;
                count++;
                if (tempDict.ContainsKey(text[i])) tempDict[text[i]]++;
                else tempDict.Add(text[i], 1);
            }
            int ERRORS = 2 * tempDict.Keys.Count / 3;
            int errors = 0;
            double ksi = 0.05;
            if (text.Length > 150) ksi = 0.01;
            double percentT = 0;
            double percentAbsolute = 0;
            foreach (char a in tempDict.Keys)
            {
                percentT = tempDict[a] / count;
                percentAbsolute = lettersFreqD[a];
                if (Math.Abs(percentAbsolute - percentT) > ksi)
                    errors++;
            }
            return errors < ERRORS;
        }
    }
}
