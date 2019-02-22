using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Steuert das Verhalten einer abgeschossenen Pistolenkugel.
/// </summary>
public class Bullet : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * (transform.rotation.y < 0f ? -5f : 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BulletCatcher bc = collision.gameObject.GetComponent<BulletCatcher>();
        if (bc != null)
            bc.onHitByBullet();
        Destroy(gameObject); //die kugel wird gelöscht, wenn sie irgendwo auftrifft
    }
} 
