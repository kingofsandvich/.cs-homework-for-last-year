using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solution
{
    public class Logic
    {
        public static void PrintStats(string s, int longest_word_len, List<String> longest_word)
        {
            if ((longest_word_len != 0) & (longest_word.Count != 0))
            {
                Console.Write($"{s} {longest_word_len}: ");
                longest_word.Sort();
                foreach (string word in longest_word)
                {
                    Console.Write(word + "; ");
                }
                Console.WriteLine();
            }
        }
        public static bool isRealWord(string word)
        {
            if (word.Length > 0)
            {
                for (int i = 1; i < word.Length; i++)
                    if ((
                        !(word[i] > 'A' && word[i] < 'Z') && 
                        !(word[i] > 'a' && word[i] < 'z')))
                        return false;
            }
            return true;
        }

        public static bool isHeadword(string word)
        {
            if (word.Length > 1)
            {
                if ('A' < word[0] & word[0] < 'Z')
                {
                    for (int i = 1; i < word.Length; i++)
                    {
                        if (word[i] < 'a' | word[i] > 'z') return false;
                    }
                    return true;
                }
                else return false;
            }
            return true;
        }

        public static bool isAcronim(string word)
        {
            if (word.Length > 0)
            {
                for (int i = 1; i < word.Length; i++)
                {
                    if (!(word[i] > 'A' && word[i] < 'Z')) return false;
                }
            }
            return true;
        }

        public static bool isArbitraryWord(string word)
        {
            if (word.Length > 0)
            {
                for (int i = 1; i < word.Length; i++)
                {
                    if (!(
                        (word[i] > 'a' && word[i] < 'z') ||
                        (word[i] > 'A' && word[i] < 'Z') || 
                        (word[i] > '0' && word[i] < '9')))
                        return false;
                }
            }
            return true;
        }

    }
}
