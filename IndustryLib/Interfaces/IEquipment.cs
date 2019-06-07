using System;

namespace IndustryLib
{

  public interface IEquipment
  {
    string Name { get; }
    bool Operational { get; set; }
    string TypeOfEquipment { get; }
  }
}
