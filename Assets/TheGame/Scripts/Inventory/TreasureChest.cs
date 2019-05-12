using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : SaveableDestructable
{
    /// <summary>
    /// Schlüssel, der sich im Inventar befinden muss, um die Schatztruhe zu öffnen.
    /// </summary>
    public InventoryItem key;

    /// <summary>
    /// Objekt das sich in der Kiste befindet.
    /// </summary>
    public InventoryItem treasure;

    private bool keyWasPressed=false;

    private void OnTriggerStay(Collider collider) 
    {
        if (Input.GetAxisRaw("Fire1")!=0) //aktionstaste gedrückt
        {
            if(keyWasPressed)
            {
                return;
            }
            keyWasPressed = true;
            if(SaveGameData.current.inventory.contains(key))
            {
                SaveGameData.current.inventory.add(treasure);
                SaveGameData.current.inventory.remove(key);
                gameObject.SetActive(false);
            }
            else //wenn der Schlüssel sich nicht im Inventar befindet
            {
                Debug.Log("Schlüsselobjekt fehlt");
            }
        }
        else
        {
            keyWasPressed = false;
        }
    }
}
