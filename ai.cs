using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    internal class ai
    {

        public List<Card> deck;
        public Card topCard;
        public ai(List<Card>deck, Card topCard)
        {
            this.deck = deck;
            this.topCard = topCard;
        }
        public static Card? PlayTurn()
        {
            return null; 
        }
    }
}
