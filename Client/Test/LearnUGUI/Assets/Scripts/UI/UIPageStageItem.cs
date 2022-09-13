using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPageStageItem : MonoBehaviour
{
    // 0 : open, 1 : complete, 2 : lock
    public GameObject[] arrBgs;
    public GameObject[] arrStars;
    public TMP_Text textStageNo;


    public void Init(int stageNo, int state, int stars)
    {
        this.textStageNo.text = stageNo.ToString();
        this.UpdateState(state);
        this.UpdateStars(stars);
             
    }

    private void UpdateState(int state)
    {
        foreach (var bg in this.arrBgs)
        {
            bg.SetActive(false);
        }
        this.arrBgs[state].gameObject.SetActive(true);

        this.textStageNo.gameObject.SetActive(state != 2);

    }
    private void UpdateStars(int stars)
    {
        Debug.Log("stars : " + stars);
        foreach (var star in this.arrStars)
        {
            star.gameObject.SetActive(false);
        }

        for (int i = 0; i < stars; i++)
        {
            this.arrStars[i].SetActive(true);
        }
        
    }

    public void Show()
    {
        this.gameObject.SetActive(true);

    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
