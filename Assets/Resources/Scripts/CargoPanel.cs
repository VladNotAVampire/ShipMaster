using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CargoPanel : MonoBehaviour 
{
    [SerializeField] Text Name;
    [SerializeField] InputField selectedAmountDisplay;   
    [SerializeField] Slider     slider;

    public Text amountDisplay;
    public Cargo cargo;

    StorageWindow storage;

    public void SetCargo(StorageWindow _storage, Cargo _cargo)
    {
        storage   = _storage;
        cargo     = _cargo;
        Name.text = cargo.Name;
        amountDisplay.text = storage.content[cargo].ToString();
        slider.maxValue = storage.content[cargo];
    }

    public void Put()
    {
        ushort takenAmount;
        if (ushort.TryParse(selectedAmountDisplay.text, out takenAmount))
        {
            Put(takenAmount);
        }
    }

    public bool Put(ushort amount)
    { 
        if (storage.otherWindow.Take(cargo, amount))
        {
            storage.content[cargo] -= amount;

            if (storage.content[cargo] == 0)    
            {
                 storage.RemoveCargo(this);
                 return true;
            }

            amountDisplay.text = storage.content[cargo].ToString();
            Refresh();
            return true;
        }   
        
        return false;    
    }

    public void SelectAll()
    {
        selectedAmountDisplay.text = storage.content[cargo].ToString();
    }

    public void Refresh()
    {
        int number; 
        if (int.TryParse(selectedAmountDisplay.text, out number))
        { 
            if (number > storage.content[cargo])     selectedAmountDisplay.text = storage.content[cargo].ToString();
            if (number <= 0)         selectedAmountDisplay.text = "1"; 

            slider.maxValue = storage.content[cargo];
            slider.value    = number;
        }
    }

    public void SliderSet()
    {
        selectedAmountDisplay.text = slider.value.ToString();
    }

    public void Plus(int plus)
    {
        selectedAmountDisplay.text = (int.Parse(selectedAmountDisplay.text) + plus).ToString();
        Refresh();
    }
}
