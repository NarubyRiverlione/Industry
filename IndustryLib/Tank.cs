using System;
using System.Collections.Generic;

namespace IndustryLib {
    public class Tank : Equipment {
        public int Temperature { get; set; }

        public bool HeatingOn { get; set; }
        public bool FillingNow { get; set; }


        public Tank(string name, int volume, int content = 0, int temp = 20)
        : base(EquipmentCst.Types.Tank, name, 1, volume, content) {
            Temperature = temp;
            HeatingOn = false;
            FillingNow = false;
        }

        override public void SetFailed() {
            base.SetFailed();
            HeatingOn = false;
            FillingNow = false;
        }


        public override string ToString() {
            string output = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is " + (Operational ? "operational" : "not operational");

            output += Content != 0 ? " and a content of " + Content + " liters on a temperature of " + Temperature + " C" : " and is empty";
            output += HeatingOn ? " and is currently heating up" : "";
            output += FillingNow ? " and is filling up" : "";

            return output;
        }
    }
}
