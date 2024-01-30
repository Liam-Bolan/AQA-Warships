//Skeleton Program for the examination
//this code should be used in conjunction with the Preliminary Material


//Version Number 1.0

using System;
using System.Data.Common;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

class Program
{
    public struct ShipType
    {
        public string Name;
        public int Size;
        
    }

    const string TrainingGame = "Training.txt";

    private static void GetRowColumn(ref int Row, ref int Column)
    {
        Console.WriteLine();   
             
        try
        {
            Console.Write("Please enter column: ");
            Column = Convert.ToInt32(Console.ReadLine());
            while(Column < 0 || Column > 9)
            {
                Console.WriteLine("incorrect coordinate entered! Enter again: ");
                Column = int.Parse(Console.ReadLine());
            } 
        }
        catch(Exception)
        {
            Console.Write("Error! enter a number: ");
            Column = int.Parse(Console.ReadLine());
        }
        try
        {
            Console.Write("Please enter Row: ");
            Row = Convert.ToInt32(Console.ReadLine());
            while (Row < 0 || Row > 9)
            {
                Console.Write("incorrect coordinate entered! Enter again: ");
                Row = int.Parse(Console.ReadLine());
            }
        }
        catch (Exception)
        {
            Console.Write("Error! enter a number: ");
            Row = int.Parse(Console.ReadLine());
        }

    }

    private static void MakePlayerMove(ref char[,] Board, ref ShipType[] Ships)
    {
        int Row = 0;
        int Column = 0;
        int hit = 0;
        int miss = 0;
        bool won = false;
        
        GetRowColumn(ref Row, ref Column);
        
        if (Board[Row, Column] == 'm' || Board[Row, Column] == 'h')
        {
            Console.Clear();
            Console.WriteLine("Sorry, you have already shot at the square (" + Column + "," + Row + "). Please try again.");
        }
        else if (Board[Row, Column] == '-')
        {
            Console.Clear();
            Console.WriteLine("Sorry, (" + Column + "," + Row + ") is a miss.");
            miss++;
            Board[Row, Column] = 'm';
                       
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Hit at (" + Column + "," + Row + ").");
            
            while (won = false)
            {
                Console.WriteLine($"Hits: {hit} Misses{miss}");
                if (won = true)
                {
                    hit++;
                }
            }
            for (int i = 0; i < 7-1; i++)
            {
                if (Board[Row, Column] == System.Convert.ToChar(Ships[i].Name[0]))
                {
                    Board[Row, Column] = System.Convert.ToChar(Ships[i].Name.ToLower()[0]);
                }
                
            }
            if(Board[Row, Column] == 'A')
            {
                Console.WriteLine("You hit an Aircraft carrier");
                Board[Row, Column] = 'a';
            }
            else if (Board[Row, Column] == 'B')
            {
                Console.WriteLine("You hit a Battleship");
                Board[Row, Column] = 'b';
            }
            else if (Board[Row, Column] == 'S')
            {
                Console.WriteLine("You hit a Submarine");
                Board[Row, Column] = 's';
            }
            else if (Board[Row, Column] == 'D')
            {
                Console.WriteLine("You hit a Destroyer");
                Board[Row, Column] = 'd';
            }
            else if (Board[Row, Column] == 'P')
            {
                Console.WriteLine("You hit a Patrol Boat");
                Board[Row, Column] = 'p';
            }
            else if (Board[Row, Column] == 'F')
            {
                Console.WriteLine("You hit a Frigate");
                Board[Row, Column] = 'f';
                           
            }
           
            
            


        }
        
    }
    

    private static void SetUpBoard(ref char[,] Board)
    {
        for (int Row = 0; Row < 10; Row++)
        {
            for (int Column = 0; Column < 10; Column++)
            {
                Board[Row, Column] = '-';
            }
        }
    }

    private static void LoadGame(string TrainingGame, ref char[,] Board)
    {
        string Line = "";
        StreamReader BoardFile = new StreamReader(TrainingGame);
        for (int Row = 0; Row < 10; Row++)
        {
            Line = BoardFile.ReadLine();
            for (int Column = 0; Column < 10; Column++)
            {
                Board[Row, Column] = Line[Column];
            }
        }
        BoardFile.Close();
    }

    private static void PlaceRandomShips(ref char[,] Board, ShipType[] Ships)
    {
        Random RandomNumber = new Random();
        bool Valid;
        char Orientation = ' ';
        int Row = 0;
        int Column = 0;
        int HorV = 0;
        foreach (var Ship in Ships)
        {
            Valid = false;
            while (Valid == false)
            {
                Row = RandomNumber.Next(0, 10);
                Column = RandomNumber.Next(0, 10);
                HorV = RandomNumber.Next(0, 2);
                if (HorV == 0)
                {
                    Orientation = 'v';
                }
                else
                {
                    Orientation = 'h';
                }
                Valid = ValidateBoatPosition(Board, Ship, Row, Column, Orientation);
            }
            Console.WriteLine("Computer placing the " + Ship.Name);
            PlaceShip(ref Board, Ship, Row, Column, Orientation);
        }
    }

    private static void PlaceShip(ref char[,] Board, ShipType Ship, int Row, int Column, char Orientation)
    {
        if (Orientation == 'v')
        {
            for (int Scan = 0; Scan < Ship.Size; Scan++)
            {
                Board[Row + Scan, Column] = Ship.Name[0];
            }
        }
        else if (Orientation == 'h')
        {
            for (int Scan = 0; Scan < Ship.Size; Scan++)
            {
                Board[Row, Column + Scan] = Ship.Name[0];
            }
        }
    }

    private static bool ValidateBoatPosition(char[,] Board, ShipType Ship, int Row, int Column, char Orientation)
    {
        if (Orientation == 'v' && Row + Ship.Size > 10)
        {
            return false;
        }
        else if (Orientation == 'h' && Column + Ship.Size > 10)
        {
            return false;
        }
        else
        {
            if (Orientation == 'v')
            {
                for (int Scan = 0; Scan < Ship.Size; Scan++)
                {
                    if (Board[Row + Scan, Column] != '-')
                    {
                        return false;
                    }
                }
            }
            else if (Orientation == 'h')
            {
                for (int Scan = 0; Scan < Ship.Size; Scan++)
                {
                    if (Board[Row, Column + Scan] != '-')
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private static bool CheckWin(char[,] Board)
    {
        for (int Row = 0; Row < 10; Row++)
        {
            for (int Column = 0; Column < 10; Column++)
            {
                if (Board[Row, Column] == 'A' || Board[Row, Column] == 'B' || Board[Row, Column] == 'S' || Board[Row, Column] == 'D' || Board[Row, Column] == 'P' || Board[Row, Column] == 'F')
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static void PrintBoard(char[,] Board)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.Write(" ");
        for (int Column = 0; Column < 10; Column++)
        {
            Console.Write(" " + Column + "  ");
        }
        Console.WriteLine();
        for (int Row = 0; Row < 10; Row++)
        {
            Console.Write(Row + " ");
            for (int Column = 0; Column < 10; Column++)
            {
                if (Board[Row, Column] == '-')
                {
                    Console.Write(" ");
                }
                else if (Board[Row, Column] == 'A' || Board[Row, Column] == 'B' || Board[Row, Column] == 'S' || Board[Row, Column] == 'D' || Board[Row, Column] == 'P' ||Board[Row, Column] == 'F')
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(Board[Row, Column]);
                }
                if (Column != 9)
                {
                    Console.Write(" | ");
                }
            }
            Console.WriteLine();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("");
        Console.WriteLine("1. Start new game");
        Console.WriteLine("2. Load training game");
        Console.WriteLine("3. Quit");
        Console.WriteLine();
    }

    private static int GetMainMenuChoice()
    {
        int Choice = 0;
        try
        {
            Console.Write("Please enter Choice: ");
            Choice = Convert.ToInt32(Console.ReadLine());
            while (Choice < 0 || Choice > 3)
            {
                Console.Write("Please enter choice again: ");
                Choice = int.Parse(Console.ReadLine());
            }
        }
        catch (Exception)
        {
            Console.Write("Error! enter a number: ");
            Choice = int.Parse(Console.ReadLine());
        }
        return Choice;
    }

    private static void PlayGame(ref char[,] Board, ref ShipType[] Ships)
    {
        bool GameWon = false;
        int maxGos = 30;
        while (GameWon == false)
        {
            PrintBoard(Board);
            MakePlayerMove(ref Board, ref Ships);
            GameWon = CheckWin(Board);
            if (GameWon == true)
            {
                Console.WriteLine("All ships sunk!");
                Console.WriteLine();
            }
            else
            {
                maxGos -= 1;
                Console.WriteLine($"You have {maxGos} moves left! ");
                if (maxGos == 0)
                {
                    Console.WriteLine("Oh no you have ran out of moves!");
                    return;
                }
            }
        }
    }

    private static void SetUpShips(ref ShipType[] Ships)
    {
        
        Ships[0].Name = "Aircraft Carrier";
        Ships[0].Size = 5;
        
        Ships[1].Name = "Battleship";
        Ships[1].Size = 4;
        
        Ships[2].Name = "Submarine";
        Ships[2].Size = 3;
        
        Ships[3].Name = "Destroyer";
        Ships[3].Size = 3;
        
        Ships[4].Name = "Patrol Boat";
        Ships[4].Size = 2;
        
        Ships[5].Name = "Frigate";
        Ships[5].Size = 2;
        
    }

    static void Main(string[] args)
    {
        ShipType[] Ships = new ShipType[6];
        char[,] Board = new char[10, 10];
        int MenuOption = 0;
        while (MenuOption != 3)
        {
            SetUpBoard(ref Board);
            SetUpShips(ref Ships);
            DisplayMenu();
            MenuOption = GetMainMenuChoice();
            if (MenuOption == 1)
            {
                PlaceRandomShips(ref Board, Ships);
                PlayGame(ref Board, ref Ships);
            }
            if (MenuOption == 2)
            {
                LoadGame(TrainingGame, ref Board);
                PlayGame(ref Board, ref Ships);
            }
        }
    }
}
