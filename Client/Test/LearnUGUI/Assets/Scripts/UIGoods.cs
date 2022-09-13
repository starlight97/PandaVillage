using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGoods : MonoBehaviour
{
    public UIGoodsItem[] uiGoodsItems;
    public UnityEvent<UIGoodsItem.eType> onClickItem = new UnityEvent<UIGoodsItem.eType>();
    int myCoins = 50000;
    int myEnergy = 10;
    int myDiamond = 2000;

    public void Init()
    {
        foreach (var item in uiGoodsItems)
        {
            item.onClick = (type) => {
                //Debug.Log("===> " + type);
                this.onClickItem.Invoke(type);
            };
            item.Init();
        }
    }
    public void UpdateCoin(int val)
    {
        myCoins += val;
        this.uiGoodsItems[0].textVal.text = string.Format("{0:#,0}", myCoins);
    }
    public void UpdateEnergy(int val)
    {
        myEnergy += val;
        var maxEnergy = 100;

        //myEnergy = Mathf.Clamp

        this.uiGoodsItems[1].textVal.text = string.Format("{0}/{1}", myEnergy, maxEnergy);

    }
    public void UpdateDiamond(int val)
    {
        myDiamond += val;
        this.uiGoodsItems[2].textVal.text = string.Format("{0:#,0}", myDiamond);
    }

}
