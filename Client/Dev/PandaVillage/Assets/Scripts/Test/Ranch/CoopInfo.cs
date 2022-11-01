using System.Collections;
using System.Collections.Generic;

public class CoopInfo 
{  
    public int coopId;
    public List<AnimalInfo> animalinfoList;
    public int posX;
    public int posY;

    public CoopInfo(int coopId, int posX, int posY) 
    {
        this.coopId = coopId;
        this.animalinfoList = new List<AnimalInfo>();
        this.posX = posX;
        this.posY = posY;
    }
}
