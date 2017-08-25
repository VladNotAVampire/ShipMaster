using UnityEngine;

public class Extractable : MonoBehaviour, IInteractable
{
    [SerializeField]Cargo.CargoID cargoID;
     
    Cargo cargo;

    void Start()
    {
        cargo = Cargo.GetCargo[cargoID];
    }

    public void Interact(GameObject player)
    {
        if (player.GetComponent<PlayerCarrying>().TakeCargo(cargo, 1))
            Destroy(gameObject);
    }
}

