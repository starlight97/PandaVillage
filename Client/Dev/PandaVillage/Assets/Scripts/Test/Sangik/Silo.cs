using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : Building
{
    public int hay;
   public void Init()
    {
        base.FindCollider();
        StartCoroutine(base.ColliderRoutine());
    }
}
