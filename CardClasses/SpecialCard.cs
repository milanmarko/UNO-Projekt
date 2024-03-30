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
          Reverse = 1, PlusTwo = 2, ColorChanger = 2, Block = 3,  PlusFour = 4,
    }
    internal class SpecialCard : Card
    {
        public static string[] SpecialCardTypes = new string[] { "Fordító", "+2", "Színkérő",  "Blokkoló", "+4"};
        public Types Type;
        public SpecialCard(ConsoleColor color, Types type):base(color, null)
        {

            Type = type;
            Color_ = color;
        }

        public override string ToString()
        {
            return SpecialCardTypes[(int)Type];
        }
    }
}
