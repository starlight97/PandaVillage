using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGoodsItem : MonoBehaviour
{
    public enum eType
    {
        Gold, Energy, Diamond
    }
    public TMP_Text textVal;

    public eType type;
    public Button btn;

    public UnityAction<eType> onClick;


    public void Init()
    {
        this.btn.onClick.AddListener(() => 
        {
            this.onClick(this.type);
        });
    }

}
