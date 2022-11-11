using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
public class UIShopPlayerGold : MonoBehaviour
{
    public Text GoldText;
    public UnityAction<int> onChangeGold;

    public void Init()
    {
        //this.GoldText = this.transform.Find("GoldText").GetComponent<Text>();

        onChangeGold = (val) => {
            this.GoldText.text = val.ToString();
            //나중에하기
            //SetGoldText(val);
        };
    }

    public void SetGoldText(int gold)
    {
        var goldArr = gold.ToString().ToCharArray();
        Array.Reverse(goldArr);
    }


}
