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
    internal class Ai:PlayerObj
    {
        static string[] AiNames = new string[] { "Koppány", "Töhötöm", "Kende", "Vajk", "Bulcsú", "Gellért", "Tordai" };
        static int ID = 0;
        static Random r = new Random();
        public static Card? TopCard { get { return Game.onTable.Last(); } }

        public Ai(List<Card> DeckGivenByGameMaster):base(DeckGivenByGameMaster, $"{AiNames[ID++]} (AI)")
        {
            if (ID > AiNames.Length - 1) ID = 0;
        }
        public override Card? PlayRound()
        {
            List<Card>possible = Deck.Where(x => x.ToString() == TopCard.ToString()|| x.Color_ == TopCard.Color_ || x.Color_ == ConsoleColor.Black).ToList();
            if (Game.Plusses > 0)
            {
                possible = Deck.Where(x => x.ToString() == "+2" || x.ToString() == "+4").ToList();
            }
            if (possible.Count > 0)
            {
                List<Card> numberedCards = possible.Where(x => x.Value != null).ToList(); // Ha lehet, sima kártyát játszon ki előszörs
                Card playing = possible[r.Next(possible.Count)];
                if (numberedCards.Count > 0)
                    playing = numberedCards[r.Next(numberedCards.Count)];
                if (Deck.Count == 2)
                {
                    Console.WriteLine("UNO");
                }
                Deck.Remove(playing);
                return playing;
            }
            Deck.Add(Game.DrawCard());
            return null;
        }
        public override ConsoleColor ColorChange()
        {
            Dictionary<ConsoleColor, int> Colors = new Dictionary<ConsoleColor, int>()
            { [ConsoleColor.Blue] = 0, [ConsoleColor.Red] = 0, [ConsoleColor.Yellow] = 0, [ConsoleColor.Green] = 0};
            Colors[ConsoleColor.Blue] = Deck.Where(x => x.Color_ == ConsoleColor.Blue).ToList().Count();
            Colors[ConsoleColor.Red] = Deck.Where(x => x.Color_ == ConsoleColor.Red).ToList().Count();
            Colors[ConsoleColor.Yellow] = Deck.Where(x => x.Color_ == ConsoleColor.Yellow).ToList().Count();
            Colors[ConsoleColor.Green] = Deck.Where(x => x.Color_ == ConsoleColor.Green).ToList().Count();
            return Colors.MaxBy(x => x.Value).Key;
        }
    }
}
