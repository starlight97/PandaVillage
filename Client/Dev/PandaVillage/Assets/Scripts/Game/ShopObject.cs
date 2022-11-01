using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopObject : MonoBehaviour
{
    public UnityAction onShowShopUI;

    public void ShowShop()
    {
        this.onShowShopUI();
    }

}
