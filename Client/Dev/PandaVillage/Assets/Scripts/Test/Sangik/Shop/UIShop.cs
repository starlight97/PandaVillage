using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.IO;
using System.Linq;

public enum eShopCategory
{
    GeneralStore,   //잡화점
    CarpenterShop,  //목공소
    MarniesRanch,   //마니의목장

}
public enum eDataType
{
    Tool, Materials, Seed,
}

public class UIShop : MonoBehaviour
{
    private UIShopItemScrollView uIShopItemScrollView;
    private UIShopItemBuy uIShopItemBuy;
    private UIPlayerGold uIPlayerGold;
    private Button inventoryButton;
    private Button exitButton;

    public SpriteAtlas springObjectAtlas;

    private GameInfo gameInfo;

    public eShopCategory eShopCategory;


    public void Test()
    {
        
        DataManager.instance.Init();

        DataManager.instance.LoadAllData(this);
        var shopData = DataManager.instance.GetDataList<ShopData>();


        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            shopData = DataManager.instance.GetDataList<ShopData>();

            var datas = DataManager.instance.GetDataList<ToolData>().ToList();

            Test2(shopData);
        });
    }
    public void Test2(IEnumerable<ShopData> shopData)
    {
        foreach (var shop in shopData)
        {
            if (shop.shop_category == (int)eShopCategory)
            {
               // Test3(shop);
                Test4(shop);
            }
        }       

        //===========================================================
        gameInfo = InfoManager.instance.GetInfo(1);
        ButtonInit();

    }
    //===============================
    public void Test3(ShopData shop)
    {
        Sprite sp;

        switch (eShopCategory)
        {
            case eShopCategory.GeneralStore :
                var seed = DataManager.instance.GetData<SeedData>(shop.item_id);
                sp = springObjectAtlas.GetSprite(seed.sprite_name);
                uIShopItemScrollView.SetItem(seed, shop.item_description, sp);
                break;
            case eShopCategory.CarpenterShop :                    
                var carpenter = DataManager.instance.GetData<MaterialData>(shop.item_id);
                sp = springObjectAtlas.GetSprite(carpenter.sprite_name);
                uIShopItemScrollView.SetItem(carpenter, shop.item_description, sp);
                break;
            case eShopCategory.MarniesRanch :
                if (shop.item_id < 5000)
                {
                    var materialData = DataManager.instance.GetData<MaterialData>(shop.item_id);
                    sp = springObjectAtlas.GetSprite(materialData.sprite_name);
                    uIShopItemScrollView.SetItem(materialData, shop.item_description, sp);
                }
                else
                {
                    var toolData = DataManager.instance.GetData<ToolData>(shop.item_id);
                    sp = springObjectAtlas.GetSprite(toolData.sprite_name);
                    uIShopItemScrollView.SetItem(toolData, shop.item_description, sp);
                }
                break;
            default: break;
        }                
    }
    public void Test4(ShopData shop)
    {
        Sprite sp;
        int i = shop.item_id / 1000;
        switch (i)
        {
            case 1:
                var seedData = DataManager.instance.GetData<SeedData>(shop.item_id);
                sp = springObjectAtlas.GetSprite(seedData.sprite_name);
                uIShopItemScrollView.SetItem(seedData, shop.item_description, sp);
                break;
            case 4:
                var materialData = DataManager.instance.GetData<MaterialData>(shop.item_id);
                sp = springObjectAtlas.GetSprite(materialData.sprite_name);
                uIShopItemScrollView.SetItem(materialData, shop.item_description, sp);
                break;
            case 6:
                var toolData = DataManager.instance.GetData<ToolData>(shop.item_id);
                sp = springObjectAtlas.GetSprite(toolData.sprite_name);
                uIShopItemScrollView.SetItem(toolData, shop.item_description, sp);
                break;

            default: break;
        }
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

            if (selectedItemTemp != null)
            {
                var bill = selectedItemTemp.price * amount;
                Debug.Log(bill);
                if (gameInfo.playerInfo.gold - bill >= 0)
                {
                    BuyingItem(amount);
                    gameInfo.playerInfo.gold = gameInfo.playerInfo.gold - bill;
                    this.uIPlayerGold.onChangeGold(gameInfo.playerInfo.gold);
                    InfoManager.instance.UpdateInfo(gameInfo);
                    InfoManager.instance.SaveInfo();
                }
                else
                    Debug.Log("잔액이 부족합니다");

            }                          
        };
    }
    public void BuyingItem(int amount)
    {
        if(!gameInfo.playerInfo.inventory.dicItem.ContainsKey(selectedItemTemp.id))
        gameInfo.playerInfo.inventory.dicItem.Add(selectedItemTemp.id, amount);
        else
        {
            int itemAmount = gameInfo.playerInfo.inventory.dicItem.GetValueOrDefault(selectedItemTemp.id);
            itemAmount += amount;
            gameInfo.playerInfo.inventory.dicItem.Remove(selectedItemTemp.id);
            gameInfo.playerInfo.inventory.dicItem.Add(selectedItemTemp.id, itemAmount);
            Debug.LogFormat("아이템 : {0} 양 : {1}", selectedItemTemp.item_name, itemAmount);
        }
    }

    
}
