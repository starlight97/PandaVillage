using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanchInfo
{
    public List<CoopInfo> coopInfoList;

    public int hay;
    public int houseId;

    public RanchInfo()
    {
        this.coopInfoList = new List<CoopInfo>();    
        this.hay = 0;
        this.houseId = 8999;
    }
}
