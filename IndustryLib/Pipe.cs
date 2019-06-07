using System;
using System.Collections.Generic;
using System.Text;

namespace IndustryLib {
    public class Pipe : Equipment {
     
        public Pipe(string name, int volume):base(name,volume) {
            TypeOfEquipment = "Pipe";
        }
    }
}