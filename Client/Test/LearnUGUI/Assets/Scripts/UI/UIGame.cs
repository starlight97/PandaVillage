using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public UIOnOffBtn uIOnOffBtn;
    public UISlider uiSlider;
    // Start is called before the first frame update
    void Start()
    {
       
        this.uIOnOffBtn.onUpdate = (isOn) =>
        {
            Debug.Log(isOn);
        };
        this.uiSlider.onValueChanged = (value) =>
        {
            this.uiSlider.SetValue(value);
            Debug.Log("UIGAME : " + value);
        };

        this.uiSlider.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
