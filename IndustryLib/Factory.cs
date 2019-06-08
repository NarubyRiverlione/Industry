using System;
using System.Collections.Generic;

namespace IndustryLib {
    public class Factory {
        private List<Equipment> AllEquipment = new List<Equipment>();

        public int EquipmentCount => AllEquipment.Count;

        public List<Equipment> GetEquipment() => AllEquipment;

        public void AddEquipment(Equipment equipment) => AllEquipment.Add(equipment);
    }
}