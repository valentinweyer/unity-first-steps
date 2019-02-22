using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevProfi.Utils;
using System.IO;

[System.Serializable]
public class SaveGameData
{
    /// <summary>
    /// Das aktuelle Savegame. 
    /// </summary>
    public static SaveGameData current = new SaveGameData();

    public Vector3 playerPosition = Vector3.zero;
    public float playerHealth = 0.6f;
    public int playerAmmo = 1;

    //Liste der IDs aller SaveableDestrucable-Objekte, die bereits entfernt wurden.
    public List<string> destroyedObjects = new List<string>();

    /// <summary>
    /// Lister der IDs aller Health-Orbs, die bereits eingesammelt wurden.
    /// </summary>
    //public List<string> disabledHealthOrbs = new List<string>();

    //public List<string> collectedAmmos = new List<string>();

    //public List<string> destroyedTrainers = new List<string>();


    [System.Serializable]
    public class BarrelData
    {
        public string ID = "";
        public Vector3 position = Vector3.zero;
    }

    /// <summary>
    /// Positon der Fässer.
    /// </summary>
    public List<BarrelData> barrelData = new List<BarrelData>();

    /// <summary>
    /// Sucht die gespeicherten Daten für das Fass mit der gegebenen ID.
    /// </summary>
    /// <param name="ID">Id des gesuchtem Fasses</param>
    /// <returns>Datensatz für das Fass mit der gegebenen Id oder BULL, wenn nicht vorhanden.</returns>
    public BarrelData findBarrelDataByID(string ID)
    {
        foreach (BarrelData bd in barrelData)
        {
            if (bd.ID == ID)
            {
                return bd;
            }
        }
        return null;
    }






    public bool doorIsOpen = false;

    /// <summary>
    /// Die ID des zuletzt ausgelösten Save-Triggers.
    /// </summary>
    /// <seealso cref="SaveGameTrigger.ID"/> 
    public string lastTriggerID = "";

    /// <summary>
    /// Name der Szene in der sich die Spielfigur momentan befindet.
    /// </summary>
    public string recentScene = "Szene1";


    /// <summary>
    /// Methoden, die sich in ein Save-Event eintragen wollen, müssen
    /// von dieser Form sein. 
    /// </summary>
    public delegate void SaveHandler(SaveGameData savegame);

    /// <summary>
    /// Methoden, die sich hier eintragen, werden aufgerufen, wenn
    /// Szenenobjekte ihren Zustand in den Speicherstand eintragen sollen.
    /// </summary>
    public static event SaveHandler onSave;
    /// <summary>
    /// Methoden, die sich hier eintragen, werden aufgerufen, wenn 
    /// ein Spielstand aus einer Savegame-Datei geladen wurde.
    /// Die Methoden sollten das Wiederherstellen des Objektzustands
    /// aus dem Spielstand implementieren.
    /// </summary>
    public static event SaveHandler onLoad;


    /// <summary>
    /// Liefert den Namen der Datei, in die der Spielstand geschrieben wird. 
    /// </summary>
    /// <returns>Name der Spielstanddatei.</returns>
    private static string getFilename()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "savegame.xml";
    }

    /// <summary>
    /// Speichert einen Spielstand.
    /// </summary>
    public void save()
    {
        Debug.Log("Speichere Spielstand " + getFilename());

        if (onSave != null) onSave(this);

        string xml = XML.Save(this);
        File.WriteAllText(getFilename(), xml);

        Debug.Log (xml);
    }

    /// <summary>
    /// Lädt einen Spielstand. 
    /// </summary>
    public static SaveGameData load()
    {
        if (!File.Exists(getFilename()))
            return new SaveGameData();

        Debug.Log("Lade Spielstand " + getFilename());
        SaveGameData save = XML.Load<SaveGameData>(File.ReadAllText(getFilename()));

        if (onLoad != null) onLoad(save);

        return save;
    }

}