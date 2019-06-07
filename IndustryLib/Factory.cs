using System;
using System.Collections.Generic;

namespace IndustryLib {
    public class Factory {
        private List<IEquipment> AllEquipment = new List<IEquipment>();

        public int EquipmentCount => AllEquipment.Count;

        public List<IEquipment> GetEquipment() => AllEquipment;

        public void AddEquipment(IEquipment equipment) => AllEquipment.Add(equipment);
    }
}