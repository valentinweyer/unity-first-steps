using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Zeig die Spieler-Gesundheit in Form eines Fortschrittbalkens.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// Zeigt auf die Grafik, die skaliert wird.
    /// </summary>
    public Image progressbar;

    /// <summary>
    /// Zeiger auf die aktuelle Player-Komponente.
    /// </summary>
    private Player player;

    private Menu menuRoot;



    private void Update ()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
        else
        {
            progressbar.fillAmount = player.health;   
        }
    }
}
