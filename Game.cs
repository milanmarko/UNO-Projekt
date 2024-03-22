using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    internal class Game
    {
        private int PlayerCount;
        private int StartCardCount;
        private List<Card> PlayerDeck;
        private List<Card> AIDeck;
        private List<Card> GameDeck;

        public Game(int startCardCount)
        {
            StartCardCount = startCardCount;
            FillGameDeck();
        }

        private void FillGameDeck()
        {
            GameDeck = new List<Card>();
            ConsoleColor[] __colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green };
            for(int i = 0; i < 10; i++)
            {
                foreach(ConsoleColor color in __colors)
                {
                    for (int j = 0; i < 2; i++)
                        GameDeck.Add(new Card(color, i));
                }
            }
            foreach(Types type in Enum.GetValues(typeof(Types)))
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

        }

    }
}
