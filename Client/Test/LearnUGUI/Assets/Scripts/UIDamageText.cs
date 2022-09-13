using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIDamageText : MonoBehaviour
{
    public TMP_Text textDamage;
    public UIAnimationEventReciver animReceiver;

    public void Init(int damage)
    {
        this.textDamage.text = string.Format("{0}", damage);
        this.animReceiver.onAnimationComplete.AddListener(() =>
        {
            Destroy(this.gameObject);
        });
    }


}
