using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIVarietyStore : UIBase
{
    private UIShop uiShop;

    public override void Init()
    {
        base.Init();
        this.uiShop = GameObject.FindObjectOfType<UIShop>();


        this.uiShop.onExitClick = () =>
        {
            this.HideShopUI();
        };
        this.HideShopUI();
        this.uiShop.Init();
    }

    public void ShowShopUI()
    {
        this.uiInGameMenu.gameObject.SetActive(false);
        this.uiShop.gameObject.SetActive(true);
        this.uiMenu.RePainting(36);
    }

    public void HideShopUI()
    {
        this.uiShop.gameObject.SetActive(false);
        this.uiInGameMenu.gameObject.SetActive(true);
        this.uiInGameMenu.RePainting(12);
    }

}
