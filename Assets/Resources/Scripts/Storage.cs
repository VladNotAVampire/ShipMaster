using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour, IInteractable, IStorage
{
    public Dictionary<Cargo, ushort> content {get; private set;}
    
    [SerializeField] MainStorageWindow storageWindow;

    private void Start()
    {
        content = new Dictionary<Cargo, ushort>();
    }

    public virtual void Interact(GameObject player)
    {
        var playerCarrying = player.GetComponent<PlayerCarrying>();
        player.GetComponent<PlayerController>().SetBlock(false, false);
        storageWindow.storagePosition = transform.position;
        storageWindow.OpenStorage(this, playerCarrying);
    }

    public virtual bool CanTake(Cargo cargo, ushort amount)
    {
        return true;
    }

    public virtual ushort CalculateMaxAmountToPut(Cargo cargo)
    {
        return 9999;
    }
}