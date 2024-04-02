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
        private static Random r = new Random();
        private List<Card> PlayerDeck;
        private List<Card> AIDeck;
        private static List<Card> Available = new List<Card>();
        public static List<Card> GameDeck = new List<Card>();
        public static List<Card> onTable = new List<Card>();
        private bool blocked = false;
        private int Plusses = 0;
        private ConsoleColor[] __colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green };
        private static ConsoleColor currentColor = ConsoleColor.White;
        public static ConsoleColor CurrentColor { get { if (onTable.Last().Color_ == ConsoleColor.Black) return currentColor; else return onTable.Last().Color_; } private set{ } }

        public Game(int startCardCount)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            StartCardCount = startCardCount;
            GameLoop();
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
                    for (global::System.Int32 i = 0; i < 4; i++)
                    {
                        GameDeck.Add(new SpecialCard(ConsoleColor.Black, type));
                    }
                }
            }
            foreach (var item in GameDeck)
            {
                //Debug
                //Console.ForegroundColor = item.Color_;
                //Console.Write(item);
                Available.Add(item);
            }
        }

        private void StartGame()
        {
            Shuffler.Shuffle(Available);
            PlayerDeck = GetCards(PlayerDeck);
            AIDeck = GetCards(AIDeck);
            //List<Card> lapok = new List<Card>();
            //Available[0] = new SpecialCard(ConsoleColor.Black, Types.PlusFour);
            //do
            //{
                onTable.Add(Available.First());
                //lapok.Add(Available.First());
                Available.RemoveAt(0);
            //}
            //while (Available.First().ToString() == "+4" || Available.First().ToString() == "Színkérő");
            //Available.AddRange(lapok);
        }
        private List<Card> GetCards(List<Card> a)
        {
            a = new List<Card>();
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
                Card Last = onTable.Last();
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
            return Available.Where(x => x.ToString() == onTable.Last().ToString() || x.Color_ == onTable.Last().Color_ || x.Color_ == ConsoleColor.Black).ToList();
        }
        private bool isSelectedCardPlayable(Card card)
        {
            return (card.Color_ == CurrentColor|| card.ToString() == onTable.Last().ToString() || card.Color_ == ConsoleColor.Black);
        }
        private bool isSelectedPlusPlayable(Card card)
        {
            return (card.Color_ == CurrentColor&&  card.ToString() == "+2" || card.Color_ == ConsoleColor.Black && card.ToString() == "+4");
        }
        private Card? PlayerPLusChoice()
        {
            //Console.WriteLine($"DEBUG");
            //foreach (Card card in PlayerDeck)
            //{
            //    Console.Write(card);
            //}
            Console.ForegroundColor = CurrentColor;
            Console.Write($"\n{onTable.Last()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("-ra raksz\n");
            if (PlayerDeck.Where(x => isSelectedPlusPlayable(x)).Count() > 0)
            {
                foreach (Card card in PlayerDeck.Where(x => (x.Color_ == CurrentColor && x.ToString() == "+2") ||( x.Color_ == ConsoleColor.Black && x.ToString() == "+4")).ToList())
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
                } while (uInput < 1 || uInput > PlayerDeck.Count || !isSelectedCardPlayable(PlayerDeck[uInput - 1]));
                Console.WriteLine(uInput);
                return (PlayerDeck.Where(x => (x.Color_ == CurrentColor && x.ToString() == "+2") || (x.Color_ == ConsoleColor.Black && x.ToString() == "+4")).ToList())[uInput-1];
            }
            else
                return null;

        }
        private bool WhoWon()
        { 
            if(AIDeck.Count() == 0)
            { return false; }
            return true;
        }
        private Card? PlayerChoice()
        {
            //Console.WriteLine($"DEBUG");
            //foreach (Card card in PlayerDeck)
            //{
            //    Console.Write(card);
            //}
            Console.ForegroundColor = CurrentColor;
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
                Console.WriteLine("\nÍrd be annak a lapnak a sorszámát, amit le szeretnél rakni! Írj 0-t ha húzni szeretnél");
                int uInput = -1;
                do
                {
                    string a = Console.ReadLine();
                    if (a == "0")
                    {
                        return null;
                    }
                    int.TryParse(a, out uInput);
                } while (uInput < 1 || uInput > PlayerDeck.Count || !isSelectedCardPlayable(PlayerDeck[uInput - 1]));
                Console.WriteLine(uInput);
                return PlayerDeck[uInput - 1];
            }
            else
                return null;

        }

        private Card? Turn(Player player, bool plusses = false)
        {
            Card? playedCard = Ai.PlayRound(AIDeck);
            List<Card> deck = AIDeck;
            if (player == Player.Player && plusses == false)
            {
                playedCard = PlayerChoice();
                deck = PlayerDeck;
            }
            else if(player == Player.Player && plusses == true)  
            {
                playedCard = PlayerPLusChoice();
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
        private ConsoleColor colorchange() 
        {
            Console.WriteLine("Jelenlegi lapjaid:");
            foreach (Card card in PlayerDeck)
            {
                Console.ForegroundColor = card.Color_;
                Console.Write($"{card}, ");
            }
            foreach (ConsoleColor color in __colors)
            {
                Console.ForegroundColor = color;
                Console.Write($"\n {string.Join(",", color)}");
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nÍrd be annak a színnek a sorszámát, amit le szeretnél rakni!");
            int uInput;
            do
            {
                int.TryParse(Console.ReadLine(), out uInput);
            } while (uInput < 1 || uInput > __colors.Length) ;
            Console.WriteLine(uInput);
            return __colors[uInput - 1];
        }


        private string GameLoop()
        {
            FillGameDeck();
            StartGame();
            string whoWon = null;
            while(PlayerDeck.Count > 0 && AIDeck.Count > 0)
            {
                // Ide kéne majd rakni a különleges lapok akcióit
                if (!blocked && Plusses == 0)
                {
                    Card? playedByPlayer = Turn(Player.Player);
                    if (playedByPlayer != null)
                    {
                        Console.Write($"A Következő kártyát tetted le: ");
                        Console.ForegroundColor = playedByPlayer.Color_;
                        Console.Write($"{playedByPlayer} \n");
                        Console.ForegroundColor = ConsoleColor.Black;
                        if (playedByPlayer.ToString() == "Fordító" || playedByPlayer.ToString() == "Blokkoló")
                            blocked = true;
                        else if (playedByPlayer.ToString() == "+2" || playedByPlayer.ToString() == "+4")
                        {
                            blocked = true;
                            Plusses += (int)((SpecialCard)playedByPlayer).Type;
                        }
                        if (playedByPlayer.ToString() == "+4" || playedByPlayer.ToString() == "Színkérő")
                        {
                            currentColor = colorchange();
                        }
                    }
                    else
                    {
                        PlayerDeck.Add(DrawCard());
                        Console.WriteLine("Felhúztál egy lapot.");
                    }
                }
                else if (Plusses > 0 && blocked == true)
                {
                    if (PlayerDeck.Where(x => x.Value == null).ToList().Where(y => ((SpecialCard)y).Type == Types.PlusTwo || ((SpecialCard)y).Type == Types.PlusFour).ToList().Count() > 0)
                    {
                        Turn(Player.Player, true);
                        Plusses += (int)((SpecialCard)onTable.Last()).Value;
                    }
                    else
                    {
                        blocked = false;
                        for (int i = 0; i < Plusses; i++)
                        {
                            PlayerDeck.Add(DrawCard());
                        }
                        Console.WriteLine($"Felhúztál {Plusses} lapot és kimaradtál egy körböl");
                        Plusses = 0;
                    }
                }
                else
                {
                    blocked = false;
                    Console.WriteLine("Kimaradtál a körből");
                }
                foreach (var item in AIDeck)
                {
                    Console.Write(item + ",");
                }
                if (!blocked && Plusses == 0)
                {
                    if (AIDeck.Count > 0)
                    {
                        Card? playedByAi = Turn(Player.AI);
                        if (playedByAi != null)
                        {
                            Console.WriteLine($"Az AI a következő kártyát tette le: {playedByAi}");
                            if (playedByAi.ToString() == "Fordító" || playedByAi.ToString() == "Blokkoló")
                                blocked = true;
                            else if (playedByAi.ToString() == "+2" || playedByAi.ToString() == "+4")
                            {
                                blocked = true;
                                Plusses += (int)((SpecialCard)playedByAi).Type;
                            }
                            if (playedByAi.ToString() == "Színkérő" || playedByAi.ToString() == "+4")
                            {
                                currentColor = Ai.ColorMax(AIDeck);
                            }
                        }
                        else
                        {
                            AIDeck.Add(DrawCard());
                            Console.WriteLine("Az AI felhúzott egy lapot.");
                        }

                    }
                }
                else if (Plusses > 0 && blocked == true)
                {
                    if (AIDeck.Where(x => x.Value == null).ToList().Where(y => ((SpecialCard)y).Type == Types.PlusTwo || ((SpecialCard)y).Type == Types.PlusFour).ToList().Count() > 0)
                    {
                        Ai.PlayOnPlus(AIDeck);
                    }
                    else
                    {
                        blocked = false;
                        for (global::System.Int32 i = 0; i < Plusses; i++)
                        {
                            AIDeck.Add(DrawCard());
                        }
                        Console.WriteLine($"AI felhúzott {Plusses} lapot és kimaradt egy körből");
                        Plusses = 0;
                    }
                }
                else
                {
                    blocked = false;
                    Console.WriteLine("AI kimaradt a körből");
                }
            }
            // Vége a játéknak
            return WhoWon()?  "Gratulálunk nyertél" : "Az AI nyert";
        }
    }
}
