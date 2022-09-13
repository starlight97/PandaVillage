using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDailyMain : MonoBehaviour
{
    public UIRewardDaily uiRewardDaily;
    public int currentDay;
    // Start is called before the first frame update
    void Start()
    {
        this.LoadData();

        // info load complete
        InfoManager.instance.onDataLoadFinished.AddListener(() =>
        {
            uiRewardDaily.Init(currentDay);
        });

    }


    public void LoadData()
    {
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);

        // data load complete
        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            bool check = InfoManager.instance.LoadData<DailyrewardInfo>("daily_info");
            if (!check)
            {
                Debug.Log("신규 유저");
                var datas = DataManager.instance.GetDataList<DailyrewardData>();
                foreach (var data in datas)
                {
                    var info = new DailyrewardInfo(data.id);
                    InfoManager.instance.InsertInfo(info);
                }
                InfoManager.instance.SaveInfo<DailyrewardInfo>("daily_info");
                uiRewardDaily.Init(currentDay);
            }
        });
    }
}
