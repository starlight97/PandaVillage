using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopItemBuy : MonoBehaviour
{
    private Text SelectedItemName;
    private Text SelectedItemInfomationText;
    private Text SelectedItemGold;
    private Button buyButton;

    public Slider slider;

    private int itemPrice;

    private void Start()
    {
        this.SelectedItemName = this.transform.Find("SelectedItemName").GetComponent<Text>();
        this.SelectedItemInfomationText = this.transform.Find("SelectedItemInfomationText").GetComponent<Text>();
        this.SelectedItemGold = this.transform.Find("SelectedItemGold").GetComponent<Text>();
        this.buyButton = this.GetComponentInChildren<Button>();

        buyButton.onClick.AddListener(() => {
            Debug.Log("구매완료");
        });

        //슬라이더의 값이 변경되면
        this.slider.onValueChanged.AddListener(delegate { SliderValueChange(); });
    }

    //슬라이더의 값이 변경될때마다 버튼의 텍스트를 변경해줘야함
    public void SliderValueChange()
    {
        var btnText = buyButton.GetComponentInChildren<Text>();
        btnText.text = String.Format("{0} : {1}", (int)slider.value, itemPrice);
    }

    public void SetText(UIShopItem item)
    {
        SelectedItemName.text = item.item_name;
        SelectedItemInfomationText.text = item.item_description;
        SelectedItemGold.text = item.price.ToString();
    }


    public void SetSliderMaxValue(int PlayerMoney, int itemPrice)
    {
        this.itemPrice = itemPrice;

        int MaxVal = (PlayerMoney / itemPrice);

        if (MaxVal <= 1)
        {
            this.slider.gameObject.transform.parent.gameObject.SetActive(false);    //살수있는 아이템 갯수가 1개 이하일때는 슬라이더를 끈다.

            buyButton.GetComponentInChildren<Text>().text = itemPrice.ToString();         //버튼 텍스트도 변경해줘야함
        }
        else
        {
            this.slider.gameObject.transform.parent.gameObject.SetActive(true);
            this.slider.minValue = 1;
        this.slider.maxValue = MaxVal;
        }

    }

}
