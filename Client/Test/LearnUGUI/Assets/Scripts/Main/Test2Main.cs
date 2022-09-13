using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2Main : MonoBehaviour
{
    public UITest2 uiTest2;
    public Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        this.monster.onHit = (damage) =>
        {
            Debug.Log(damage);

            this.uiTest2.CreateDamageText(this.monster.damageTextPoint.position, damage);
        };
        this.monster.onUpdateMove = (tWorldPos) =>
        {
            this.uiTest2.hpGauge.UpdatePosition(tWorldPos);
        };
    }

}
