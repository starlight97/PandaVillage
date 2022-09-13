using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardItem : MonoBehaviour
{
    public int id;
    public TMP_Text textDay;
    public Image rewardIcon;

    public GameObject complete;
    public GameObject completeCheck;
    public Image completeCheckRewardIcon;

    public TMP_Text textRewardAmount;
    public Button btn;

    public int state;
    public void Init(int id, int day, Sprite rewardIconsp, int state, int rewardAmount)
    {
        this.id = id;
        this.textDay.text = day.ToString();
        this.rewardIcon.sprite = rewardIconsp;
        this.rewardIcon.SetNativeSize();

        this.state = state;

        this.completeCheckRewardIcon.sprite = rewardIconsp;
        this.completeCheckRewardIcon.SetNativeSize();
        this.textRewardAmount.text = rewardAmount.ToString();

        this.UpdateState(this.state);
    }
    public void UpdateState(int state)
    {
        if(state == 1)
        {
            complete.SetActive(true);
            completeCheck.SetActive(false);
        }
        else if(state == 2)
        {
            complete.SetActive(false);
            completeCheck.SetActive(true);
        }
    }
}
