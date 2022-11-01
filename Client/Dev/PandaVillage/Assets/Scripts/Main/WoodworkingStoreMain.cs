using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodworkingStoreMain : GameSceneMain
{
    private UIWoodworkingStore uiWoodworkingStore;
    private ShopObject shopObject;

    public int purchaseBuildingId;
    // 1 = create   // 신규 설치
    // 2 = move     // 이동
    // 3 = delete   // 철거
    public int editType;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        var info = InfoManager.instance.GetInfo();
        var objs = info.objectInfoList.FindAll(x => x.sceneName == "Farm");

        this.uiWoodworkingStore = this.uiBase.GetComponent<UIWoodworkingStore>();
        this.shopObject = GameObject.FindObjectOfType<ShopObject>();

        this.shopObject.onShowShopUI = () =>
        {
            this.uiWoodworkingStore.ShowShopUI();
        };
        this.uiWoodworkingStore.selectBuildingId = (editType, id) => 
        {
            // 신규 생성
            this.editType = editType;
            this.purchaseBuildingId = id;
            Dispatch("onClickFarmEdit");
            //Dispatch("onClickEditBuilding");
        };

    }
}
