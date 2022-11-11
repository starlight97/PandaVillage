using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIPlayerGold : MonoBehaviour
{
    private Text GoldText;
    public UnityAction<int> onChangeGold;

    public void Init()
    {
        this.GoldText = this.transform.Find("GoldText").GetComponent<Text>();

        onChangeGold = (val) => {
            this.GoldText.text = val.ToString();
        };
    }

}
