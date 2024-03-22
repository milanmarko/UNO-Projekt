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
        PlusTwo = 0, PlusFour = 1, Reverse = 2, Block = 3, ColorChanger = 4,
    }
    internal class SpecialCard : Card
    {
        private Types Type;
        public SpecialCard(ConsoleColor color, Types type):base(color)
        {

            Type = type;
            Color_ = color;
        }

        public override Card PlayCard()
        {

        }
    }
}
