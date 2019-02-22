using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basis-Script für alle Dinge, die mit der Pistole abgeschossen weredn können.
/// </summary>
public class BulletCatcher : SaveableDestructable
{
    
    public virtual void onHitByBullet()
    {
        Debug.Log(gameObject.name + " wurde von einer Kugel getroffen");
    }
}
