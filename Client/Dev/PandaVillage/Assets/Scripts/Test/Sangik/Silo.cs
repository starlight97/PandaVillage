using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Silo : Building
{
    public int hay; //건초
    public readonly int maxHay = 120;
    private GameObject canvasGo;
    private Button closeButton;
    public void Init()
    {
        // base.FindCollider();
        // StartCoroutine(base.ColliderRoutine());

        this.canvasGo = this.transform.Find("Canvas").gameObject;
        this.closeButton = canvasGo.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() => {
            ShowUI();
            this.canvasGo.SetActive(false);
        });

    }

    public void ShowUI()
    {
        Debug.LogFormat("건초의양 : {0} / {1}",hay, maxHay);
    }
}
