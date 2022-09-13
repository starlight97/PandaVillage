using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UIAnimationEventReciver : MonoBehaviour
{
    public UnityEvent onAnimationComplete = new UnityEvent();
    public void OnAnimationComplete()
    {
        //Debug.Log("animation complete!");
        this.onAnimationComplete.Invoke();
    }

}
