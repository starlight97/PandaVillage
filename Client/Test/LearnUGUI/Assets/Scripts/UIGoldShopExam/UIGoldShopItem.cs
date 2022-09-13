
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIGoldShopExample
{
    public class UIGoldShopItem : MonoBehaviour
    {
        public TMP_Text textName;
        public TMP_Text textPrice;
        public TMP_Text textDollar;
        public Button btnPurchase;
        public Image icon;
        public int id;

        public void Init(int id, string name, int price, string dollar, Sprite sp)
        {
            this.id = id;
            this.textName.text = name;
            this.textPrice.text = string.Format("{0:#,###} Gold", price); // {0:#,0}
            this.textDollar.text = dollar;
            this.icon.sprite = sp;
            this.icon.SetNativeSize();

        }
    }
}