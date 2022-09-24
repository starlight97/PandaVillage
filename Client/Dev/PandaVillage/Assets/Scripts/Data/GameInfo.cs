using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameInfo
{
    public int playerId;
    public List<ObjectInfo> objectInfos;


    public GameInfo()
    {
        this.objectInfos = new List<ObjectInfo>();

        //var datas = this.objectInfos.Where(x => x.playerId == 1);



    }
}
