using System;
using IndustryLib;

namespace IndustryConsole {

    partial class Program {
        static void UseEquipment() {
            Equipment equipment = SelectEquipment();
        }


        static Equipment SelectEquipment() {
            ShowFactory();
            Console.WriteLine();

            int index = -1;
            do {
                Console.Write("Select equipment number: ");
                string IndexInput = Console.ReadLine();

                int.TryParse(IndexInput, out index);
            }
            while (index > factory.EquipmentCount || index < 1);
            index--; // factory shows first equipment with number 1

            Equipment equipment = factory.GetEquipment()[index];
            return equipment;

        }
    }
}