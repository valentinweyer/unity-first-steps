using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrb : SaveableDestructable
{

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.gameObject.GetComponent<Player>();
        if(p!= null) //Kollision mit dem Spieler
        {
            if (p.health != 0.6f)
            {
                p.health += 0.2f;
                gameObject.SetActive(false);
            }
        }
    }
}
