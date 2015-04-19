using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Problem9
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            var textBuilder = new StringBuilder(text);
            var decryptor = new SimpleCipher(text, Ciphers.Affine, Locale.en);

            decryptor.LoadFreq(null, null);
            decryptor.SimpleDecryption(textBuilder);
        }
    }

    enum Ciphers { KeyAlphabet = 0, Simple, Affine }
    enum Locale { en, ru }

    /// <summary>
    /// Класс представляющий набор метод для рассшифровки некоторых шифров замены
    /// </summary>
    internal class SimpleCipher
    {
        private readonly char[] alphabet;
        private readonly int alphaLenght;
        private readonly Locale lang;
        private Dictionary<char, long> lettersFreq = null;
        private Dictionary<string, long> bigrammsFreq = null;
        public StringBuilder secrtLang = null;
        private string file_letters = "lettersFreqFIle";
        private string file_bigramms = "bigrammsFreqFIle";

        /// <summary>
        /// Инициализирует объект класс SimpleCipher
        /// </summary>
        /// <param name="secretLanguage">Зашифрованный текст</param>
        /// <param name="enu">Предполагаемы тип шифрования</param>
        public SimpleCipher(string secretLanguage, Ciphers enu, Locale lang)
        {
            this.lang = lang;
            switch (lang)
            {
                case Locale.en:
                    alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F',
                                           'G', 'H', 'I', 'J', 'K', 'L', 'M',
                                           'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                                           'U', 'V', 'W', 'X', 'Y', 'Z' };
                    break;
                case Locale.ru:
                    alphabet = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж',
                                           'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О',
                                           'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц',
                                           'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
                    break;
                default:
                    break;
            }
            alphaLenght = alphabet.Length;
            file_letters = "lettersFreqFIle_" + lang + ".txt";
            file_bigramms = "bigrammsFreqFIle_" + lang + ".txt";
            secrtLang = new StringBuilder(secretLanguage);
            switch (enu)
            {
                case Ciphers.KeyAlphabet:
                    break;
                case Ciphers.Simple:
                    break;
                case Ciphers.Affine:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Загрузить словари биграмм и букв из файлов со стандартными именами "lettersFreqFIle.txt", "bigrammsFreqFIle.txt"
        /// </summary>
        public void LoadFreq()
        {
            StreamReader rd = new StreamReader(file_bigramms, Encoding.GetEncoding(65001));
            string temp = rd.ReadToEnd().Trim();
            string[] temps = temp.Split(' ');
            if (bigrammsFreq != null) bigrammsFreq.Clear();
            else bigrammsFreq = new Dictionary<string, long>();
            for (int i = 0; i < temps.Length; i += 2)
            {
                bigrammsFreq.Add(temps[i], Convert.ToInt64(temps[i + 1]));
            }
            rd.Close();
            rd = new StreamReader(file_letters, Encoding.GetEncoding(65001));
            temp = rd.ReadToEnd().Trim();
            temps = temp.Split(' ');
            if (lettersFreq != null) lettersFreq.Clear();
            else lettersFreq = new Dictionary<char, long>();
            for (int i = 0; i < temps.Length; i += 2)
            {
                lettersFreq.Add(temps[i].ToCharArray()[0], Convert.ToInt64(temps[i + 1]));
            }
            rd.Close();
        }


        public void LoadFreq(Dictionary<char, long> letfreq, Dictionary<char, long> bigrfreq) { }

        /// <summary>
        /// Возвращает номер цифры в алфавите
        /// </summary>
        /// <param name="literal"></param>
        /// <returns></returns>
        public int IndexAlphaBet(char literal)
        {
            int temp = 10000;
            literal = char.ToUpper(literal);
            for (int i = 0; i < alphaLenght; i++)
            {
                if (literal == alphabet[i]) { temp = i; break; }
            }
            return temp;
        }
        /// <summary>
        /// Возвращает коллекцию слов
        /// </summary>
        /// <param name="path">путь к файлу с текстом</param>
        /// <param name="words">Коллекция в которую будут записаны слова из текста</param>
        /// <returns></returns>
        private List<string> GetWords(string path, List<string> words)
        {
            words.Clear();
            String text = File.ReadAllText(path, Encoding.GetEncoding(1251)).ToUpperInvariant();
            Regex toWords = null;
            switch (lang)
            {
                case Locale.en:
                    toWords = new Regex("[a-z]+", RegexOptions.IgnoreCase);
                    break;
                case Locale.ru:
                    toWords = new Regex("[а-яёъьщый]+", RegexOptions.IgnoreCase);
                    break;
                default:
                    break;
            }
            Match m = toWords.Match(text);
            while (m.Success)
            {
                words.Add(m.Value);
                m = m.NextMatch();
            }
            return words;
        }
        /// <summary>
        /// Возвращает коллекцию слов выдернутых из text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private List<StringBuilder> GetWords(StringBuilder text)
        {
            string textS = text.ToString();
            List<StringBuilder> words = new List<StringBuilder>();
            Regex toWords = null;
            switch (lang)
            {
                case Locale.en:
                    toWords = new Regex("[a-z]+", RegexOptions.IgnoreCase);
                    break;
                case Locale.ru:
                    toWords = new Regex("[а-яёъьщый]+", RegexOptions.IgnoreCase);
                    break;
                default:
                    break;
            }
            Match m = toWords.Match(textS);
            while (m.Success)
            {
                words.Add(new StringBuilder(m.Value));
                m = m.NextMatch();
            }
            return words;
        }
        /// <summary>
        /// Составить словари частот биграмм и букв, используя файлы с текстом на русском языке
        /// </summary>
        /// <param name="path">Путь к файлам по которым будут генерироваться частоты</param>
        /// <param name="count_of_files">Колличество таких файлов. Имена файлов отличаются только на цифру (file0, file1, file2,...)</param>
        public void CalculateFrequnсies(string path, int count_of_files)
        {
            List<string> words = new List<string>();
            BuildBigramms();
            BuildMonogramms();
            int countOfliterals = 0, countOfbi = 0;
            for (int file = 0; file < count_of_files; file++)
            {
                Console.WriteLine(file);
                words = GetWords(path + file + "_" + lang + ".txt", words);
                for (int word = 0; word < words.Count; word++)
                {
                    IncrementBigramms(words[word], ref countOfbi);
                    foreach (char a in words[word])
                    {
                        lettersFreq[a]++;
                        countOfliterals++;
                    }
                }
            }
            bigrammsFreq.Add("size", countOfliterals);
            lettersFreq.Add('!', countOfliterals);
            ICollection bi = bigrammsFreq.Keys;
            ICollection alph = lettersFreq.Keys;
            StreamWriter wr_l = new StreamWriter(file_letters);
            StreamWriter wr_bi = new StreamWriter(file_bigramms);
            foreach (char c in alph)
                wr_l.Write(c + " " + lettersFreq[c] + " ");
            foreach (string c in bi)
                wr_bi.Write(c + " " + bigrammsFreq[c] + " ");
            wr_bi.Close();
            wr_l.Close();
            Console.WriteLine("end");
        }
        private void IncrementBigramms(string word, ref int count)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                //string bi = word.Substring(i, 2);// new String(new char[] { word[i - 1], word[i] });
                bigrammsFreq[word.Substring(i, 2)]++;
                count++;
            }
        }
        private void BuildBigramms()
        {
            bigrammsFreq = new Dictionary<string, long>();
            foreach (char a in alphabet)
                foreach (char b in alphabet)
                    bigrammsFreq.Add(a.ToString() + b.ToString(), 0);
        }
        private void BuildMonogramms()
        {
            lettersFreq = new Dictionary<char, long>();
            foreach (char a in alphabet)
                lettersFreq.Add(a, 0);
        }
        /// <summary>
        /// Метод шифрует текстовую строку text экземпляра StringBuilder c ключем шифрования offset
        /// </summary>
        /// <param name="text">Текст предназначеный для шифрование</param>
        /// <param name="offset">Параметр смещение в шифре Цезаря</param>
        /// <returns></returns>
        public StringBuilder SimpleEncryption(StringBuilder text, int offset)
        {
            StringBuilder encryptedText = new StringBuilder(text.ToString());
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ') continue;
                encryptedText[i] = alphabet[(IndexAlphaBet(text[i]) + offset) % alphaLenght];
            }
            secrtLang = encryptedText;
            return secrtLang;
        }
        /// <summary>
        /// Метод расшифровывает текст, который передается параметром secretL
        /// </summary>
        /// <param name="secretL">Зашифрованный текст</param>
        /// <returns></returns>
        public string SimpleDecryption(StringBuilder secretL)
        {
            List<StringBuilder> decryptedText = new List<StringBuilder>();
            List<StringBuilder> decryptedTempText = new List<StringBuilder>();
            List<StringBuilder> encryptedText = GetWords(secretL);
            for (int offset = 0; offset <= alphaLenght; offset++)
            {
                foreach (StringBuilder word in encryptedText)
                {
                    StringBuilder decryptedWord = new StringBuilder(word.ToString());
                    for (int i = 0; i < word.Length; i++)
                    {
                        decryptedWord[i] = alphabet[(IndexAlphaBet(word[i]) - offset + alphaLenght) % alphaLenght];
                    }
                    //проверка биграмм                    
                    if (!checkBigrammsFreq(decryptedWord))
                    {
                        decryptedTempText.Clear();
                        break;
                    }
                    decryptedTempText.Add(decryptedWord);
                }
                //проверка частот букв
                if (decryptedTempText.Count == 0) continue;
                StringBuilder attach = new StringBuilder();
                foreach (StringBuilder str in decryptedTempText)
                    attach.Append(str + " ");
                if (checkLettersFreq(attach))
                    decryptedText.Add(attach.Append("\n----->>Ключ: " + offset + "\n"));
                decryptedTempText.Clear();
            }
            string result = " ";
            foreach (StringBuilder str in decryptedText)
                result += str.ToString() + "\n";
            return result;
        }
        /// <summary>
        /// Метод проверяет частотные характеристики текстового объекта text по биграммам.
        /// Возвращает TRUE, если большинство частот соответствующих биграмм в словаре bigrammsFreq достаточно велико
        /// </summary>
        /// <param name="text">Строка с текстом для проверки</param>
        /// <returns></returns>
        private bool checkBigrammsFreq(StringBuilder text)
        {
            string bi;
            int errors = 0;
            long freq = 0;
            for (int i = 1; i < text.Length; i++)
            {
                bi = text[i - 1].ToString() + text[i].ToString();
                freq = bigrammsFreq[bi];
                if (freq == 0) return false;
                if (freq <= 10 && i >= 3)
                    return false;
                else if (freq <= 100) errors++;
            }
            if (errors > 2 * text.Length / 3)
                return false;
            return true;
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
                if (text[i] == ' ') continue;
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
            double volume = (double)lettersFreq['!'];
            foreach (char a in tempDict.Keys)
            {
                percentT = tempDict[a] / count;
                percentAbsolute = lettersFreq[a] / volume;
                if (Math.Abs(percentAbsolute - percentT) > ksi)
                    errors++;
            }
            return (errors <= ERRORS) ? true : false;
        }

    }
}
