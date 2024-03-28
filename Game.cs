using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    internal class Game
    {
        private int PlayerCount;
        private int AICount;
        private int StartCardCount;
        private static Random r = new Random();
        private List<Card> PlayerDeck;
        private List<Card> AIDeck;
        public static List<Card> GameDeck;
        public static List<Card>? onTable;
        public Game(int startCardCount)
        {
            StartCardCount = startCardCount;
            FillGameDeck();
        }

        private void FillGameDeck()
        {
            GameDeck = new List<Card>();
            ConsoleColor[] __colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green };
            for (int i = 0; i < 10; i++)
            {
                foreach (ConsoleColor color in __colors)
                {
                    for (int j = 0; j < 2; j++)
                        GameDeck.Add(new Card(color, i));
                }
            }
            foreach (Types type in Enum.GetValues(typeof(Types)))
            {
                if (type != Types.ColorChanger || type != Types.PlusFour)
                {
                    foreach (ConsoleColor color in __colors)
                    {
                        GameDeck.Add(new SpecialCard(color, type));
                    }
                }
                else
                {
                    GameDeck.Add(new SpecialCard(ConsoleColor.Black, type));
                }
            }
        }

        private void StartGame()
        {
            Console.WriteLine("UNO");
            do
            {
                Console.WriteLine("Kérem adja meg mennyi lappal szeretné kezdeni a játékot");
            }
            while (!int.TryParse(Console.ReadLine(), out StartCardCount));
            PlayerDeck = GetCards(PlayerDeck);
            AIDeck = GetCards(AIDeck);
            onTable.Add(GameDeck.First());
            GameDeck.RemoveAt(0);
            //Kene vmi ellenorzes hogy ne lehessen +4 az 1. lap
          
        }
        private static List<Card> GetCards(List<Card> a)
        {
            a = new List<Card>();
            GameDeck = GameDeck.OrderBy(_ => r.Next()).ToList();
            for (int i = 0; i < StartCardCount; i++)
            { 
                a.Add(GameDeck.First());
                GameDeck.RemoveAt(0);
            }
            return a;
        }
        public static Card DrawCard()
        {
            Card drawn = GameDeck.First();
            GameDeck.RemoveAt(0);
            return drawn;
        }

    }
}
