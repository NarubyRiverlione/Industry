using System;
namespace IndustryLib {
    public class Tank : IEquipment {

        public string Name { get; private set; }
        public bool Operational { get; set; }
        public string TypeOfEquipment { get; private set; }

        public int Volume { get; private set; }
        public int Content { get; private set; }
        public int Temperature { get; set; }

        public bool HeatingOn { get; set; }
        public bool FillingNow { get; set; }

        public Tank(string name, int volume, int content = 0, int temp = 20) {
            Name = name;
            Operational = true;
            TypeOfEquipment = "Tank";

            Volume = volume;

            if (content > volume) content = volume;

            Content = content;
            Temperature = temp;
            HeatingOn = false;
            FillingNow = false;
        }

        public override string ToString() {
            string output = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is " + (Operational ? "operational" : "not operational");

            output += Content != 0 ? " and a content of " + Content + " liters on a temperature of " + Temperature + " °C" : " and is empty";
            output += HeatingOn ? "and is currently heating up" : "";
            output += FillingNow ? "and is filling up" : "";

            return output;
        }

        // set new content
        public void Fill(int newContent) {
            if (newContent > Volume) newContent = Volume;
            Content = newContent;
        }
    }
}
