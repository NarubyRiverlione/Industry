using System;
using System.Collections.Generic;

namespace IndustryLib {

    public interface IEquipment {
        string Name { get; }
        bool Operational { get; }
        string TypeOfEquipment { get; }

        int Content { get; }
        void Fill(int content);

        void SetFailed();
        void SetRepaired();
    }
}
