using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button btn;   

    void Start()
    {
        btn = this.gameObject.GetComponent<Button>();

        this.btn.onClick.AddListener(() =>
        {
            Debug.Log("Å¬¸¯");
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.DOScale(1.2f,0.5f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");       
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.DOScale(1, 0.5f);
    }
}
