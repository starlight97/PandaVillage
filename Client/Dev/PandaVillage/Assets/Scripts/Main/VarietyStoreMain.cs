using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarietyStoreMain : GameSceneMain
{
    private UIVarietyStore uiVarietyStore;
    private ShopObject shopObject;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiVarietyStore = this.uiBase.GetComponent<UIVarietyStore>();
        this.shopObject = GameObject.FindObjectOfType<ShopObject>();

        this.shopObject.onShowShopUI = () =>
        {
            this.uiVarietyStore.ShowShopUI();
        };

    }

}
