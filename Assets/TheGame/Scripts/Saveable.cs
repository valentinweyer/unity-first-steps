using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Elternklasse für alle Verhalten, die  Speichern und Laden impöementieren wollen.
/// </summary>
public class Saveable : MonoBehaviour
{

    protected virtual void Awake()
    {
        SaveGameData.onSave += saveme;
        SaveGameData.onLoad += loadme;
    }

    protected virtual void Start()
    {
        loadme(SaveGameData.current);
    }

    protected virtual void saveme(SaveGameData savegame)
    {
        
    }

    protected virtual void loadme(SaveGameData savegame)
    {
        
    }

    protected virtual void OnDestroy()
    {
        SaveGameData.onLoad -= loadme;
        SaveGameData.onSave -= saveme;
    }
}
