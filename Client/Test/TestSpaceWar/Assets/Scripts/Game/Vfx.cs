using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vfx : MonoBehaviour
{
    //public float length;
    private Animator anim;
    public UnityAction onExplosionComplet;

    //private float delta = 0;

    private void Start()
    {
        this.anim = GetComponent<Animator>();

    }

    private void Update()
    {
        //this.delta += Time.deltaTime;
        //if (this.delta >= this.length)
        //{
        //    //destory
        //    Destroy(this.gameObject);
        //}

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (this.onExplosionComplet != null) CompleteExplosion();
            Destroy(this.gameObject);
        }

    }

    public void CompleteExplosion()
    {
        this.onExplosionComplet();
    }
}
