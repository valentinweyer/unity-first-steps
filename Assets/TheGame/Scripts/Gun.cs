using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implementiert das Verhalten der Pistole.
/// </summary>
public class Gun : Saveable
{
    /// <summary>
    /// Lichtquelle für den Schuss.
    /// </summary>
    private Light fireLight;

    private Animator playerAnim;

    /// <summary>
    /// Sound für den Pistolenschuss.
    /// </summary>
    public AudioSource soundShot;

    /// <summary>
    /// Sound, wenn versucht wird, die Waffe ohne Munition abzufeuern.
    /// </summary>
    public AudioSource soundEmpty;

    /// <summary>
    /// Sound zum laden der Waffe.
    /// </summary>
    public AudioSource soundLoad;

    private int _ammo = 3;
    /// <summary>
    /// Anzahl der Patronen in der Waffe.
    /// </summary>
    public int ammo
    {
        get { return _ammo; }
        set { if (!soundLoad.isPlaying && value>ammo)
            {
                soundLoad.Play();
            }
            _ammo = value;
        }
    }


    // Use this for initialization
    override protected void Start()
    {
        fireLight = GetComponentInChildren<Light>();
        fireLight.enabled = false;
        playerAnim = GetComponentInParent<Animator>();

        bulletPrototype.SetActive(false);

        base.Start();
    }

    /// <summary>
    /// Ist der vorherige Schuss schon fertig bearbeitet?
    /// </summary>
    private bool shotDone = true;

    /// <summary>
    /// Feuert einen Schuss aus der Pistole ab.
    /// </summary>
    public void shoot()
    {
        if (shotDone)
        {
            if (ammo > 0)
            {
                StartCoroutine(doShoot());
            }
            else
            {
                if (!soundEmpty.isPlaying)
                {
                    soundEmpty.Play();
                }
            }
        }
    }

    /// <summary>
    /// Original Kugel, die dupliziert in die Szene geschossen wird.
    /// </summary>
    public GameObject bulletPrototype;

    /// <summary>
    /// Regelt die Schuss-Ausführung.
    /// </summary>
    /// <returns>(Enumerator)</returns>
    private IEnumerator doShoot()
    {
        shotDone = false;

        soundShot.Play();
        GameObject bullet = Instantiate(bulletPrototype, bulletPrototype.transform.parent);
        bullet.transform.parent = null;
        //bullet.transform.localRotation = Quaternion.Euler (90f, 0f, transform.rotation.y <0f ? 0f:180f);
        bullet.SetActive(true);
        ammo -= 1; //Patrone verbrauchen 

        playerAnim.SetTrigger("gunShot");
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;

        while (playerAnim.GetCurrentAnimatorStateInfo(1).IsName("gunShot"))
            yield return new WaitForEndOfFrame();

        shotDone = true;
    }

    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);
        savegame.playerAmmo = ammo;

    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        ammo = savegame.playerAmmo;
    }


}
