using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Verwaltet die Darstellung der einzelnen InventoryItemRenderer. 
/// </summary>
public class InventoryRenderer : MonoBehaviour
{
    /// <summary>
    /// Vorlage des Item Renderer-Gameobjects, das für jedes Invertarobjekt dupliziert wird.
    /// </summary>
    public GameObject itemRendererPrototype;
    
    void Start()
    {
        Inventory.onItemAdded += Inventory_onItemAdded;
        itemRendererPrototype.SetActive(false);
    }

    private void Inventory_onItemAdded(InventoryItem item)
    {
        GameObject newItemRenderer = Instantiate(itemRendererPrototype, itemRendererPrototype.transform.parent);
        InventoryItemRenderer iir = newItemRenderer.AddComponent<InventoryItemRenderer>();
        iir.item = item;
        newItemRenderer.SetActive(true);
        doLayout();
    }

    private void OnDestroy() 
    {
        Inventory.onItemAdded -= Inventory_onItemAdded;
    }
    
    /// <summary>
    /// Ordnet die sichbaren Inventarobjekte nebeneinnander an.
    /// </summary>
    public void doLayout() 
    {
        float x = 20f;
        foreach(InventoryItemRenderer r in FindObjectsOfType<InventoryItemRenderer>())
        {
            if(!r.enabled) continue;
            RectTransform rt = r.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(x, rt.anchoredPosition.y);
            x += rt.sizeDelta.x + 20f;
        }
    }
}
