using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToScene : MonoBehaviour
{
    /// <summary>
    /// Name der Szene die geladen wird, wenn die Figur den Trigger auslöst.
    /// </summary>
    public string scene = "";

    private void OnTriggerEnter(Collider other)
    {
        LevelManager lm = FindObjectOfType<LevelManager>();

        if (lm)
        {
            lm.loadScene(scene);
        }
    }

    private void OnDrawGizmos()
    {
        Utils.DrawBoxCollider(this, Color.red);
    }
}

