using UNO_Projekt;
using UNO_Projekt.CardClasses;

//Menu.PrintMenu(new Card[] { new Card(ConsoleColor.DarkBlue, 2) , new Card(ConsoleColor.Red, 2) });
//Thread.Sleep(10000);

bool isSettingsRestart = false;
do
{
    Menu.GameStartMenu();
    bool isNewGame = false;
    do
    {
        Game g = new Game(Menu.StartCardCount, Menu.AiCount, Menu.PlayerCount, Menu.PlayerNames);
        Console.WriteLine($"Ezt a játékot most {g.GameLoop()} nyerte.");
        Console.WriteLine("Nyomj egy 0-t, hogy ugyan ezekkel a beállításokkal új játékot kezdj, 1-t, hogy újra a beállítások fülre kerülj!");
        ConsoleKey key = Console.ReadKey(true).Key;
        isNewGame = key == ConsoleKey.D0 ? true : false;
        isSettingsRestart = key == ConsoleKey.D1 ? true : false;
    }while( isNewGame );
} while( isSettingsRestart );
