using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public class UIRewardDaily : MonoBehaviour
{
    public GameObject rewardItemprefab;
    public Button btnCliam;
    public RectTransform grid;
    public SpriteAtlas atlas;

    private RewardItem targetRewardItem;
    public void Init(int currentDay)
    {
        this.btnCliam.gameObject.SetActive(false);
        var datas = DataManager.instance.GetDataList<DailyrewardData>().ToList();
        

        foreach (var data in datas)
        {
            var itemGo = Instantiate(this.rewardItemprefab, this.grid);
            var item = itemGo.GetComponent<RewardItem>();

            var rewardItem = DataManager.instance.GetData<RewarditemData>(data.item_id);
            Sprite icon = this.atlas.GetSprite(rewardItem.sprite_name);
            var info = InfoManager.instance.GetInfo<DailyrewardInfo>(data.id);
            int state = info.state;
            if(data.day == currentDay && info.state == 0)
            {
                state = 1;
                targetRewardItem = item;
                this.btnCliam.gameObject.SetActive(true);
            }
            item.Init(data.id, data.day, icon, state, data.amount);
            item.btn.onClick.AddListener(() =>
            {
                //Debug.Log(item.textDay.text + " : click");
            });
        }

        this.btnCliam.onClick.AddListener(() =>
        {
            var info = InfoManager.instance.GetInfo<DailyrewardInfo>(targetRewardItem.id);
            info.state = 2;
            InfoManager.instance.UpdateInfo(info);
            InfoManager.instance.SaveInfo<DailyrewardInfo>("daily_info");

            targetRewardItem.state = 2;
            targetRewardItem.UpdateState(2);
            this.btnCliam.gameObject.SetActive(false);
        });

    }

}
