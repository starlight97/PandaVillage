using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIMissionExample
{
    public class UIMissionItem : MonoBehaviour
    {
        public GameObject doing;
        public GameObject complete;
        public GameObject completeCheck;

        public int id;
        public TMP_Text textTitle;
        public TMP_Text textSubTitle;
        public Image iconMission;

        public TMP_Text textProgress;
        public Slider sliderProgress;
        public float progress;
        public int state;

        public Image iconreward;
        public TMP_Text textRewardAmount;
        public Button btnComplete;

        public void Init(int id, string title, string subTitle, int goal, Sprite spMission,
            int progress, int state, Sprite spItem, int rewardAmount)
        {
            // string.Format(data.name, data.goal);
            this.id = id;
            this.textTitle.text = title;
            this.textSubTitle.text = string.Format(subTitle, goal);
            //this.textSubTitle.text = string.Format("{0:#,###}", price); // {0:#,0}
            this.iconMission.sprite = spMission;
            this.iconMission.SetNativeSize();

            // progress
            this.progress = (float)progress / (float)goal;
            this.state = state;
            this.textProgress.text = progress.ToString() + " / " + goal.ToString();
            this.sliderProgress.value = this.progress;

            this.iconreward.sprite = spItem;
            this.iconreward.SetNativeSize();
            this.textRewardAmount.text = rewardAmount.ToString();

            this.UpdateState(this.state);
        }

        public void UpdateState(int state)
        {
            if (state == 1)
            {
                doing.SetActive(false);
                complete.SetActive(true);
            }
            else if (state == 2)
            {
                doing.SetActive(false);
                complete.SetActive(false);
                completeCheck.SetActive(true);
            }
        }
    }
}