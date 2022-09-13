using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIStar : MonoBehaviour
{
    public Animator anim;
    public UIAnimationEventReciver animReceiver;
    public System.Action<float> onUpdateAnimation;


    public UnityEvent Init()
    {
        this.anim.speed = 0;

        return this.animReceiver.onAnimationComplete;
    }

    public void Play()
    {
        this.anim.speed = 1;
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        while (true)
        {
            var norm = this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if(norm >= 0.3f)
            {
                this.onUpdateAnimation(norm);
                break;
            }
            yield return null;
        }
    }

    public bool IsPlaying()
    {
        return this.anim.speed > 0;
    }
}
