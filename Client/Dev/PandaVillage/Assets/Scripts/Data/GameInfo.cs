using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GameInfo
{
    public int playerId;
    public bool isNewbie;
    public List<ObjectInfo> objectInfoList;
    public Dictionary<App.eMapType, bool> dicVisited;
    public PlayerInfo playerInfo;


    public GameInfo(int id, string name, bool isNewbie)
    {
        this.playerId = id;
        this.isNewbie = isNewbie;
        this.dicVisited = new Dictionary<App.eMapType, bool>();
        this.objectInfoList = new List<ObjectInfo>();
        this.playerInfo = new PlayerInfo(name, "강아지");

        dicVisited.Add(App.eMapType.Alley, false);
        dicVisited.Add(App.eMapType.MountainRange, false);
        dicVisited.Add(App.eMapType.Farm, false);
        dicVisited.Add(App.eMapType.BusStop, false);
        dicVisited.Add(App.eMapType.PandaVillage, false);
        dicVisited.Add(App.eMapType.CindersapForest, false);
        dicVisited.Add(App.eMapType.SecretForest, false);
    }
}
