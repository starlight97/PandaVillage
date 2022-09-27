using System.Collections;
using System.Collections.Generic;


public class Inventory 
{
    // 인벤토리 사이즈
    public int size;
    // key : itemId, value : amount
    public Dictionary<int, int>dicItem;

    public Inventory(int size)
    {
        this.size = size;
        dicItem = new Dictionary<int, int>();
    }

}
