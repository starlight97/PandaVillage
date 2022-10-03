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
    private UIPlayerGold uIPlayerGold;
    private Button inventoryButton;
    private Button exitButton;

    public SpriteAtlas springObjectAtlas;

    private GameInfo gameInfo;
    
    public void Test()
    {
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
        var shopData = DataManager.instance.GetDataList<ShopData>();
        var shopitemData = DataManager.instance.GetDataList<ShopitemData>();
        DataManager.instance.onDataLoadFinished.AddListener(() => 
        {
            shopData = DataManager.instance.GetDataList<ShopData>();
            shopitemData = DataManager.instance.GetDataList<ShopitemData>();
            Test2(shopData, shopitemData);
        });       
    }

    public void Test2(IEnumerable<ShopData> shopData, IEnumerable<ShopitemData> shopitemData)
    {
        Dictionary<int, ShopitemData> dicItemDatas = new Dictionary<int, ShopitemData>();
        shopitemData.ToDictionary(x => x.id).ToList().ForEach(x => dicItemDatas.Add(x.Key, x.Value));


        foreach (var shop in shopData)
        {
            if (shop.shop_category != 0)
                continue;

            var itemData = dicItemDatas.GetValueOrDefault(shop.item_id);
            Sprite sp = this.springObjectAtlas.GetSprite(itemData.atlas_sprite_name);
            uIShopItemScrollView.SetItem(shop.price, sp, itemData);
        }       

        //===========================================================
        gameInfo = InfoManager.instance.GetInfo(1);
        ButtonInit();

    }

    private void LoadInfo(int playerId)
    {
        bool check = InfoManager.instance.LoadData();
        GameInfo gameInfo = new GameInfo();
        if (check == false)
        {
            gameInfo.playerId = playerId;
            gameInfo.objectInfoList = new List<ObjectInfo>();
            gameInfo.playerInfo = new PlayerInfo("강아지", "강아지");

            gameInfo.playerInfo.inventory.dicItem.Add(1000, 10);
            gameInfo.playerInfo.inventory.dicItem.Add(2000, 20);
            gameInfo.playerInfo.inventory.dicItem.Add(3000, 30);
            InfoManager.instance.InsertInfo(gameInfo);

            InfoManager.instance.SaveInfo();
            Debug.Log("신규 유저 입니다.");
        }

        else
        {
            Debug.Log("기존 유저 입니다.");
            gameInfo = InfoManager.instance.GetInfo(playerId);

            var datas = gameInfo.objectInfoList;
            foreach (var data in datas)
            {
                Debug.Log(data.objectId);
                Debug.Log(data.sceneName);
                Debug.Log(data.posX);
                Debug.Log(data.posY);
            }
        }
    }

    private void Start()
    {
        LoadInfo(1);

        this.uIShopItemScrollView = this.transform.Find("UIShopItemScrollView").GetComponent<UIShopItemScrollView>();
        this.uIShopItemBuy = this.transform.Find("UIShopItemBuy").GetComponent<UIShopItemBuy>();
        this.uIPlayerGold = this.transform.Find("UIPlayerGold").GetComponent<UIPlayerGold>();
        this.inventoryButton = this.transform.Find("InventoryButton").GetComponent<Button>();
        this.exitButton = this.transform.Find("ExitButton").GetComponent<Button>();

        Test();
        
    }

    private UIShopItem selectedItemTemp;
    private void ButtonInit()
    {
        uIShopItemScrollView.Init();


        exitButton.onClick.AddListener(() => {
            this.gameObject.SetActive(false);
        });


        inventoryButton.onClick.AddListener(() => {
            Debug.Log("인벤토리 팝업, 아이템 판매기능");
        });

        //아이템이 선택되면
        uIShopItemScrollView.onItemSelected = (item) => {
            uIShopItemBuy.SetText(item);
            uIShopItemBuy.SetSliderMaxValue(gameInfo.playerInfo.gold, item.price);    //슬라이더의 최댓값은 플레이어가 살수있는 최대수량이어야함
            this.selectedItemTemp = item;
        };

        this.uIPlayerGold.onChangeGold(gameInfo.playerInfo.gold);

        this.uIShopItemBuy.buyButtonClicked = (amount) => {
            
            var bill = selectedItemTemp.price * amount;
            Debug.Log(bill);
            if (gameInfo.playerInfo.gold - bill >= 0)
            {
                gameInfo.playerInfo.gold = gameInfo.playerInfo.gold - bill;
                this.uIPlayerGold.onChangeGold(gameInfo.playerInfo.gold);
                InfoManager.instance.UpdateInfo(gameInfo);
                InfoManager.instance.SaveInfo();
            }
            else
                Debug.Log("잔액이 부족합니다");
        };
    }

    
}
