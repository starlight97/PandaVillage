using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManiRanch : UIBase
{
    private UIShop uiShop;
    private UIAnimalShop uIAnimalShop;
    private UIMarniesRanchSelect uIMarniesRanchSelect;
    public UnityAction<int> onAnimalBuyButtonClick;

    public override void Init()
    {
        base.Init();
        this.uIMarniesRanchSelect = this.transform.Find("UIMarniesRanchSelect").GetComponent<UIMarniesRanchSelect>();
        this.uiShop = this.transform.Find("UIShop").GetComponent<UIShop>();
        this.uIAnimalShop = this.transform.Find("UIAnimalShop").GetComponent<UIAnimalShop>();


        this.uiShop.onExitClick = () =>
        {
            this.HideShopUI(uiShop.gameObject);
        };
        this.uIAnimalShop.onExitClick = () =>
        {
            this.HideShopUI(uIAnimalShop.gameObject);
        };
        this.uIMarniesRanchSelect.onUIAnimalShopClick = () =>
        {
            uIMarniesRanchSelect.gameObject.SetActive(false);
            this.uiInGameMenu.gameObject.SetActive(false);
            this.uIAnimalShop.gameObject.SetActive(true);
        };
        this.uIMarniesRanchSelect.onUIShopClick = () =>
        {
            uIMarniesRanchSelect.gameObject.SetActive(false);
            this.uiInGameMenu.gameObject.SetActive(false);
            this.uiShop.gameObject.SetActive(true);
        };

        this.uIAnimalShop.onAnimalBuyButtonClick = (selectAnimalId) => {
            onAnimalBuyButtonClick(selectAnimalId);
        };
        this.uIMarniesRanchSelect.Init();
        this.uiShop.Init();
        this.uIAnimalShop.Init();
    }

    public void ShowShopUI()
    {
        Debug.Log("UIManiRanch");
        this.uIMarniesRanchSelect.gameObject.SetActive(true);
        
    }

    public void HideShopUI(GameObject go)
    {
        go.SetActive(false);
        this.uiInGameMenu.gameObject.SetActive(true);        
        this.uiInGameMenu.RePainting(12);
        this.uiMenu.RePainting(36);
    }
}
