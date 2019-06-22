
namespace IndustryLib {
    public class Source : Equipment {

        public Source(string name)
            : base(EquipmentCst.Types.Source, name, 1,100000, 100000) {
        }

        public override void CalcPressure(Equipment connectedTo=null) {
            // Pressure = content / factor        
            Pressure = (double)Content / EquipmentCst.PressureContentFactor;
        }

        // never change content of a Source
        public override void Fill(int content) {
            
        }
    }
}
