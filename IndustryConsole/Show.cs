
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
            foreach (IEquipment equipment in factory.GetEquipment()) {
                index++;
                Console.WriteLine(index + ": " + equipment.TypeOfEquipment + " " + equipment.Name);
            }
        }


        static void ShowEquipment() {
            IEquipment equipment = SelectEquipment();
            Console.WriteLine(equipment);
        }
    }
}