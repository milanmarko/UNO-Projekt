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
            int? uInput = null;
            do
            {
                uInput = Menu.PrintCardSelect(Deck.ToArray(), Name);
                if ((Action)uInput == Action.Draw)
                    return null;
            } while(!isSelectedCardPlayable(Deck[uInput ?? 0]) || (Game.Plusses > 0 && !isSelectedPlusPlayable(Deck[uInput ?? 0])));
            Card play = Deck[uInput ?? 0];
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
            int uInput = Menu.PrintColorChanger();
            return Game.__colors[uInput];
        }
        public override string ToString()
        {
            return $"{Name}, Lapjai száma: {Deck.Count}";
        }
    }
}
