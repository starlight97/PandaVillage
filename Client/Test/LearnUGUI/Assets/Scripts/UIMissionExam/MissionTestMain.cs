using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace UIMissionExample
{

    public class MissionTestMain : MonoBehaviour
    {
        public UIMission uiMission;
        public UIHome uiMissionHome;
        // Start is called before the first frame update
        void Start()
        {
            this.LoadData();
            uiMissionHome.btn.gameObject.SetActive(false);
            uiMissionHome.btn.onClick.AddListener(() =>
            {
                uiMission.Init();
                uiMission.Show();
            });

            this.uiMission.btnBack.onClick.AddListener(() =>
            {
                this.uiMission.Close();
            });



        }


        private void LoadData()
        {
            DataManager.instance.Init();
            DataManager.instance.LoadAllData(this);

            // data load complete
            DataManager.instance.onDataLoadFinished.AddListener(() =>
            {
                bool check = InfoManager.instance.LoadData<MissionInfo>("mission_info");

                if (!check)
                {
                    Debug.Log("신규 유저");
                    var datas = DataManager.instance.GetDataList<MissionData>();
                    foreach (var data in datas)
                    {
                        var info = new MissionInfo(data.id);
                        InfoManager.instance.InsertInfo(info);
                    }
                    InfoManager.instance.SaveInfo<MissionInfo>("mission_info");
                }
            });

            // info load complete
            InfoManager.instance.onDataLoadFinished.AddListener(() =>
            {
                uiMissionHome.btn.gameObject.SetActive(true);
            });
        }
    }
}