
namespace IndustryLib {
    public class Valve : Equipment {
        public bool Open { get; private set; }

        public Valve(string name, int mark=1,int content=0, bool open=true) :
            base(EquipmentCst.Types.Valve,name, mark, EquipmentCst.ValveBasicVolume * mark, content) {
            Open = open;
            if (!open) SetClosed();
        }

        public void SetOpen() {
            // open = normal Volume
            Open = true;
            Volume = EquipmentCst.ValveBasicVolume * Mark; 
        }

        public void SetClosed() {
            // closed = 0 volume
            Open = false;
            Volume = 0;
            Pressure = 0;
            Fill(0);
        }

        public override void Fill(int content) {
            if (Open) {
                // open = fill as normal equipment
                base.Fill(content);

            }
            else // closed = set empty
                base.Fill(0);
        }

        public override void CalcPressure(Equipment connectedTo = null) {
            // closed = Pressure 0 == don't give Pressure to connected
            if (connectedTo == null || !Open)
                Pressure = 0;
            else
                // connectTo has greater pressure, that it into the Valve
                Pressure = connectedTo.Pressure > Pressure ? connectedTo.Pressure : Pressure;
        }
    }
}
