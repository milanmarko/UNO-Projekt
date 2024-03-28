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
        private static List<Card> Possible;
        private static List<Card> Available;
        public static List<Card> GameDeck;
        public static List<Card>? onTable;
        public Game(int startCardCount)
        {
            StartCardCount = startCardCount;
            GameLoop();
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
            foreach (var item in GameDeck)
            {
                Available.Add(item);
            }
        }

        private void StartGame()
        {
            Shuffler.Shuffle(Available);
            PlayerDeck = GetCards(PlayerDeck);
            AIDeck = GetCards(AIDeck);
            onTable.Add(Available.First());
            Available.RemoveAt(0);
            //Kene vmi ellenorzes hogy ne lehessen +4 az 1. lap
          
        }
        private List<Card> GetCards(List<Card> a)
        {
            a = new List<Card>(); // Initialize a new list
            for (int i = 0; i < StartCardCount; i++)
            {
                a.Add(Available.First());
                Available.RemoveAt(0);
            }
            return a;
        }
        public static Card DrawCard()
        {
            Card drawn = Available.First();
            Available.RemoveAt(0);
            return drawn;
        }
        private bool isPlayableOnHand(List<Card> a)
        {
            if (possibleMoves().Intersect(a).ToList().Count() == 0) return false;
            return true;
        }
        private List<Card> possibleMoves()
        {
            return Available.Where(x => x.Value == onTable.Last().Value || x.Color_ == onTable.Last().Color_ || x.Color_ == ConsoleColor.Black).ToList();
        }
        private Card? PlayerChoice()
        {
            string cards = "";
            int i = 1;
            string a = "";
            foreach (var card in PlayerDeck)
            {
                cards += card.Value + card.Color_+ i+ ", ";
            }
            cards.Remove(cards.Length - 2);
            Console.WriteLine(cards);
            if (isPlayableOnHand(PlayerDeck))
            {
                do
                {
                    a = Console.ReadLine();
                }
                while (int.TryParse(a, out i) && i < PlayerDeck.Count() && i >= 0 && possibleMoves().Contains(PlayerDeck[i]));
                if (i != 0)
                {
                    return PlayerDeck[i];
                }
            }
            return null;
        }
        private void PlayerTurn()
        { 
            Card playedCard = PlayerChoice();
        }
        private coid aiturn();
        private void GameLoop()
        {
            FillGameDeck();
            StartGame();
            PlayerTurn();
            aiturn()
        }
    }
}
