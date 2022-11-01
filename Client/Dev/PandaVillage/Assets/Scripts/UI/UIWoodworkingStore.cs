using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UIWoodworkingStore : UIBase
{
    private UICarpentersSelect uICarpentersSelect;
    private UIShop uiShop;
    private UIHouseUpgrade uIHouseUpgrade;
    private UIBuildBuilding uIBuildBuilding;

    public UnityAction<int, int> selectBuildingId;

    public override void Init()
    {
        base.Init();
        this.uICarpentersSelect = this.transform.Find("UICarpentersSelect").GetComponent<UICarpentersSelect>();
        this.uiShop = this.transform.Find("UIShop").GetComponent<UIShop>();
        this.uIHouseUpgrade = this.transform.Find("UIHouseUpgrade").GetComponent<UIHouseUpgrade>();
        this.uIBuildBuilding = this.transform.Find("UIBuildBuilding").GetComponent<UIBuildBuilding>();


        this.uiShop.onExitClick = () => {
            this.HideShopUI(uiShop.gameObject);
        };
        this.uICarpentersSelect.onCarpentersShopClick = () => {
            SelectUI(uiShop.gameObject);
        };
        this.uICarpentersSelect.onHouseUpgradeClick = () => {
            SelectUI(uIHouseUpgrade.gameObject);
            this.uiInGameMenu.gameObject.SetActive(true);
        };
        this.uICarpentersSelect.onBuildClick = () => {
            SelectUI(uIBuildBuilding.gameObject);
        };

        this.uIBuildBuilding.selectBuildingId = (editType, id) => {
            selectBuildingId(editType, id);
        };
        

        this.uICarpentersSelect.Init();
        this.uiShop.Init();
        this.uIHouseUpgrade.Init();
        this.uIBuildBuilding.Init();
    }

    public void ShowShopUI()
    {        
        this.uICarpentersSelect.gameObject.SetActive(true);
    }

    public void HideShopUI(GameObject go)
    {
        go.SetActive(false);
        this.uiInGameMenu.gameObject.SetActive(true);
        this.uiInGameMenu.RePainting(12);
        this.uiMenu.RePainting(36);
    }
    public void SelectUI(GameObject go)
    {
        this.uICarpentersSelect.gameObject.SetActive(false);
        this.uiInGameMenu.gameObject.SetActive(false);
        go.gameObject.SetActive(true);
    }
}
