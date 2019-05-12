using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Speichert eine Liste der mitgeführten Gegenstönde im Inventar und übernimmt das Laden und Speichern.
/// </summary>
public class Inventory
{
    /// <summary>
    /// Signatur der Event-Listener, die aufgerufen werden sollen, wenn sich etwas am Inventar verändert.
    /// </summary>
    /// <param name="item">Das veränderte Item</param>
    public delegate void ItemChangEvent(InventoryItem item);

    /// <summary>
    /// Wird aufgerufen, wenn ein Objekt ins Inventar aufgenommen wurde.
    /// </summary>
    public static event ItemChangEvent onItemAdded;

    /// <summary>
    /// Wird aufgerufen, wenn ein Objekt aus dem Inventar entfernt wurde.
    /// </summary>
    public static event ItemChangEvent onItemRemoved;

    private List<InventoryItem> items = new List<InventoryItem> ();

    /// <summary>
    /// Fügt ein Inventarobjekt in die Liste der mitgeführten Gegenstände ein.
    /// Doppelte Einträge sind möglich!
    /// </summary>
    /// <param name="item">Objekt das ins Inventar aufgenommen werden soll.</param>
    public void add(InventoryItem item)
    {
        Debug.Log("Inventar erhält " + item);
        items.Add (item);
        if(onItemAdded!=null)
        {
            onItemAdded (item);
        }
    }

    /// <summary>
    /// Prüft, ob sich ein Objekt derzeit im Inventar befindet.
    /// </summary>
    /// <param name="item">Gesuchtes Objekt</param>
    /// <returns>True, wenn das Objekt derziet mitgeführt wird.</returns>
    public bool contains(InventoryItem item)
    {
        return items.Contains(item);
    }

    /// <summary>
    /// Entfernt das Objekt aus der Liste der mitgeführten Tnventarobjekte
    /// </summary>
    /// <param name="item">Zu entfernendes Inventarobjekt.</param>
    public void remove(InventoryItem item)
    {
        Debug.Log ("Inventar verliert " + item);
        items.Remove(item);
        if(onItemAdded!=null)
        {
            onItemRemoved (item);
        }
    }
}
