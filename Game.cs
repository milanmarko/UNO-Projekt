﻿using System;
using System.Collections.Generic;
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
        private static Random r = new Random();
        private List<Card> PlayerDeck;
        private List<Card> AIDeck;
        private static List<Card> Possible;
        private static List<Card> Available = new List<Card>();
        public static List<Card> GameDeck = new List<Card>();
        public static List<Card> onTable = new List<Card>();
        private bool blocked = false;
        public Game(int startCardCount)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            StartCardCount = startCardCount;
            GameLoop();
        }

        private void FillGameDeck()
        {
            // Ezt még majd át kell nézni mert elég goofy
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
        private bool isSelectedCardPlayable(Card card)
        {
            return (card.Color_ == onTable.Last().Color_ || card.ToString() == onTable.Last().ToString() || card.Color_ == ConsoleColor.Black);
        }
        private Card? PlayerChoice()
        {
            //Console.WriteLine($"DEBUG");
            //foreach (Card card in PlayerDeck)
            //{
            //    Console.Write(card);
            //}
            Console.ForegroundColor = onTable.Last().Color_;
            Console.Write($"\n{onTable.Last()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("-ra raksz\n");
            if (PlayerDeck.Where(x => isSelectedCardPlayable(x)).Count() > 0)
            {
                foreach (Card card in PlayerDeck)
                {
                    Console.ForegroundColor = card.Color_;
                    Console.Write($"{card}, ");
                }
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\nÍrd be annak a lapnak a sorszámát, amit le szeretnél rakni!");
                int uInput;
                do
                {
                    int.TryParse(Console.ReadLine(), out uInput);
                } while (uInput > 0 && uInput < PlayerDeck.Count + 1 && !isSelectedCardPlayable(PlayerDeck[uInput - 1]));
                Console.WriteLine(uInput);
                return PlayerDeck[uInput-1];
            }
            else
                return null;

        }

        private Card? Turn(Player player)
        {
            Card? playedCard = Ai.PlayRound(AIDeck);
            List<Card> deck = AIDeck;
            if (player == Player.Player)
            {
                playedCard = PlayerChoice();
                deck = PlayerDeck;
            }
            if (playedCard != null)
            {
                deck.Remove(playedCard);
                onTable.Add(playedCard);
                return playedCard;
            }
            return null;
        }


        private void GameLoop()
        {
            FillGameDeck();
            StartGame();
            while(PlayerDeck.Count > 0 && AIDeck.Count > 0)
            {
                // Ide kéne majd rakni a különleges lapok akcióit
                if (!blocked)
                Card? playedByPlayer = Turn(Player.Player);
                if (playedByPlayer != null)
                    Console.WriteLine($"A Következő kártyát tetted le: {playedByPlayer}");
                else
                {
                    PlayerDeck.Add(DrawCard());
                    Console.WriteLine("Felhúztál egy lapot.");
                }
                if (AIDeck.Count > 0) {
                    Card? playedByAi = Turn(Player.AI);
                    if (playedByAi != null)
                        Console.WriteLine($"Az AI a következő kártyát tette le: {playedByAi}");
                    else
                    {
                        AIDeck.Add(DrawCard());
                        Console.WriteLine("Az AI felhúzott egy lapot.");
                    }

                }
            }
            // Vége a játéknak
        }
    }
}
