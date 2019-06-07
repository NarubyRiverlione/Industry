using System;
using System.Collections.Generic;

namespace IndustryLib {
    public class Tank : Equipment {
        public int Temperature { get; set; }

        public bool HeatingOn { get; set; }
        public bool FillingNow { get; set; }


        public Tank(string name, int volume, int temp = 20) : base(name, volume) {
            TypeOfEquipment = "Tank";
            Temperature = temp;
            HeatingOn = false;
            FillingNow = false;
        }

        override public void SetFailed() {
            Operational = false;
            Fill(0);
            HeatingOn = false;
            FillingNow = false;
        }

        override public void SetRepaired() {
            Operational = true;
        }

        public override string ToString() {
            string output = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is " + (Operational ? "operational" : "not operational");

            output += Content != 0 ? " and a content of " + Content + " liters on a temperature of " + Temperature + " °C" : " and is empty";
            output += HeatingOn ? " and is currently heating up" : "";
            output += FillingNow ?  " and is filling up" : "";

            return output;
        }
    }
 }
