using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
namespace UIGoldShopExample
{
    public class UIGoldShop : MonoBehaviour
    {
        public SpriteAtlas atlas;
        public GameObject uiGoldShopItemPrefab;
        public RectTransform content;

        public void Init()
        {
            int i = 0;
            foreach (var data in GoldShopMain.instance.dicGoldShopDatas.Values)
            {
                Debug.LogFormat("{0} {1} {2} {3} {4}", data.id, data.name, data.price, data.sprite_name, data.dollar);
                GameObject itemGo = Instantiate(this.uiGoldShopItemPrefab, this.content);
                var item = itemGo.GetComponent<UIGoldShopItem>();
                Sprite sp = this.atlas.GetSprite(data.sprite_name);

                item.Init(data.id, data.name, data.price, data.dollar, sp);

                item.btnPurchase.onClick.AddListener(() =>
                {
                    Debug.LogFormat("아이템 구매 : {0}", item.id);
                });
                i++;
            }
        }
    }
}