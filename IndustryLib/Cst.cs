
namespace IndustryLib {
    public static class EquipmentCst {

        public const int PipeBasicVolume = 100;
        public const int ValveBasicVolume = 100;

        public const int BalanceFactor = 2;
        public const double PressureContentFactor = 100.0;

        public enum Types {
            Tank,
            Pipe,
            Source,
            Valve,
            Pump

        }
    }
}
