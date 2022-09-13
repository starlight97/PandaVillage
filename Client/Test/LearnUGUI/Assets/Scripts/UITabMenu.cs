using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITabMenu : MonoBehaviour
{
    public UITabMenuItem[] uiTabMenuItems;
  

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in uiTabMenuItems)
        {
            item.Init();
            item.onUpdate = () =>
            {
                foreach (var item2 in uiTabMenuItems)
                {
                    item2.Focus();
                }
            };
        }
        uiTabMenuItems[0].Focus();
    }

}
