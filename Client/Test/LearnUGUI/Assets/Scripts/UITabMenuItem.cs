using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UITabMenuItem : MonoBehaviour
{
    private Button btn;
    public bool isFocus = false;
    public UnityAction onUpdate;
    // Start is called before the first frame update
    public void Init()
    {
        this.btn = GetComponent<Button>();

        this.btn.onClick.AddListener(() =>
        {            
            this.Focus();
            this.onUpdate();
        });
    }

    public void Focus()
    {
        if (!this.isFocus)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        this.isFocus = !isFocus;
        
    }

}
