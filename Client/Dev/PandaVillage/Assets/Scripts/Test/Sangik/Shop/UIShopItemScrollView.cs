using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIShopItemScrollView : MonoBehaviour
{
    public GameObject content;
    public GameObject ShopItemPrefab;

    public UnityAction<UIShopItem> onItemSelected;

    public void Init()
    {            
        var shopItembtn = content.GetComponentsInChildren<Button>();

        foreach (var btn in shopItembtn)
        {
            btn.onClick.AddListener(()=> {               

                var item = btn.GetComponent<UIShopItem>();

                onItemSelected(item);
            });
        }
    }

    public void SetItem(SeedData seedData, string item_description, Sprite sp)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(seedData.id, seedData.item_name, seedData.sprite_name, item_description, seedData.purchase_price);
        item.transform.GetChild(0).GetComponent<Text>().text = seedData.item_name;
        item.transform.GetChild(1).GetComponent<Text>().text = seedData.purchase_price.ToString();        
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }
    public void SetItem(MaterialData seedData, string item_description, Sprite sp)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(seedData.id, seedData.material_name, seedData.sprite_name, item_description, seedData.purchase_price);
        item.transform.GetChild(0).GetComponent<Text>().text = seedData.material_name;
        item.transform.GetChild(1).GetComponent<Text>().text = seedData.purchase_price.ToString();
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }
    public void SetItem(ToolData seedData, string item_description, Sprite sp)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(seedData.id, seedData.tool_name, seedData.sprite_name, item_description, seedData.price);
        item.transform.GetChild(0).GetComponent<Text>().text = seedData.tool_name;
        item.transform.GetChild(1).GetComponent<Text>().text = seedData.price.ToString();
        item.transform.GetChild(2).GetComponent<Image>().sprite = sp;
        item.transform.GetChild(2).GetComponent<Image>().SetNativeSize();
    }

    //test
    public void SetItem()
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        item.transform.GetChild(0).GetComponent<Text>().text = "테스트";
        item.transform.GetChild(1).GetComponent<Text>().text = "9999";
        item.transform.GetChild(2).GetComponent<Image>().sprite = null;
        item.transform.GetChild(2).GetComponent<Image>().color = Color.red;
    }
}
