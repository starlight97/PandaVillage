using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopItem : MonoBehaviour
{
    public int id;
    public string item_name;
    public string atlas_sprite_name;
    public string item_description;
    public int price;

    public void Init(int id, string itemName, string spriteName, string itemDescription, int price)
    {
        this.id = id;
        this.item_name = itemName;
        this.atlas_sprite_name = spriteName;
        this.item_description = itemDescription;
        this.price = price;
    }


}
