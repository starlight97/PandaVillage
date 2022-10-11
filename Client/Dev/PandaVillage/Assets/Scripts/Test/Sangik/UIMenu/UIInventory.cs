using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public GameObject[] uIInventoryItems;
    private GameObject uIInventoryItemArea;

    public SpriteAtlas springObjectAtlas;

    public void Init(GameInfo gameInfo)
    {
        this.uIInventoryItemArea = this.transform.Find("UIInventoryItemArea").gameObject;

        InventorySize(gameInfo.playerInfo.inventory.size);
        var dicInventoryItem = gameInfo.playerInfo.inventory.dicItem;
        Debug.Log(dicInventoryItem.Count);

        foreach (var item in dicInventoryItem)
        {
            if(item.Value !=null)
            UIInventoryItemInit(item.Key , item.Value);
        }
    }

    public void InventorySize(int size)
    {
        foreach (var inven in uIInventoryItems)
        {
            inven.SetActive(false);
        }

        for (int i = 0; i < size; i++)
        {
            uIInventoryItems[i].SetActive(true);
        }
    }


    public void UIInventoryItemInit(int key, InventoryData inventoryData)
    {
       var uIInventoryItem = uIInventoryItemArea.transform.GetChild(key).GetComponent<UIInventoryItem>();
        Debug.Log(inventoryData.itemId);
        Sprite sp = GetItemSprite(inventoryData.itemId);
        uIInventoryItem.Init(sp, inventoryData.amount);   

    }

    public Sprite GetItemSprite(int itemID)
    {
        
        int i = itemID / 1000;
        switch (i)
        {
            case 1:
                var seedData = DataManager.instance.GetData<SeedData>(itemID);
                Debug.Log(seedData.sprite_name);
                return springObjectAtlas.GetSprite(seedData.sprite_name);
                
            case 4:
                var materialData = DataManager.instance.GetData<MaterialData>(itemID);
                Debug.Log(materialData.sprite_name);
                return springObjectAtlas.GetSprite(materialData.sprite_name);
                
            case 6:
                var toolData = DataManager.instance.GetData<ToolData>(itemID);
                Debug.Log(toolData.sprite_name);
                return springObjectAtlas.GetSprite(toolData.sprite_name);               

            default: Debug.Log("이상해요"); return null;
        }
    }

}
