using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Verhalten für die Robo-Kugel.
/// </summary>
public class Trainer : BulletCatcher
{

    /// <summary>
    /// Prefab, das die Explosion realisiert.
    /// </summary>
    public GameObject explosionPrototype;

    /// <summary>
    /// Soundplayer für den Explosionseffekt.
    /// </summary>
    public GameObject explosionAudioPrototype;

    public override void onHitByBullet()
    {
        base.onHitByBullet();
        Debug.Log("Trainer zerstört");

        Instantiate(explosionPrototype, transform.position, transform.rotation);
        Instantiate(explosionAudioPrototype, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
} 
