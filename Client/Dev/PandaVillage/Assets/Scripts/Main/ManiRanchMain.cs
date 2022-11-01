using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManiRanchMain : GameSceneMain
{
    private UIManiRanch uiManiRanch;
    private ShopObject shopObject;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiManiRanch = this.uiBase.GetComponent<UIManiRanch>();
        this.shopObject = GameObject.FindObjectOfType<ShopObject>();

        this.shopObject.onShowShopUI = () =>
        {
            Debug.Log("ManiRanchMain");
            this.uiManiRanch.ShowShopUI();
        };

        this.uiManiRanch.onAnimalBuyButtonClick = (selectAnimalId) => { 
            //씬전환후 원하는 coop에 동물을 집어넣어야함 플레이어 info에 동물작업해야함
        };
    }
}
