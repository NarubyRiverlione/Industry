
namespace IndustryLib {
    public class Tank : Equipment {
        public int Temperature { get; set; }

        public bool HeatingOn { get; set; }

        public Tank(string name, int volume, int content = 0, int temp = 20)
        : base(EquipmentCst.Types.Tank, name, 1, volume, content) {
            Temperature = temp;
            HeatingOn = false;
        }

        override public void SetFailed() {
            base.SetFailed();
            HeatingOn = false;
        }


        public override string ToString() {
            string output = "Tank " + Name + " has volume of " + Volume.ToString() + " liter and is " + (Operational ? "operational" : "not operational");

            output += Content != 0 ? " and a content of " + Content + " liters on a temperature of " + Temperature + " C" : " and is empty";
            output += HeatingOn ? " and is currently heating up" : "";

            return output;
        }

        public override void CalcPressure(Equipment connectedTo=null) {
            // Pressure = content / factor        
                Pressure = (double)Content / EquipmentCst.PressureContentFactor;            
            }
        }
    }

