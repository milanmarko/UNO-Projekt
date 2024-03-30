using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Projekt.CardClasses
{
    internal class Card
    {
        public ConsoleColor Color_;
        public int? Value;

        public Card(ConsoleColor color, int? value)
        {
            Value = value;
            Color_ = color;
        }

        public Card PlayCard()
        {
            return this;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
