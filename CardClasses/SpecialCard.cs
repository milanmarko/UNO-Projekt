using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Projekt.CardClasses
{
    public enum Types
    {
        PlusTwo, PlusFour, Reverse, Block
    }
    internal class SpecialCard : Card
    {
        private Types Type;
        public SpecialCard(Types type, ConsoleColor color)
        {

            Type = type;
            Color_ = color;
        }

        public override Card PlayCard()
        {

        }
    }
}
