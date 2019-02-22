using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Elternklasse für Savebles, die ihren Sichtbarkeitszustand speichern und wiederherstellen.
/// </summary>
public class SaveableDestructable : Saveable
{
    public string ID = "";

    protected override void Start()
    {
        base.Start();
        if (ID == "")
            Debug.LogError("Das speicherbare Objekt " + gameObject.name + " hat keine ID bekommen");
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);

        if (!gameObject.activeSelf && !savegame.destroyedObjects.Contains(ID))
        {
            savegame.destroyedObjects.Add(ID);
        }
    }
    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);

        if (savegame.destroyedObjects.Contains(ID))
        {
            gameObject.SetActive(false);
        }
    }
}
