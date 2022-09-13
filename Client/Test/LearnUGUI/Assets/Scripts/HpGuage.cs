using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpGuage : MonoBehaviour
{
    private RectTransform rectTrans;
    public Camera uiCam;
    public RectTransform canvasRectTrans;
    private void Awake()
    {
        this.rectTrans = this.GetComponent<RectTransform>();
    }
    public void UpdatePosition(Vector3 tWorldPosition)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, tWorldPosition);
        Vector2 localPos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTrans, screenPos, this.uiCam, out localPos))
        {
            this.rectTrans.localPosition = localPos;
        }

        //this.rectTrans.position = screenPos;

    }
}
