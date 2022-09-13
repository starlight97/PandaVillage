using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

namespace UIMissionExample
{
    public class UIMission : MonoBehaviour
    {
        public SpriteAtlas atlas;
        public GameObject uiMissionItemPrefab;
        public RectTransform content;
        public Button btnBack;

        public void Init()
        {
            int i = 0;
            var missionDataList = DataManager.instance.GetDataList<MissionData>();

            foreach (var missionData in missionDataList)
            {
                //Debug.LogFormat("{0} {1} {2} {3} {4}", data.id, data.name, data.price, data.sprite_name, data.dollar);
                GameObject itemGo = Instantiate(this.uiMissionItemPrefab, this.content);
                var item = itemGo.GetComponent<UIMissionItem>();
                Sprite spMission = this.atlas.GetSprite(missionData.icon_sprite_name);
                var rewardgroupData = DataManager.instance.GetData<RewardgroupData>(missionData.reward_group_id);
                var itemData = DataManager.instance.GetData<ItemData>(rewardgroupData.item_id);
                Sprite spItem = this.atlas.GetSprite(itemData.icon_sprite_name);

                //var missionInfo = InfoManager.instance.dicmissionInfos[missionData.id];
                var missionInfo = InfoManager.instance.GetInfo<MissionInfo>(missionData.id);
                item.Init(missionData.id, missionData.title, missionData.subtitle, missionData.goal, spMission, missionInfo.progress,
                     missionInfo.state, spItem, rewardgroupData.amount);

                item.btnComplete.onClick.AddListener(() =>
                {
                    //InfoManager.instance.dicmissionInfos[item.id].state = 2;
                    var info = InfoManager.instance.GetInfo<MissionInfo>(item.id);
                    info.state = 2;
                    InfoManager.instance.UpdateInfo(info);
                    InfoManager.instance.SaveInfo<MissionInfo>("mission_info");

                    item.UpdateState(2);
                    Debug.Log("보상수령");
                    //InfoManager.instance.SaveMissionInfo();
                });
                i++;
            }
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}