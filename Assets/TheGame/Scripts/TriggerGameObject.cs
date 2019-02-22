using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aktiviert ein Objekt, wenn ein Trigger ausgelöst wird.
/// </summary>
public class TriggerGameObject : MonoBehaviour
{
    /// <summary>
    /// Das Objket, das durch diesen Trigger aktiviert wird.
    /// </summary>
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null)
        {
            return; // kollision mit etwas anderem als Spieler -> ignorieren
        }

        target.GetComponent<Rigidbody>().isKinematic = false;
    }
}
