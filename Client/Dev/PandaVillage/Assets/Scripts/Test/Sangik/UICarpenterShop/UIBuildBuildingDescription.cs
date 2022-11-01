using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBuildBuildingDescription : MonoBehaviour
{
    private Text requireGoldText;
    private Text requireWoodText;
    private Text requireStoneText;

    public void Init()
    {
        this.requireGoldText = this.transform.Find("requireGoldText").GetComponent<Text>();
        this.requireWoodText = this.transform.Find("requireWoodText").GetComponent<Text>();
        this.requireStoneText = this.transform.Find("requireStoneText").GetComponent<Text>();
    }

    public void setDesc(int gold, int wood, int stone)
    {
        requireGoldText.text = string.Format("{0} 골드", gold);
        requireWoodText.text = string.Format("{0} 나무", wood);
        requireStoneText.text = string.Format("{0} 돌", stone);
    }
}

