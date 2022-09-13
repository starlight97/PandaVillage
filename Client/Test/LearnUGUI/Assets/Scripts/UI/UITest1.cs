using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest1 : MonoBehaviour
{
    public UIGoods uiGoods;
    // Start is called before the first frame update
    void Start()
    {

        uiGoods.onClickItem.AddListener((type) =>
        {
            if (type == UIGoodsItem.eType.Gold)
            {
                uiGoods.UpdateCoin(10);
            }
            else if (type == UIGoodsItem.eType.Energy)
            {
                uiGoods.UpdateEnergy(10);

            }
            else if (type == UIGoodsItem.eType.Diamond)
            {
                uiGoods.UpdateDiamond(10);

            }
        });
        uiGoods.Init();
    }

}
