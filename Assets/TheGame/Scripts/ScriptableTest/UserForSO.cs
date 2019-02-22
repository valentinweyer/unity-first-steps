using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserForSO : MonoBehaviour
{
    public MeinScriptableObject mso;

    void Start()
    {
        Debug.Log("Mein mso" + mso.einText);
    }
}