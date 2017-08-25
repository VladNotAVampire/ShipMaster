using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCarrying : MonoBehaviour, IStorage
{
    public Dictionary<Cargo, ushort> content {get; private set;}

    private void Start()
    {
        content = new Dictionary<Cargo, ushort>();
    }

    private void OnGUI()
    {
        string cargoInfo = string.Empty;

        foreach(var cargo in content)
        {
            cargoInfo += cargo.Key.Name + " " + cargo.Value + "/" + cargo.Key.maxCarryingAmount + "\n";
        }

        GUI.Box(new Rect(Screen.width - 200, Screen.height - 20 * content.Count, 200, 20 * content.Count), cargoInfo);
    }

    public bool TakeCargo(Cargo cargo, ushort amount)
    {
        if (content.ContainsKey(cargo))
        {
            if (content[cargo] + amount <= cargo.maxCarryingAmount) 
            {
                content[cargo] += amount;
                return true;
            }
        }
        else
        {
            if (amount <= cargo.maxCarryingAmount)
            {
                content.Add(cargo, amount);
                return true;
            }
        }

        return false;
    }

    public bool CanTake(Cargo cargo, ushort amount)
    {
        if (content.ContainsKey(cargo))
        {
            return (content[cargo] + amount <= cargo.maxCarryingAmount);
        }

        return (amount <= cargo.maxCarryingAmount);
    }

    public ushort CalculateMaxAmountToPut(Cargo cargo)
    {
        if (content.ContainsKey(cargo))
        {
            return (ushort)(cargo.maxCarryingAmount - content[cargo]);
        }

        return cargo.maxCarryingAmount;
    }
}
