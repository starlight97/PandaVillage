using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class UISlider : MonoBehaviour
{
    public Slider slider;
    public UnityAction<float> onValueChanged;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogFormat("onValueChanged {0}", onValueChanged);
        
    }

    public void Init()
    {
        this.slider.onValueChanged.AddListener(onValueChanged);
    }

    public void SetValue(float value)
    {
        this.slider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
