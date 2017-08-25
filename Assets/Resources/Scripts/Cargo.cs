using UnityEngine;
using System.Collections.Generic;

public class Cargo
{
    public Cargo(string _Name, float _mass, float _space, ushort _maxCarryingAmount)
    {
        Name              = _Name;
        mass              = _mass;
        space             = _space;
        maxCarryingAmount = _maxCarryingAmount;
    }

    public string Name;
    public float mass;
    public float space;
    public ushort maxCarryingAmount;

    public static readonly Cargo coal = new Cargo("Coal", 5, 5, 5); 
    public static readonly Cargo wood = new Cargo("Wood", 5, 5, 5);
    //public static readonly Cargo
    //public static readonly Cargo
    //public static readonly Cargo

    public enum CargoID
    {
        coal,
        wood
    }

    public static Dictionary<CargoID, Cargo> GetCargo = new Dictionary<CargoID, Cargo>
    {
        {CargoID.coal, coal},
        {CargoID.wood, wood}
    };
}
