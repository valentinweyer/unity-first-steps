using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Realisiert, wie die Tür mit einem Schalter interaktiv
/// geöffnet werden kann. 
/// </summary>
public class DoorSwitch : Saveable
{

    /// <summary>
    /// Animator auf dem Tür-Mesh, um das Öffnen der Tür zu realisieren.
    /// </summary>
    public Animator doorAnimator;

    /// <summary>
    /// Zeiger auf das Mesh, das die Lichter an der Türschalter-Konsole
    /// darstellt. 
    /// </summary>
    public MeshRenderer mesh;

    /// <summary>
    /// Schlüssel, der im Inventar vorhanden sein muss, damit der Schalter funktioniert.
    /// </summary>
    public InventoryItem key;

    /// <summary>
    /// Steuert die Tür mittels der Schaltkonsole, wenn die Feuer-Taste gedrückt wird.
    /// </summary>
    /// <param name="other">Other.</param>
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))
        {
            if(SaveGameData.current.inventory.contains(key))
            {
                SaveGameData.current.inventory.remove(key);
                openTheDoor();
            }
            else
            {
                Debug.Log ("Schlüssel fehlt"); 
            }
        }
    }

    /// <summary>
    /// Öffnet die Tür, inklusive Synchronisation von
    /// Schalter und Türobjekt.
    /// </summary>
    private void openTheDoor()
    {
        doorAnimator.SetBool("isOpen", true);

        Material[] mats = mesh.materials;
        Material m2 = mats[2]; //ausgeschaltete material
        mats[2] = mats[1];
        mats[1] = m2;
        mesh.materials = mats;
    }


    protected override void saveme(SaveGameData savegame)
    {
        base.saveme(savegame);
        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
    }

    protected override void loadme(SaveGameData savegame)
    {
        base.loadme(savegame);
        Debug.Log("Doorswitch loadme");
        if (savegame.doorIsOpen)
            openTheDoor();
    }

    private void OnDrawGizmos()
    {
        Utils.DrawBoxCollider(this, Color.green);
    }

}


//using UnityEngine;

///// <summary>
///// Realisiert, wie die Tür mit einem Schalter interaktiv
///// geöffnet werden kann. 
///// </summary>
//public class DoorSwitch : Saveable
//{
//    /// <summary>
//    /// Animator auf dem Tür-Mesh, um das Öffnen der Tür zu realisieren.
//    /// </summary>
//    public Animator doorAnimator; 

//    /// <summary>
//    /// Zeiger auf das Mesh, das die Lichter an der Türschalter-Konsole
//    /// darstellt. 
//    /// </summary>
//    public MeshRenderer mesh;

//    /// <summary>
//    /// Steuert die Tür mittels der Schaltkonsole, wenn die Feuer-Taste gedrückt wird.
//    /// </summary>
//    /// <param name="other">Other.</param>
//    private void OnTriggerStay(Collider other)
//    {
//        if (Input.GetAxisRaw("Fire1") != 0f && !doorAnimator.GetBool("isOpen"))
//        {
//            openTheDoor();
//        }
//    }

//    /// <summary>
//    /// Öffnet die Tür, inklusive Synchronisation von
//    /// Schalter und Türobjekt.
//    /// </summary>
//    private void openTheDoor()
//    {
//        doorAnimator.SetBool("isOpen", true);

//        Material[] mats = mesh.materials;
//        Material m2 = mats[2]; //ausgeschaltete material
//        mats[2] = mats[1];
//        mats[1] = m2;
//        mesh.materials = mats;
//    }



//    protected override void saveme(SaveGameData savegame)
//    {
//        Debug.Log("Test");
//        base.saveme(savegame);

//        savegame.doorIsOpen = doorAnimator.GetBool("isOpen");
//    }

//    protected override void loadme(SaveGameData savegame)
//    {
//        base.loadme(savegame);
//        Debug.Log("Doorswitch loadme");

//        if (savegame.doorIsOpen)
//            openTheDoor();
//    }

//    private void OnDrawGizmos()
//    {
//        Utils.DrawBoxCollider(this);
//    }
//}