
using System;
using IndustryLib;

namespace IndustryConsole {
    partial class Program {

        static void ShowFactory() {
            if (factory.EquipmentCount == 0) {
                Console.WriteLine("Factory is empty");
                return;
            }

            Console.WriteLine("Factory contains: ");
            int index = 0;
            foreach (Equipment equipment in factory.GetEquipment()) {
                index++;
                Console.WriteLine(index + ": " + equipment.TypeOfEquipment + " " + equipment.Mark + " : " + equipment.Name);
            }
        }


        static void ShowEquipment() {
            Equipment equipment = SelectEquipment();
            Console.WriteLine(equipment);
        }
    }
}