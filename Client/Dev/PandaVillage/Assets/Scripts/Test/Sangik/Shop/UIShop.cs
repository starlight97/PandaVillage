using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.IO;
using System.Linq;


public class UIShop : MonoBehaviour
{
    private UIShopItemScrollView uIShopItemScrollView;
    private UIShopItemBuy uIShopItemBuy;
    private Button InventoryButton;
    private Button ExitButton;

    public SpriteAtlas springObjectAtlas;
    public Image imgIcon;

    public string iconName = "blueJazzSeed";

    public int TestPlayerMoney = 10000;    


    public void Test()
    {
        DataManager.instance.Init();
        var json = File.ReadAllText("Assets/Resources/Datas/shop_data.json");
        //var json = Resources.Load("Datas/shop_data").ToString(); 
        var json2 = Resources.Load("Datas/shop_item_data").ToString();

        DataManager.instance.LoadData<ShopData>(json);
        DataManager.instance.LoadData<ShopitemData>(json2);

        var shopData = DataManager.instance.GetDataList<ShopData>();
        var shopItemData = DataManager.instance.GetDataList<ShopitemData>();

        Dictionary<int, ShopitemData> dicItemDatas = new Dictionary<int, ShopitemData>();        
        shopItemData.ToDictionary(x => x.id).ToList().ForEach(x => dicItemDatas.Add(x.Key, x.Value));
        
        

        foreach (var shop in shopData) 
        {
            if (shop.shop_category != 0)
                continue;

           var itemData = dicItemDatas.GetValueOrDefault(shop.item_id);
            Sprite sp = this.springObjectAtlas.GetSprite(itemData.atlas_sprite_name);
            uIShopItemScrollView.SetItem(shop.price, sp , itemData);
        }
    }


    private void Start()
    {
        this.uIShopItemScrollView = this.transform.Find("UIShopItemScrollView").GetComponent<UIShopItemScrollView>();
        this.uIShopItemBuy = this.transform.Find("UIShopItemBuy").GetComponent<UIShopItemBuy>();
        this.InventoryButton = this.transform.Find("InventoryButton").GetComponent<Button>();
        this.ExitButton = this.transform.Find("ExitButton").GetComponent<Button>();

        Test();
        uIShopItemScrollView.Init();


        ExitButton.onClick.AddListener(() => {
            this.gameObject.SetActive(false);
        });


        InventoryButton.onClick.AddListener(() => {
            Debug.Log("인벤토리 팝업, 아이템 판매기능");
        });

        //아이템이 선택되면
        uIShopItemScrollView.onItemSelected = (item) => {
            uIShopItemBuy.SetText(item);        //item data에 아이템의설명이 있어야할듯?
            uIShopItemBuy.SetSliderMaxValue(TestPlayerMoney, item.price);    //슬라이더의 최댓값은 플레이어가 살수있는 최대수량이어야함
        };  
       
    }   
}
