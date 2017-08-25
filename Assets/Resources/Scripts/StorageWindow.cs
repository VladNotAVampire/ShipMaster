using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StorageWindow : MonoBehaviour 
{
    public Dictionary<Cargo, ushort> content;
    public PlayerCarrying player;
    public StorageWindow  otherWindow;
    public Dropdown       storagesList;

    public List<IStorage> storages;


    [SerializeField] Text          spaceDisplay;
    [SerializeField] GameObject    cargoPanel;

    [SerializeField] protected Transform  contentPanel;


    public static MainStorageWindow mainWindow;

    protected Dictionary<Cargo, CargoPanel>  cargoDisplays = new Dictionary<Cargo, CargoPanel>();


    public IStorage storage {get; private set;}

    public virtual void OpenStorage(IStorage _storage, PlayerCarrying _player)
    {
        gameObject.SetActive(true);    

        storage = _storage;
        player  = _player;
        content = storage.content; 

        foreach(var cargo in content)
        {
            AddCargo(cargo);
        }
    }

    public virtual void CloseStorage()
    {
        foreach(var panel in contentPanel.GetComponentsInChildren<CargoPanel>())
        {
            Destroy(panel.gameObject);
        }

        cargoDisplays.Clear();
        
        gameObject.SetActive(false);
    }

    public virtual bool Take(Cargo cargo, ushort amount)
    {
        if (storage.CanTake(cargo, amount))
        {
            if (content.ContainsKey(cargo))     content[cargo] += amount;
            else 
            {
                content.Add(cargo, amount);
                AddCargo(cargo, amount);
            }

            cargoDisplays[cargo].amountDisplay.text = content[cargo].ToString();
            cargoDisplays[cargo].Refresh();

            return true;
        }

        return false;
    }

    void AddCargo(KeyValuePair<Cargo, ushort> cargo)
    {
        AddCargo(cargo.Key, cargo.Value);
    }
    
    protected void AddCargo(Cargo cargo, ushort amount)
    {
        GameObject newPanel = (GameObject) Instantiate(cargoPanel, contentPanel);
        CargoPanel newCargoPanel = newPanel.GetComponent<CargoPanel>();
        newCargoPanel.SetCargo(this, cargo);
        cargoDisplays.Add(cargo, newCargoPanel);
    }

    public void RemoveCargo(CargoPanel panel)
    {
        cargoDisplays.Remove(panel.cargo);
        content.Remove(panel.cargo);
        Destroy(panel.gameObject);
    }

    public void SelectStorage()
    {
        if (otherWindow.storagesList.value == storagesList.value)
        {
            otherWindow.storagesList.value = (storagesList.value == 0) ?  1 : 0;
            otherWindow.storagesList.RefreshShownValue();
            otherWindow.SelectStorage();
        }

        ReOpen();
    }

    public virtual void ReOpen()
    {
        CloseStorage();
        OpenStorage(storages[storagesList.value], player);
    }

    public void PutAll()
    {
        var contentKeys = new List<Cargo>(content.Keys);

        foreach(var cargo in contentKeys)
        {
            if (!cargoDisplays[cargo].Put(content[cargo]))
            {
                cargoDisplays[cargo].Put(otherWindow.storage.CalculateMaxAmountToPut(cargo));
            }
        }
    }
}
