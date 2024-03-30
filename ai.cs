using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    internal class Ai
    {
        static Random r = new Random();
        public static Card? TopCard { get { return Game.onTable.Last(); } }
        public static Card? PlayRound(List<Card> deck)
        {
            List<Card>possible = deck.Where(x => x.Value == TopCard.Value|| x.Color_ == TopCard.Color_ || x.Color_ == ConsoleColor.Black).ToList();
            if (possible.Count > 0)
            {
                List<Card> numberedCards = possible.Where(x => x.Value != null).ToList(); // Ha lehet, sima kártyát játszon ki előszörs
                Card playing = possible[r.Next(possible.Count)];
                if (numberedCards.Count > 0)
                    playing = numberedCards[r.Next(numberedCards.Count)];
                if (deck.Count == 2)
                {
                    Console.WriteLine("UNO");
                }
                deck.Remove(playing);
                return playing;
            }
            deck.Add(Game.DrawCard());
            return null;
        }
        public ConsoleColor ColorMax(List<Card> deck)
        {
            Dictionary<ConsoleColor, int> Colors = new Dictionary<ConsoleColor, int>()
            { [ConsoleColor.Blue] = 0, [ConsoleColor.Red] = 0, [ConsoleColor.Yellow] = 0, [ConsoleColor.Green] = 0};
            Colors[ConsoleColor.Blue] = deck.Where(x => x.Color_ == ConsoleColor.Blue).ToList().Count();
            Colors[ConsoleColor.Red] = deck.Where(x => x.Color_ == ConsoleColor.Red).ToList().Count();
            Colors[ConsoleColor.Yellow] = deck.Where(x => x.Color_ == ConsoleColor.Yellow).ToList().Count();
            Colors[ConsoleColor.Green] = deck.Where(x => x.Color_ == ConsoleColor.Green).ToList().Count();
            return Colors.MaxBy(x => x.Value).Key;
        }
        public static Card PlayOnPlus(List<Card> deck)
        {
            List<Card> possible = deck.Where(x => x.Value == null).ToList().Where(y => ((SpecialCard)y).Type == Types.PlusTwo || ((SpecialCard)y).Type == Types.PlusFour).ToList();
            Card playing = possible[r.Next(possible.Count)];
            if (deck.Count == 2)
            {
                Console.WriteLine("UNO");
            }
            deck.Remove(playing);
            return playing;
        }
    }
}
