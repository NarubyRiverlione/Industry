using System;
using IndustryLib;

namespace IndustryConsole {

    partial class Program {
        static void AddEquipment() {
            Console.Write("Add witch equipment (t)ank / (p)ipe... : ");
            string WantedEquipment = Console.ReadLine();

            switch (WantedEquipment.ToLower()) {
                case "t":
                    AddTank(); break;
                case "p":
                    AddPipe(); break;
                default:
                    Console.WriteLine("Unknown equipment"); break;
            }
        }

        static void AddTank() {
            string Name = AskName();

            int Volume = 0;
            do {

                Console.Write("Volume: ");
                string VolumeInput = Console.ReadLine();
                int.TryParse(VolumeInput, out Volume);
            } while (Volume == 0);

            Tank newTank = new Tank(Name, Volume);
            factory.AddEquipment(newTank);
        }

        static void AddPipe() {
            string Name = AskName();
            int Mark = AskMark();
            Pipe pipe = new Pipe(Name, Mark);
            factory.AddEquipment(pipe);
        }

        static string AskName() {
            Console.Write("Name: ");
            string Name = Console.ReadLine();
            return Name;
        }

        static int AskMark() {
            int Mark = 0;
            do {
                Console.Write("Mark: ");
                string MarkInput = Console.ReadLine();
                int.TryParse(MarkInput, out Mark);
            } while (Mark == 0);
            return Mark;
        }
    }
}
