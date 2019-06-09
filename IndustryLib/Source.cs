using System;
namespace IndustryLib {
    public class Source : Equipment {

        public Source(string name)
            : base(EquipmentCst.Types.Source, name, 1, int.MaxValue, int.MaxValue) {
        }
    }
}
