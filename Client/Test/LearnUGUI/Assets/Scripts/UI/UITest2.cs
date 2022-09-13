using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest2 : MonoBehaviour
{
    public HpGuage hpGauge;
    public GameObject uiDamageTextPrefab;
    public Canvas canvas;
    public Camera uiCam;

    public void CreateDamageText(Vector3 worldPos , int damage)
    {
        var go = Instantiate(this.uiDamageTextPrefab, canvas.transform);
        var screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)this.canvas.transform, screenPos, this.uiCam, out localPos);
        go.GetComponent<RectTransform>().localPosition = localPos;

        var uiDamageText = go.GetComponent<UIDamageText>();
        uiDamageText.Init(damage);
    }
}
