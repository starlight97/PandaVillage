using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    public int index;
    public bool isClickPortal;
    public UnityAction<App.eSceneType, int> onArrival;
    public App.eSceneType sceneType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.onArrival(sceneType, index);
    }

    public void ClickPotal()
    {
        if(isClickPortal == true)
            this.onArrival(sceneType, index);
    }

}
