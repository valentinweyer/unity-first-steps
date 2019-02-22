using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoMonitor : MonoBehaviour
{
    public Text uiText;

    private Gun gun;

    private void Update()
    {
        if (gun == null)
        {
            Player p = FindObjectOfType<Player>();
            if (p != null)
            {
                gun = p.GetComponentInChildren<Gun>();
            }
        }
        else
        {
            uiText.text = gun.ammo.ToString();
        }
    }
}



