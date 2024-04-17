using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        Draw = -1
    }
    internal static class Menu
    {
        public static int StartCardCount;
        public static int PlayerCount;
        public static int AiCount;
        public static string[] PlayerNames;

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
        
        public static void GameStartMenu()
        {
            Console.Clear();
            Console.WriteLine("Hány lappal kezdjenek a játékosok?");
            StartCardCount = int.Parse(Console.ReadLine());
            Console.WriteLine("Játékosok száma: ");
            PlayerCount = int.Parse(Console.ReadLine());
            PlayerNames = new string[PlayerCount];
            for (int i = 0; i <PlayerCount; i++)
            {
                Console.WriteLine($"{i+1}. Játékos neve: ");
                PlayerNames[i] = Console.ReadLine();
            }
            do
            {
                Console.WriteLine("AI-ok száma (maximum 8 lehet): ");
                AiCount = int.Parse(Console.ReadLine());
            } while (AiCount <= 0 || AiCount > 8);
        }

        public static int PrintColorChanger()
        {
            int currentSelected = 0;
            ConsoleColor[] colors = new ConsoleColor[] {ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.Yellow,  ConsoleColor.Green};
            string[] colorNames = new string[] { "Piros", "Kék", "Sárga", "Zöld" };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Milyen színt kérsz?");
                currentSelected = currentSelected > colors.Length - 1 ? 0 : currentSelected;
                currentSelected = currentSelected < 0 ? colors.Length - 1 : currentSelected;
                for (int i = 0; i < colorNames.Length; i++)
                {
                    Console.BackgroundColor = i == currentSelected ? ConsoleColor.Black : ConsoleColor.Gray;
                    Console.ForegroundColor = colors[i];
                    Console.WriteLine(colorNames[i]);
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
        public static int PrintCardSelect(Card[] cardArray, string plyname)
        {
            int currentSelected = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"{plyname} köre jön.");
                PrintPlayerHeadline();
                Console.WriteLine("Írj egy 0-t ha fel szeretnél húzni");
                currentSelected = currentSelected > cardArray.Length -1 ? 0 : currentSelected;
                currentSelected = currentSelected < 0 ? cardArray.Length - 1 : currentSelected;

                for (int i = 0; i  < cardArray.Length; i++)
                {
                    Console.BackgroundColor = i == currentSelected ? ConsoleColor.Black : ConsoleColor.Gray;
                    Console.ForegroundColor = cardArray[i].Color_ == ConsoleColor.Black ? ConsoleColor.White : cardArray[i].Color_;
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
