using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    /// <summary>
    /// Zeichnet den Boxcollider
    /// </summary>
    /// <param name="mb">MonoBahaviour, das einen Boxcollieder als Geschwisterkomponente hat.</param>
    /// <param name=">Farbe des Gizmos<param>
    public static void DrawBoxCollider(MonoBehaviour mb, Color color)
    {
        if (UnityEditor.Selection.activeGameObject != mb.gameObject)
        {
            BoxCollider bc = mb.GetComponent<BoxCollider>();
            if (bc == null)
                return;

            Gizmos.color = color;
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = mb.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bc.center, bc.size);
            Gizmos.matrix = oldMatrix;
        }  
    }
}