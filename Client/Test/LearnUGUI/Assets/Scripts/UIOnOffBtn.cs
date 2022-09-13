using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIOnOffBtn : MonoBehaviour
{
    public GameObject onGo;
    public GameObject offGo;

    private Button btn;
    private bool isOn = false;
    public UnityAction<bool> onUpdate;
    void Start()
    {
        this.btn = GetComponent<Button>();

        this.btn.onClick.AddListener(() =>
        {
            this.isOn = !this.isOn;
            if(this.isOn)
            {
                offGo.SetActive(true);
                onGo.SetActive(false);
                this.onUpdate(this.isOn);
            }
            else
            {
                offGo.SetActive(false);
                onGo.SetActive(true);
                this.onUpdate(this.isOn);
            }
        });
    }

}
