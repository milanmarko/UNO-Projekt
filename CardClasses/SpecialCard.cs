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
        public static string[] SpecialCardTypes = new string[] { "+2", "+4", "Fordító", "Blokkoló", "Színkérő" };
        public Types Type;
        public SpecialCard(ConsoleColor color, Types type):base(color, null)
        {

            Type = type;
            Color_ = color;
        }

        public override Card? PlayCard()
        {
            return null;
        }
        public override string ToString()
        {
            return SpecialCardTypes[(int)Type];
        }
    }
}
