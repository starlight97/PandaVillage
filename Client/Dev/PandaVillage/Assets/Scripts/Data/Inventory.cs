using System.Collections;
using System.Collections.Generic;


public class Inventory 
{
    // 인벤토리 사이즈
    public int size;
    // key : idx , value : itemId , amount 
    public Dictionary<int, InventoryData> dicItem;

    public Inventory(int size)
    {
        this.size = size;

        dicItem = new Dictionary<int, InventoryData>();
        for (int idx = 0; idx < size; idx++)
        {
            dicItem.Add(idx, null);
        }
    }


    //아이템이 들어갔으면 true 안들어갔으면 false 반환
    public bool AddItem(int addItemID, int addAmount)
    {
        int itemKey = GetItemIndex(addItemID);

        if (itemKey == -1)
        {
            foreach (var item in dicItem)
            {
                if (item.Value == null)
                {
                    dicItem[item.Key] = new InventoryData(addItemID, addAmount);
                    return true;
                }
            }
        }
        else
        {
            dicItem[itemKey].amount += addAmount;
            return true;
        }

        return false;

    }

    //아이템 아이디를 입력해서 아이템이 있으면 키값을 반환하고 없으면 -1을 반환
    private int GetItemIndex(int ItemID)
    {
        foreach (var item in dicItem)
        {
            if (item.Value != null && item.Value.itemId == ItemID)
                return item.Key;
        }

        return -1;
    }

}