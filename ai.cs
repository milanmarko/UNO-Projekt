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
    internal class ai
    {
        Random r = new Random();
        public List<Card> deck = new List<Card>();
        public static Card? TopCard { get { return Game.onTable.Last(); } }
        public ai(List<Card>deck)
        {
            this.deck = deck;
        }
        public Card? PlayRound()
        {
            List<Card>possible = deck.Where(x => x.Value == TopCard.Value).ToList();
            possible.AddRange(deck.Where(x => x.Color_ == TopCard.Color_|| x.Color_ == ConsoleColor.Black).ToList());
            if (possible != null)
            {
                Card playing = possible[r.Next(possible.Count)];
                if (deck.Count == 2)
                {
                    Console.WriteLine("UNO");
                }
                if (playing.Color_ == ConsoleColor.Black) 
                {
                    ColorMax();
                }
                deck.Remove(playing);
                return playing;
            }
            deck.Add(Game.DrawCard());
            return null;
        }
        private ConsoleColor ColorMax()
        {
            Dictionary<ConsoleColor, int> Colors = new Dictionary<ConsoleColor, int>()
            { [ConsoleColor.Blue] = 0, [ConsoleColor.Red] = 0, [ConsoleColor.Yellow] = 0, [ConsoleColor.Green] = 0};
            Colors[ConsoleColor.Blue] = deck.Where(x => x.Color_ == ConsoleColor.Blue).ToList().Count();
            Colors[ConsoleColor.Red] = deck.Where(x => x.Color_ == ConsoleColor.Red).ToList().Count();
            Colors[ConsoleColor.Yellow] = deck.Where(x => x.Color_ == ConsoleColor.Yellow).ToList().Count();
            Colors[ConsoleColor.Green] = deck.Where(x => x.Color_ == ConsoleColor.Green).ToList().Count();
            return Colors.MaxBy(x => x.Value).Key;
        }

    }
}
