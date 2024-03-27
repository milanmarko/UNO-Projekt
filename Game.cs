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
        private List<Card>[] PlayerDeck;
        private List<Card>[] AIDeck;
        private List<Card> GameDeck;
        private List<Card> onTable;

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
            for (int i = 0; i < PlayerCount; i++)
                PlayerDeck[i] = GetCards(PlayerDeck[i]);
            for (int i = 0; i < AICount; i++)
                AIDeck[i] = GetCards(AIDeck[i]);
            onTable.Add(GameDeck.First());
            GameDeck.RemoveAt(0);
        }
        private List<Card> GetCards(List<Card> a)
        {
            a = new List<Card>();
            this.GameDeck = this.GameDeck.OrderBy(_ => r.Next()).ToList();
            for (int i = 0; i < StartCardCount; i++)
            { 
                a.Add(GameDeck.First());
                GameDeck.RemoveAt(0);
            }
            return a;
        }


    }
}
