using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    private UIMenuButton uIMenuButton;
    private UIInventory uIInventory;

    public GameObject[] menuGo;

    private GameInfo gameInfo;

    private void Start()
    {
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);

        InfoManager.instance.Init(0);
        InfoManager.instance.LoadData();
        gameInfo = InfoManager.instance.GetInfo();

        this.uIMenuButton = this.transform.Find("UIMenuButton").GetComponent<UIMenuButton>();
        this.uIInventory = this.transform.Find("UIInventory").GetComponent<UIInventory>();

        uIMenuButton.Init();
        uIMenuButton.onMenuButtonClicked = (text) => {
            foreach (var go in menuGo)
            {
                if (go.name == text)
                    go.SetActive(true);
                else
                    go.SetActive(false);
            }
            //this.transform.Find(text).gameObject.SetActive(true);
        };


        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            uIInventory.Init(gameInfo);
        });

        uIMenuButton.FirstItemSelect();

        
    }   
}
