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

    public void SetItem(int itemPrice, Sprite itemSprite, ShopitemData shopitemData)
    {
        var item = Instantiate(ShopItemPrefab, this.content.transform);
        var uiShopItem = item.GetComponent<UIShopItem>();
        uiShopItem.Init(shopitemData.id, shopitemData.item_name, shopitemData.atlas_sprite_name, shopitemData.item_description, itemPrice);
        item.transform.GetChild(0).GetComponent<Text>().text = shopitemData.item_name;
        item.transform.GetChild(1).GetComponent<Text>().text = itemPrice.ToString();
        item.transform.GetChild(2).GetComponent<Image>().sprite = itemSprite;
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
