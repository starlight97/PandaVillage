using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIChapterButton : MonoBehaviour, IPointerClickHandler, Iidentifiable
{
    public UnityEvent<int> onClick = new UnityEvent<int>();

    public int Id { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        this.onClick.Invoke(this.Id);
    }


}
