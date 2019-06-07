using System;
using System.Collections.Generic;
using IndustryLib;

namespace IndustryConsole
{
  partial class Program
  {
    static Factory factory = new Factory();

    static void Main(string[] args)
    {
      bool Continue = true;
      do
      {

        Console.Write("Command: ");
        string Cmd = Console.ReadLine();

        if (Cmd.ToLower() == "q" || Cmd.ToLower() == "quit") Continue = false;
        ExecuteCmd(Cmd);
      } while (Continue);

    }

    static void ExecuteCmd(string Cmd)
    {
      switch (Cmd.ToLower())
      {
        case "h":
        case "help":
          ShowHelp(); break;

        case "a":
        case "add":
          AddEquipment(); break;

        case "s":
        case "show":
          ShowFactory(); break;

        case "u":
        case "use":
          UseEquipment(); break;

        case "d":
        case "detail":
          ShowEquipment(); break;

        default:
          Console.WriteLine("Unknown command"); break;

      }
    }

    static void ShowHelp()
    {
      Console.WriteLine("Valid commands:");
      Console.WriteLine("q / quit : end program");
      Console.WriteLine("s / show : show factory");
      Console.WriteLine("a / add : add equipment");
      Console.WriteLine("u / use : use equipment");
      Console.WriteLine("d / detail : status of equipment");
    }



  }
}