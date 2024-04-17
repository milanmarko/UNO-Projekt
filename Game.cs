using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    public enum Player
    {
        AI, Player
    }
    internal class Game
    {
        private int PlayerCount;
        private int AICount;
        private int StartCardCount;
        private string[] PlayerNames;
        private List<PlayerObj> Players = new List<PlayerObj>();
        private static Random r = new Random();
        private static List<Card> Available = new List<Card>();
        public static List<Card> GameDeck = new List<Card>();
        public static List<Card> onTable = new List<Card>();
        public static bool Blocked = false;
        public static bool Reversed = false;
        public static int Plusses = 0;
        public static ConsoleColor[] __colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green };
        private static ConsoleColor currentColor = ConsoleColor.White;
        public static ConsoleColor CurrentColor { get { if (TopCard.Color_ == ConsoleColor.Black) return currentColor; else return TopCard.Color_; } private set{ } }
        public static Card TopCard { get => onTable.Last(); }
        public Game(int startCardCount, int aicount, int playercount, string[] playerNames)
        {
            AICount = aicount;
            PlayerCount = playercount;
            PlayerNames = playerNames;
            Console.BackgroundColor = ConsoleColor.Gray;
            StartCardCount = startCardCount;
            //GameLoop();
        }

        private void FillGameDeck()
        {
            // Ezt még majd át kell nézni mert elég goofy
            for (int i = 1; i < 10; i++)
            {
                foreach (ConsoleColor color in __colors)
                {
                    for (int j = 0; j < 2; j++)
                        GameDeck.Add(new Card(color, i));
                }
            }
            foreach (ConsoleColor color in __colors)
                GameDeck.Add(new Card(color, 0));
            foreach (Types type in Enum.GetValues(typeof(Types)))
            {
                if ((int)type != 1&& (int)type != 4)
                {
                    foreach (ConsoleColor color in __colors)
                    {
                        GameDeck.Add(new SpecialCard(color, type));
                    }
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GameDeck.Add(new SpecialCard(ConsoleColor.Black, type));
                    }
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
            for (int i = 0; i < PlayerCount; i++)
                Players.Add(new PlayerObj(GetCards(), PlayerNames[i]));
            for (int i = 0; i < AICount; i++)
                Players.Add(new Ai(GetCards()));
            Shuffler.Shuffle(Available);
            onTable.Add(Available.First());
            Available.RemoveAt(0);

        }
        private List<Card> GetCards()
        {
            List<Card> a = new List<Card>();
            for (int i = 0; i < StartCardCount; i++)
            {
                a.Add(Available.First());
                Available.RemoveAt(0);
            }
            return a;
        }
        public static Card DrawCard()
        {

            if (Available.Count() == 0)
            {
                Card Last = TopCard;
                onTable.RemoveAt(onTable.Count - 1);
                Shuffler.Shuffle(onTable);
                Available.AddRange(onTable);
                onTable.Clear();
                onTable.Add(Last);
            }
            Card drawn = Available.First();
            Available.RemoveAt(0);
            return drawn;
        }
        private bool isPlayableOnHand(List<Card> a)
        {
            List<Card> b = possibleMoves().Intersect(a).ToList();
            if (b.Count() == 0) return false;
            return true;
        }
        private List<Card> possibleMoves()
        {
            return Available.Where(x => x.ToString() == TopCard.ToString() || x.Color_ == TopCard.Color_ || x.Color_ == ConsoleColor.Black).ToList();
        }
        private int? WhoWon()
        {
            for (int i = 0; i < Players.Count; i++ )
            {
                if (Players[i].Deck.Count == 0)
                    return i;
            }
            return null;

        }
        private void HandleSpecialCards(PlayerObj ply, Card _card)
        {
            if (_card.Value == null)
            {
                SpecialCard card = (SpecialCard)_card;
                if (card.Type == Types.Block)
                    Blocked = true;
                else if (card.Type == Types.Reverse)
                    Reversed = !Reversed;
                else if (card.Type == Types.PlusTwo || card.Type == Types.PlusFour)
                {
                    Plusses += (int)card.Type;
                }
                if (card.Type == Types.ColorChanger || card.Type == Types.PlusFour)
                {
                    currentColor = ply.ColorChange();
                }

            }
        }


        public string GameLoop()
        {
            FillGameDeck();
            StartGame();
            while (WhoWon() == null)
            {
                // Ide kéne majd rakni a különleges lapok akcióit
                for(int i = 0; i < Players.Count; i += Reversed ? -1 : 1)
                {
                    if (i < 0 ) i = 0;
                    PlayerObj ply = Players[i];
                    Console.Clear();
                    Console.WriteLine($"Következő játékos: {ply}");
                    if (Blocked)
                    {
                        Console.WriteLine($"Sajnos {ply.Name} kimarad egy körből.");
                        Blocked = false;
                        Console.ReadKey();

                        continue;
                    }
                    Card? playedCardOrNull = ply.PlayRound();
                    if (playedCardOrNull != null)
                    {
                        Console.WriteLine($"{ply.Name} a következő kártyát tette le: ");
                        Console.ForegroundColor = playedCardOrNull.Color_;
                        Console.Write($"{playedCardOrNull} \n");
                        Console.ForegroundColor = ConsoleColor.Black;
                        onTable.Add(playedCardOrNull);
                        HandleSpecialCards(ply, playedCardOrNull);

                    }
                    else
                    {
                        Console.WriteLine($"{ply.Name} felhúzott {(Plusses > 0 ? Plusses : 1)} db kártyát! ");
                        for (int j = 0; j < (Plusses > 0 ? Plusses - 1 : 1); j++)
                            ply.ReceiveCard(DrawCard());
                        Plusses = 0;
                    }
                    Console.ReadKey();
                    if (i == 0 && Reversed) i = Players.Count;
                    if (WhoWon() != null) break;
                }
                // Vége a játéknak
            }
            return Players[WhoWon() ?? 0].Name;
        }
    }
}
