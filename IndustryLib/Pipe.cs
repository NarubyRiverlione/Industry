using System;
using System.Collections.Generic;
using System.Text;

namespace IndustryLib {
    public class Pipe : Equipment {

        // pipe volume = basic volume x mark
        public Pipe(string name, int mark = 1, int content = 0)
        : base(EquipmentCst.Types.Pipe, name, mark, EquipmentCst.PipeBasicVolume * mark, content) {

        }

        public override void CalcPressure(Equipment connectedTo) {
            if (connectedTo == null)
                Pressure = 0;
            else
                // connectTo has greater pressure, that it into the Pipe
                Pressure = connectedTo.Pressure > Pressure ? connectedTo.Pressure : Pressure;
        }
    }
}
