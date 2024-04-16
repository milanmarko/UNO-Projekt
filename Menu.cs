using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO_Projekt.CardClasses;

namespace UNO_Projekt
{
    enum Action
    {
        Up,
        Down,
        Click,
        Draw
    }
    internal static class Menu
    {
        private static Action GetDirection()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
                return Action.Up;
            else if (key == ConsoleKey.DownArrow)
                return Action.Down;
            else if (key == ConsoleKey.D0)
                return Action.Draw;
            return Action.Click;
        }

        private static void PrintPlayerHeadline()
        {
            Console.ForegroundColor = Game.CurrentColor;
            Console.Write($"\n{Game.TopCard}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("-ra raksz\n");
            if (Game.Plusses > 0)
                Console.WriteLine($"Ha nem tudsz rakni, {Game.Plusses} db lapot húzol fel!");
        }
        public static int PrintCardSelect(Card[] cardArray)
        {
            int currentSelected = 0;
            while (true)
            {
                Console.Clear();
                PrintPlayerHeadline();
                Console.WriteLine("Írj egy 0-t ha fel szeretnél húzni");
                currentSelected = currentSelected > cardArray.Length -1 ? 0 : currentSelected;
                currentSelected = currentSelected < 0 ? cardArray.Length - 1 : currentSelected;

                for (int i = 0; i  < cardArray.Length; i++)
                {
                    Console.BackgroundColor = i == currentSelected ? ConsoleColor.Black : ConsoleColor.Gray;
                    Console.ForegroundColor = cardArray[i].Color_;
                    Console.WriteLine(cardArray[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                }



                switch (GetDirection())
                {
                    case Action.Up:
                        currentSelected--;
                        break;
                    case Action.Down:
                        currentSelected++; 
                        break;
                    case Action.Click:
                        return currentSelected;
                    case Action.Draw:
                        return (int)Action.Draw;
                }
            }
        }
    }
}
