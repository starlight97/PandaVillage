using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIShippingBin : MonoBehaviour
{
    private UIInventory uIInventory;
    private UIShippingBinItem uIShippingBinItem;

    public void Start()
    {
        this.uIInventory = transform.Find("UIInventory").GetComponent<UIInventory>();
        this.uIShippingBinItem = transform.Find("UIShippingBinItem").GetComponent<UIShippingBinItem>();

        uIInventory.Init(UIInventory.eInventoryType.Ui, 36);
        uIShippingBinItem.Init();

        uIInventory.onShippingItem = (lastItem) => { 
        };
    }

}
