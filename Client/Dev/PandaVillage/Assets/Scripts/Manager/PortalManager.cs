using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalManager : MonoBehaviour
{
    public UnityAction<App.eSceneType, int> onArrival;

    public void Init()
    {
        int portalCount = this.transform.childCount;
        for(int index = 0; index < portalCount; index++)
        {
            var portal = this.transform.GetChild(index).GetComponent<Portal>();
            portal.onArrival = (sceneType, index) =>
            {
                this.onArrival(sceneType, index);
            };

        }

    }
}
