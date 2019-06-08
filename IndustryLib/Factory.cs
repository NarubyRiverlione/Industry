using System;
using System.Collections.Generic;
using System.Linq;

namespace IndustryLib {
    public class Factory {
        private readonly List<Equipment> AllEquipment = new List<Equipment>();

        public int EquipmentCount => AllEquipment.Count;

        public List<Equipment> GetEquipment() => AllEquipment;

        public void AddEquipment(Equipment equipment) => AllEquipment.Add(equipment);

        public Equipment GetEquipmentByName(string name) {
            Equipment found = AllEquipment.FirstOrDefault(eq => eq.Name == name);
            return found;
        }

        public void BalanceContents() {
            foreach (Equipment eq in AllEquipment) {
                eq.BalanceConnections();
            }
        }
    }
}