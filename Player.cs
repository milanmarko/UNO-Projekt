using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    internal class PlayerObj
    {
        public List<Card> Deck { get; private set; }
        public string Name { get; private set; }
        public PlayerObj(List<Card> DeckGivenByGameMaster)
        {
            Deck = DeckGivenByGameMaster;
            
        }
        public PlayerObj(List<Card> DeckGivenByGameMaster, string name_ = "Debug"):this(DeckGivenByGameMaster)
        {
            Name = name_;
        }

        private bool isSelectedCardPlayable(Card card)
        {
            return (card.Color_ == Game.CurrentColor || card.ToString() == Game.TopCard.ToString() || card.Color_ == ConsoleColor.Black);
        }
        private bool isSelectedPlusPlayable(Card card)
        {
            return card.ToString() == "+2" || card.ToString() == "+4";
        }
        public virtual Card? PlayRound()
        {
            Console.ForegroundColor = Game.CurrentColor;
            Console.Write($"\n{Game.TopCard}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("-ra raksz\n");
            if (Game.Plusses > 0)
                Console.WriteLine($"Ha nem tudsz rakni, {Game.Plusses} db lapot húzol fel!");

            foreach (Card card in Deck)
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
                if (a == "0") return null;
                int.TryParse(a, out uInput);
            } while (uInput < 1 || uInput > Deck.Count || !isSelectedCardPlayable(Deck[uInput - 1]) || (Game.Plusses > 0 && !isSelectedPlusPlayable(Deck[uInput -1])));
            //Console.WriteLine(uInput);
            Card play = Deck[uInput - 1];
            Deck.Remove(play);
            return play;
        }

        public void ReceiveCard(Card card)
        {
            Deck.Add(card);
        }

        public virtual ConsoleColor ColorChange()
        {
            Console.WriteLine("Jelenlegi lapjaid:");
            foreach (Card card in Deck)
            {
                Console.ForegroundColor = card.Color_;
                Console.Write($"{card}, ");
            }
            foreach (ConsoleColor color in Game.__colors)
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
            } while (uInput < 1 || uInput > Game.__colors.Length);
            Console.WriteLine(uInput);
            return Game.__colors[uInput - 1];
        }
        public override string ToString()
        {
            return $"{Name}, Lapjai száma: {Deck.Count}";
        }
    }
}
