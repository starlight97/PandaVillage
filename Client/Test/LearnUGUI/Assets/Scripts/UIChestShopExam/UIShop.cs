using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace UIChestShopExample
{
    public class UIShop : MonoBehaviour
    {
        public SpriteAtlas atlas;
        public GameObject uiShopItemAdPrefab;
        public GameObject uiShopItemPrefab;
        public RectTransform content;


        public void Init()
        {
            int i = 0;
            foreach (var data in ChestShopMain.instance.dicChestDatas.Values)
            {
                Debug.LogFormat("{0} {1} {2} {3} {4} {5} ", data.id, data.name, data.price, data.sprite_name, data.width, data.height);
                GameObject itemGo = null;
                if (i == 0)
                {
                    itemGo = Instantiate(this.uiShopItemAdPrefab, this.content);
                    var itemAd = itemGo.GetComponent<UIShopItemAd>();
                    itemGo.GetComponent<UIShopItemAd>().btnAd.onClick.AddListener(() =>
                    {
                        Debug.Log("광고보기");

                    });
                }
                else
                {
                    itemGo = Instantiate(this.uiShopItemPrefab, this.content);
                }
                var item = itemGo.GetComponent<UIShopItem>();
                Sprite sp = this.atlas.GetSprite(data.sprite_name);

                item.Init(data.id, data.name, data.price, sp, data.width, data.height);

                item.btnPurchase.onClick.AddListener(() =>
                {
                    Debug.LogFormat("아이템 구매 : {0}", item.id);
                });
                i++;
            }
        }
    }
}