using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MainStorageWindow : StorageWindow
{

    [SerializeField] float findStoragesDistance;

    public Vector3 storagePosition;

    void Start()
    {
        mainWindow  = this;
    }

    public override void OpenStorage(IStorage _storage, PlayerCarrying _player)//С ОТКРЫТИЕМ !!!
    {
        base.OpenStorage(_storage, _player);

        otherWindow.OpenStorage(player, _player);

        var nearStorages = FindObjectsOfType<Storage>().Where(s => Vector3.Distance(s.transform.position, storagePosition) < findStoragesDistance)
                                                       .ToList();

        storagesList.AddOptions(new List<string>{player.name});
        storages = new List<IStorage> { player };

        if (nearStorages.Count > 0)
        {
            storagesList.AddOptions(nearStorages.Select(s => s.name).ToList());
            storages.AddRange(nearStorages.Select(s => s as IStorage).ToList());
        }
        otherWindow.storages = storages;
        otherWindow.storagesList.options = storagesList.options;
        //storagesList.transform.GetChild(0).GetComponent<Text>().text = nearStorages.First(s => s.transform.position == storagePosition).name;
        storagesList.value = storages.IndexOf(storage);
    }

    public override void CloseStorage()
    {
        base.CloseStorage();

        otherWindow.CloseStorage();
        storagesList.options.Clear();
        storages.Clear();

        if (player != null) player.GetComponent<PlayerController>().SetBlock(true, true); //while this is the only UI
    }

    public override void ReOpen()
    {
        base.CloseStorage();
        base.OpenStorage(storages[storagesList.value], player);
    }
}