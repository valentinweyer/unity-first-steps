using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stellt ein einzelnes Inventarobjekt auf dem UI-Canvas dar.
/// </summary>
public class InventoryItemRenderer : MonoBehaviour
{
    private InventoryItem _item; 

    public InventoryItem item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            
            GetComponent<Image>().sprite = item.uiImage;
        }
    }
    private void Start() 
    {
        Inventory.onItemRemoved += onItemRemoved;
    }

    private void onItemRemoved(InventoryItem item)
    {
        if(item==_item)
        {
            Destroy (gameObject);
            enabled = false;
            GetComponentInParent<InventoryRenderer> ().doLayout();
        }
    }

    private void OnDestroy() 
    {
        Inventory.onItemRemoved -= onItemRemoved;    
    }
}
