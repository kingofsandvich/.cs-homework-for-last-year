using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace solution
{
    class Program
    {
        
        static bool q_fl = false;
        static bool s_fl = false;
        static bool c_fl = false;
        static bool p_fl = false;
        static bool l_fl = false;

        static void Main(string[] args)
        {
            List<string> arg = new List<string>();
            string s;
            string path = "";

            int longest_word_len = 0;
            int longest_arb_word_len = 0;
            int longest_headword_len = 0;
            int longest_acronim_len = 0;
            int longest_real_word_len = 0;

            int word_count = 0;
            int arb_word_count = 0;
            int headword_count = 0;
            int acronim_count = 0;
            int real_word_count = 0;
            int count_strings;

            List<char> trim = new List<char>();

            List<string> longest_word = new List<string>();
            List<string> longest_arb_word = new List<string>();
            List<string> longest_real_word = new List<string>();
            List<string> longest_headword = new List<string>();
            List<string> longest_acronim = new List<string>();

            List<string> mod_longest_word = new List<string>();
            List<string> mod_longest_arb_word = new List<string>();
            List<string> mod_longest_real_word = new List<string>();
            List<string> mod_longest_headword = new List<string>();
            List<string> mod_longest_acronim = new List<string>();

            int len; // length of word
            #region Разбор консольной команды
            foreach (string i in Console.ReadLine().Split(new char[0])) arg.Add(i);
            if (arg.Count > 0)
            {
                foreach (string i in arg)
                {
                    s = Regex.Replace(i, @"\s+", "");
                    switch (s)
                    {
                        case ("-q"):
                            q_fl = true;
                            break;
                        case ("-s"):
                            s_fl = true;
                            break;
                        case ("-c"):
                            c_fl = true;
                            break;
                        case ("-p"):
                            p_fl = true;
                            break;
                        case ("-l"):
                            l_fl = true;
                            break;
                        case (""):
                            break;
                        default:
                            if (s[0] == '-')
                            {
                                Console.WriteLine(s + " INVALID FLAG");
                                Console.ReadLine();
                                return;
                            }

                            if (path == "")
                            {
                                path = s;
                            }
                            else
                            {
                                Console.WriteLine("TOO MANY FILES");
                                Console.ReadLine();
                                return;
                            }
                            break;
                    }
                }

            }

            if ((s_fl | c_fl) & q_fl)
            {
                Console.WriteLine("CONFLICTIN FLAGS");
                Console.ReadLine();
                return;
            }
            #endregion
            #region Ввод текста
            StreamReader sr;
            string text = "";

            if (path != "") // читаем файл
            {
                try
                {
                    if (File.Exists(path))
                    {
                        using (Stream stream = File.Open(path, FileMode.Open))
                        {
                            sr = new StreamReader(stream);
                            text = sr.ReadToEnd();
                        }
                        //text = System.IO.File.ReadAllText(path);
                    }
                    else throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine(path + " CANNOT OPEN");
                    Console.ReadLine();
                    return;
                }
            }
            else // читаем поток ввода
            {
                Console.WriteLine("Type your text below");
                Console.WriteLine("Press Enter additional time to stop typing");
                do
                {
                    text += Console.ReadLine() + '\n';
                } while (Console.ReadKey().Key != ConsoleKey.Enter);
            }

            string text_modified = text;
            int text_len = text.Length;
            #endregion
            // Преобразования с текстом
            #region Удаление пробелов
            if (s_fl)
            {
                if (text.Length > 0)
                {
                    if (!Char.IsWhiteSpace(text[0])) trim.Add(text[0]);

                    for (int i = 1; i < text.Length; i++)
                    {
                        if (Char.IsWhiteSpace(text[i]))
                        {
                            if (!Char.IsWhiteSpace(text[i-1]))
                            {
                                trim.Add(text[i]);
                            }
                        }
                        else trim.Add(text[i]);
                    }
                }
                text_modified = "";
                foreach (char item in trim)
                {
                    text_modified += item.ToString();
                }

            }
            #endregion
            #region Статистика входного текста
            count_strings = text.Split('\n').Length;

            foreach (string word in text_modified.Split(new char[0]))
            {
                len = word.Length;
                word_count++;
                if (len >= longest_word_len)
                {
                    if (len != longest_word_len)
                    {
                        longest_word_len = len;
                        longest_word.Clear();
                    }
                    longest_word.Add(word);
                }
                
                if (Logic.isAcronim(word))
                {
                    acronim_count++;
                    if (len >= longest_acronim_len)
                    {
                        if (len != longest_acronim_len)
                        {
                            longest_acronim_len = len;
                            longest_acronim.Clear();
                        }
                        longest_acronim.Add(word);
                    }
                }

                if (Logic.isArbitraryWord(word))
                {
                    arb_word_count++;
                    if (len >= longest_arb_word_len)
                    {
                        if (len != longest_arb_word_len)
                        {
                            longest_arb_word_len = len;
                            longest_arb_word.Clear();
                        }
                        longest_arb_word.Add(word);
                    }
                }

                if (Logic.isHeadword(word))
                {
                    headword_count++;
                    if (len >= longest_headword_len)
                    {
                        if (len != longest_headword_len)
                        {
                            longest_headword_len = len;
                            longest_headword.Clear();
                        }
                        longest_headword.Add(word);
                    }
                }

                if (Logic.isRealWord(word))
                {
                    real_word_count++;
                    if (len >= longest_real_word_len)
                    {
                        if (len != longest_real_word_len)
                        {
                            longest_real_word_len = len;
                            longest_real_word.Clear();
                        }
                        longest_real_word.Add(word);
                    }
                }
            }
            #endregion
            #region Получение обаботанного текста
            text = text_modified;
            text_modified = "";
            
            int a = 0;
            if (c_fl) // оставляем только настоящие слова
            {
                if (text.Length > 0)
                {

                    for (int i = 0; i < text.Length; i++)
                        if (!Char.IsWhiteSpace(text[i]))
                        {
                            a = i;
                            break;
                        }

                    if (a != text.Length - 1)
                        for (int i = a + 1; i < text.Length; i++)
                        {
                            if ( ( (Char.IsWhiteSpace(text[i - 1]) || ('\n' == text[i - 1])) && !Char.IsWhiteSpace(text[i])) || 
                                (i == text.Length - 1) )
                            {
                                if (Logic.isRealWord(Regex.Replace(text.Substring(a, i - a), @"\s+", "")))
                                    text_modified += text.Substring(a, i - a);
                                a = i;
                            }
                        }
                    else text_modified += text.Substring(a, 1);
                }
            }
            #endregion
            #region Запись обаботанного текста
            
            if (!q_fl)
            {
                using (StreamWriter sw = new StreamWriter("proceeded.txt"))
                {
                    sw.Write(text_modified);
                    Console.WriteLine("result is written in \"proceeded.txt\"");
                }
            }
            #endregion

            int text_modified_len = text_modified.Length;

            #region Вывод статистики
            if (p_fl)
            {
                Console.WriteLine("Info about input");
                Console.WriteLine($"Words: {word_count}");
                Console.WriteLine($"Arbitrary words: {arb_word_count}");
                Console.WriteLine($"Headwords: {headword_count}");
                Console.WriteLine($"Acronims: {acronim_count}");
                Console.WriteLine($"Real words: {real_word_count}");
                Console.WriteLine();
                Console.WriteLine($"Strings: {count_strings}");
                Console.WriteLine();
                Logic.PrintStats("Слово", longest_word_len, longest_word);
                Logic.PrintStats("Произвольное слово", longest_arb_word_len, longest_arb_word);
                Logic.PrintStats("Настоящее слово", longest_real_word_len, longest_real_word);
                Logic.PrintStats("Акроним", longest_acronim_len, longest_acronim);
                Logic.PrintStats("Заглавное слово", longest_headword_len, longest_headword);
                
            }
            Console.WriteLine();
            #endregion

            Console.ReadLine();
        }
    }
}
