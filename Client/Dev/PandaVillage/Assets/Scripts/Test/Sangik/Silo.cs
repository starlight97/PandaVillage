using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silo : TransparentObject
{
    public void Init(int posX, int posY)
    {
        this.transform.position = new Vector2(posX, posY);
    }
}
