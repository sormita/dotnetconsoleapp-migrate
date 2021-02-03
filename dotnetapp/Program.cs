using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using Utils;
using static System.Console;
using System.Collections.Generic;
using dotnetapp;
using System.Linq;

public static class Program 
{
    public static bool ReturnToMainMenu { get; set; }

    public static void Main(string[] args) 
    {
          var defaultMessage = "Hello from .NET Framework!";
          var bot = GetBot();
          var (message, withColor) = ParseArgs(args);
          var output = message == string.Empty ? $"    {defaultMessage}{bot}" : $"    {message}{bot}";

          if (withColor)
          {
              ConsoleUtils.PrintStringWithRandomColor(output);
          }
          else
          {
              WriteLine(output);
          }
          
          WriteLine("**Environment**");
          WriteLine($"Platform: .NET Framework");
          WriteLine($"OS: {RuntimeInformation.OSDescription}");
          WriteLine();

        ReturnToMainMenu = false;
        Console.ForegroundColor = ConsoleColor.Green;
        ShowMainMenu();
        Console.ResetColor();
    }

    private static (string, bool) ParseArgs(string[] args)
    {
      var buffer = new StringBuilder();
      var withColor = false;
      foreach(var s in args)
      {
        if (s == "--with-color")
        {
          withColor = true;
          continue;
        }
        buffer.Append(" ");
        buffer.Append(s);
      }

      return (buffer.ToString(), withColor);
    }

    private static string GetBot() 
    {
            
            return @"
      __________________
                        \
                        \
                            ....
                            ....'
                            ....
                          ..........
                      .............'..'..
                  ................'..'.....
                .......'..........'..'..'....
                ........'..........'..'..'.....
              .'....'..'..........'..'.......'.
              .'..................'...   ......
              .  ......'.........         .....
              .                           ......
              ..    .            ..        ......
            ....       .                 .......
            ......  .......          ............
              ................  ......................
              ........................'................
            ......................'..'......    .......
          .........................'..'.....       .......
      ........    ..'.............'..'....      ..........
    ..'..'...      ...............'.......      ..........
    ...'......     ...... ..........  ......         .......
  ...........   .......              ........        ......
  .......        '...'.'.              '.'.'.'         ....
  .......       .....'..               ..'.....
    ..       ..........               ..'........
            ............               ..............
          .............               '..............
          ...........'..              .'.'............
        ...............              .'.'.............
        .............'..               ..'..'...........
        ...............                 .'..............
        .........                        ..............
          .....

  ";
    }

    private static readonly Dictionary<int, string> MainMenuOptions = new Dictionary<int, string>()
        {
            { 1, "Go to Basic Exercises Menu" },
            { 2, "Go to Data Type Exercises Menu" },
            { 3, "Go to Conditional Statements Exercise Menu" },
            { 4, "Go to Cave Of Programming Exercise Menu" },
            { 5, "Go to Crypto Exercise Menu" },
            { 6, "Execute all Basic Exercises" },
            { 7, "Execute all Data Type Exercises" },
            { 8, "Execute all Conditional Statements Exercises" },
            { 9, "Excute all Cave Of Programming Exercises" },
            { 10, "Execute all Crypto Exercises" },
            { 11, "Execute All Exercises" },
            { 12, "Exit" }
        };

    private static void ShowMainMenu()
    {
        Console.WriteLine("***MAIN MENU***\n\nChoose an option from the menu to get started...\n");
        foreach (KeyValuePair<int, string> option in MainMenuOptions)
            Console.WriteLine($"{(option.Key < 10 ? " " : "")}{option.Key}.) {option.Value}");
        Console.WriteLine();
        NavigateUsersRequest();
    }

    private static void NavigateUsersRequest()
    {
        int request = (int)GetUsersRequest(0);
        Console.Clear();
        switch (request)
        {
            case 1:
                BasicExercises.ShowBasicExerciseMenu();
                break;
            case 2:
                DataTypesExercises.ShowDataTypeExerciseMenu();
                break;
            case 3:
                ConditionalStatementsExercises.ShowConditionalStatementsExerciseMenu();
                break;
            //case 4:
            //    ShowCaveOfProgrammingExerciseMenu();
            //    break;
            case 5:
                CryptoExercises.ShowCryptoExerciseMenu();
                break;
            case 6:
                ReturnToMainMenu = true;
                BasicExercises.ExecuteAllBasicExercises();
                break;
            case 7:
                ReturnToMainMenu = true;
                DataTypesExercises.ExecuteAllDataTypeExercises();
                break;
            case 8:
                ReturnToMainMenu = true;
                ConditionalStatementsExercises.ExecuteAllConditionalStatementsExercises();
                break;
            //case 9:
            //    ReturnToMainMenu = true;
            //    ExecuteAllCaveOfProgrammingExercises();
            //    break;
            case 10:
                ReturnToMainMenu = true;
                CryptoExercises.ExecuteAllCryptoExercises();
                break;
            case 11:
                ReturnToMainMenu = true;
                ExecuteAllExercises();
                break;
            case 12:
                Environment.Exit(0);
                break;
            default:
                Console.Clear();
                ReadKeyAndClear();
                Main(new string[] { });
                break;
        }
    }

    private static object GetUsersRequest(int attempts)
    {
        if (attempts > 0)
        {
            Console.WriteLine("\nYour input is invalid. Try again.\n");
            ReadKeyAndClear();
            ShowMainMenu();
        }
        int request = 0;
        if (Int32.TryParse(Console.ReadLine(), out request) && MainMenuOptions.Keys.Contains(request))
            return request;
        return GetUsersRequest(++attempts);
    }

    private static void ExecuteAllExercises()
    {
        BasicExercises.ExecuteAllBasicExercises();
        DataTypesExercises.ExecuteAllDataTypeExercises();
        ConditionalStatementsExercises.ExecuteAllConditionalStatementsExercises();
    }

    public static void ReadKeyAndClear()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public static void ExerciseNotComplete()
    {
        Console.WriteLine("This exercise has not been completed yet, but thanks for dropping by!");
        ReadKeyAndClear();
    }
}
