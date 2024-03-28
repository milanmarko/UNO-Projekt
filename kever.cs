using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Projekt
{
    static class Shuffler
    { 
        static Random r = new Random();

        public static void Shuffle<T>(List<T> deck)
        {
            for (int n = deck.Count - 1; n > 0; n--)
            {
                int k = r.Next(n + 1);
                T temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }
        }
    }
}
