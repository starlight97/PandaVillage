using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo 
{
    public int id;
    public int state;   // 0 : open, 1 : complete, 2 : lock
    public int stars;

    //public StageInfo(int id, int state = 0, int stars = 0)
    //{
    //    this.id = id;
    //    this.state = state;
    //    this.stars = stars;
    //}

    public void Init(int id, int state = 0, int stars = 0)
    {
        this.id = id;
        this.state = state;
        this.stars = stars;
    }
}
