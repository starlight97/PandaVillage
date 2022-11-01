using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFarmEdit : MonoBehaviour
{
    public UnityAction onCLickBtnOkay;
    public UnityAction onCLickBtnCancel;
    private GameObject buttons;
    private Button btnOk;
    private Button btnCancel;
    public void Init()
    {
        this.buttons = transform.Find("Buttons").gameObject;
        this.btnOk = buttons.transform.Find("BtnOk").GetComponent<Button>();
        this.btnCancel = buttons.transform.Find("BtnCancel").GetComponent<Button>();
        this.btnOk.onClick.AddListener(() =>
        {
            this.onCLickBtnOkay();
        });
        this.btnCancel.onClick.AddListener(() =>
        {
            this.onCLickBtnCancel();
        });

        this.HideBtnOk();
    }

    public void ShowBtnOk()
    {
        this.btnOk.gameObject.SetActive(true);
    }
    public void HideBtnOk()
    {
        this.btnOk.gameObject.SetActive(false);
    }
}
