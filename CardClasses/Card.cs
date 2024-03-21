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
        protected ConsoleColor Color_;
        private byte Value;

        public Card(ConsoleColor color)
        {
            Color_ = color;
        }
        public Card(ConsoleColor color, byte value)
        {
            Color_ = color;
            Value = value;
        }

        public virtual Card PlayCard()
        {
            return this;
        }
    }
}
