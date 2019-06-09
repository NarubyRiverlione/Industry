using System;
using System.Collections.Generic;
using System.Text;

namespace IndustryLib {
    public class Pipe : Equipment {

        // pipe volume = basic volume x mark
        public Pipe(string name, int mark, int content = 0)
        : base(EquipmentCst.Types.Pipe, name, mark, EquipmentCst.PipeBasicVolume * mark, content) {

        }
    }
}