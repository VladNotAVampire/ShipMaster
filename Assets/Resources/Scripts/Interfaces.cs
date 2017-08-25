using UnityEngine;
using System.Collections.Generic;

public interface IInteractable
{
    void Interact(GameObject player);
}

public interface IStorage
{
    Dictionary<Cargo, ushort> content {get;}

    Transform transform {get;}

    bool CanTake(Cargo cargo, ushort amount);

    ushort CalculateMaxAmountToPut(Cargo cargo);
}

public interface IMount
{
    float Speed {get; set;}

    void GetOn(GameObject player);

    void GetOff();
}

public interface IControlable
{
    KeyCode GoForward {get; set;}
    KeyCode GoBack {get; set;}
    KeyCode GoLeft {get; set;}
    KeyCode GoRight {get; set;}
    KeyCode Run {get; set;}
    KeyCode Jump {get; set;}
    
    bool IsGoingForward {get;}
    bool IsGoingBack {get;}
    bool IsGoingLeft {get;}
    bool IsGoingRight {get;}
    bool IsRuning {get;}
}
