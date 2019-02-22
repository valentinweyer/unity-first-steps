using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auslöser für automatischen Speicherpunkt. 
/// </summary>
public class SaveGameTrigger : MonoBehaviour
{
    /// <summary>
    /// Die Speicher-ID für den Trigger, die verhindert, dass ein Trigger 
    /// mehrmals nacheinander auslöst. 
    /// </summary>
    public string ID = "";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Jetzt speichern");
        SaveGameData savegame = SaveGameData.current;

        Player p = other.gameObject.GetComponent<Player>();
        if(p==null) //kein Spieler
        {
            Debug.Log("Lol");
            return;
        }
        else if (p.health <= 0f) // Spieler schon tot.
        {
            Debug.Log("Der Spieler hat keine Gesundheitspunkte mehr. Überspringe das Speichern.");
        }

        else if (savegame.lastTriggerID == ID)// Speicherpunkt schon gespeichert.
        {
            Debug.Log("Dieser Speicherpunkt hat bereits zuletzt gespeichert. Überspringe das Speichern.");
        }

        else //wenn nichts dagegen spricht, dann speichern.
        {
            savegame.lastTriggerID = ID;
            savegame.save();
        }
        

    }

    private void OnDrawGizmos()
    {
        Utils.DrawBoxCollider(this, Color.magenta);
    }

}