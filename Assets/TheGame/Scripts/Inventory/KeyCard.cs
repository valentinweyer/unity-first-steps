using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : SaveableDestructable
{
    /// <summary>
    /// Das Inventarobjekt, das den mitgeführten Gegenständen hinzugefügt wird.
    /// </summary>
    public InventoryItem item;


    private void OnTriggerEnter(Collider other) 
    {
        SaveGameData.current.inventory.add(item);
        gameObject.SetActive (false);
    }
}
