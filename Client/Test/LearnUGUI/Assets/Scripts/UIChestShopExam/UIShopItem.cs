using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace UIChestShopExample
{
    public class UIShopItem : MonoBehaviour
    {
        public TMP_Text textName;
        public TMP_Text textPrice;
        public Button btnPurchase;
        public Image icon;
        public int id;

        public void Init(int id, string name, int price, Sprite sp, int width, int height)
        {
            this.id = id;
            this.textName.text = name;
            this.textPrice.text = string.Format("{0:#,###}", price); // {0:#,0}
            this.icon.sprite = sp;
            this.icon.SetNativeSize();
            this.icon.rectTransform.sizeDelta = new Vector2(width, height);
        }
    }
}