using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
    /// <summary>
    /// Das Prefab.
    /// </summary>
    public GameObject prototype;

    private void Awake()
    {
        Instantiate(prototype, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
