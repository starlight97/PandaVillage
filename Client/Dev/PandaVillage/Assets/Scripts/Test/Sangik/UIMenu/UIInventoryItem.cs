using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventoryItem : MonoBehaviour
{
    private Image itemSprite;
    private Text itemAmount;

    private void Start()
    {
        this.itemSprite = this.transform.Find("itemSprite").GetComponent<Image>();
        this.itemAmount = this.transform.Find("itemAmount").GetComponent<Text>();
    }

    public void Init(Sprite sp, int amount)
    {
        this.itemSprite.sprite = sp;
        this.itemAmount.text = amount.ToString();
    }
   
}
