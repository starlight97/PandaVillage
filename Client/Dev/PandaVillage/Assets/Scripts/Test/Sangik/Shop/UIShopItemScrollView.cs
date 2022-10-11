using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.U2D;

public class UIShopItemScrollView : MonoBehaviour
{
    private UIShopItem[] shopItem;

    public GameObject content;
    public GameObject ShopItemPrefab;
    public SpriteAtlas springObjectAtlas;

    public UnityAction<UIShopItem> onItemSelected;

    public void Init()
    {            
        shopItem = content.GetComponentsInChildren<UIShopItem>();

        foreach (var item in shopItem)
        {
            item.ButtonInit();
            item.onClicked = () => { 
                foreach (var i in shopItem)                
                    i.ChangeButtonColor(Color.white);                
            };
        }
    }

    //스크롤뷰의 첫번째 아이템이 선택되어있게함
    public void FirstItemSelect()
    {
        var firstItemBtn = content.GetComponentInChildren<Button>();
        firstItemBtn.Select();
        var firstItem = firstItemBtn.GetComponent<UIShopItem>();
        onItemSelected(firstItem);
        firstItem.ChangeButtonColor(Color.cyan);
    }

    public void SetItem(object data)
    {
        if (data.GetType() == typeof(SeedData))
        {
            SetItem((SeedData)data);
        }
        if (data.GetType() == typeof(MaterialData))
        {
            SetItem((MaterialData)data);
        }
        if (data.GetType() == typeof(ToolData))
        {
            SetItem((ToolData)data);
        }
    }

    public void SetItem(SeedData seedData)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(seedData.id, seedData.item_name, seedData.sprite_name, seedData.description, seedData.purchase_price);
        var sp =springObjectAtlas.GetSprite(seedData.sprite_name);
        item.transform.GetChild(0).GetComponent<Text>().text = seedData.item_name;
        item.transform.GetChild(1).GetComponent<Text>().text = seedData.purchase_price.ToString();        
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }
    public void SetItem(MaterialData materialData)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(materialData.id, materialData.material_name, materialData.sprite_name, materialData.description, materialData.purchase_price);
        var sp = springObjectAtlas.GetSprite(materialData.sprite_name);
        item.transform.GetChild(0).GetComponent<Text>().text = materialData.material_name;
        item.transform.GetChild(1).GetComponent<Text>().text = materialData.purchase_price.ToString();
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }
    public void SetItem(ToolData toolData)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(toolData.id, toolData.tool_name, toolData.sprite_name, toolData.description, toolData.price);
        var sp = springObjectAtlas.GetSprite(toolData.sprite_name);
        item.transform.GetChild(0).GetComponent<Text>().text = toolData.tool_name;
        item.transform.GetChild(1).GetComponent<Text>().text = toolData.price.ToString();
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }       
}
