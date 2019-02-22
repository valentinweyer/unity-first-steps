using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeinScriptableObject : ScriptableObject
{
    public int eineZahl = 1;

    public float eineKommaZahl = 0.5f;

    public bool einSchalter = false;

    public string einText = "Hallo Welt";

    public GameObject go = null;

    public Texture bild = null;

    public void Awake()
    {
        Debug.Log("MeinScriptableObject Awake");
    }

    public void OnEnable()
    {
        Debug.Log("MeinScriptableObject OnEnable");
    }

    public void OnDisable()
    {
        Debug.Log("MeinScriptableObject OnDisable");
    }

    public void OnDestroy()
    {
        Debug.Log("MeinScriptableObject OnDestroy");
    }
}