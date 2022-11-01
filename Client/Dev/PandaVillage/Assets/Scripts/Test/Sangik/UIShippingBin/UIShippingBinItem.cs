using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIShippingBinItem : MonoBehaviour
{
    private Image shippingBinImage;
    private Button lastShippingItemButton;
    private Text ShippingBinText;
    private Text lastShippingItemText;

    //마지막으로 배송시킨 아이템의 id와 수량을 info에서 가져와야함
    private InventoryData lastItemData;

    public void Init()
    {
        this.shippingBinImage = transform.Find("ShippingBinImage").GetComponent<Image>();
        this.lastShippingItemButton = transform.Find("LastShippingItemButton").GetComponent<Button>();
        this.ShippingBinText = transform.Find("ShippingBinText").GetComponent<Text>();
        this.lastShippingItemText = transform.Find("LastShippingItemText").GetComponent<Text>();
    }

}
