using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPageStage : MonoBehaviour
{
    public const int MAX_PANEL_COUNT = 18;

    public Button btnBack;
    public Button btnPrev;
    public Button btnNext;
    public TMP_Text textChapterName;
    public UIPageStageItem[] arrUiPageStageItems;

    private int currentPage;
    private int maxStage;
    private int totalPage;
    private int chapterId;
    private int startStage;
    private int endStage;

    public void Init(int chapterId, string chapterName, int maxStage)
    {
        this.chapterId = chapterId;
        this.textChapterName.text = chapterName;
        this.maxStage = maxStage;
        currentPage = 1;
        Debug.LogFormat("maxStage: {0}", maxStage);
        this.totalPage = Mathf.CeilToInt((float)maxStage / MAX_PANEL_COUNT);
        Debug.LogFormat("totalPage: {0}", this.totalPage);

        this.startStage = 1 + 18 * (this.currentPage - 1);

        this.endStage = 0;

        if (totalPage > 1)
        {
            endStage = (startStage + MAX_PANEL_COUNT) - 1;
        }
        else
        {
            endStage = this.maxStage;
        }

        if (this.totalPage > 1)
        {
            this.btnNext.gameObject.SetActive(true);
        }

        this.btnPrev.gameObject.SetActive(false);

        Debug.LogFormat("{0} ~ {1}", startStage, endStage);
        this.btnNext.onClick.AddListener(() => 
        {
            this.Next();
        });

        this.btnPrev.onClick.AddListener(() => 
        {
            this.Prev();
        });
        
        UpdateUIPageStateItem();

    }

    private void HideUIStageItems()
    {
        foreach (var item in this.arrUiPageStageItems)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void UpdateUIPageStateItem()
    {
        int uiIdx = 0;
        Debug.Log("ENDSTAGE : " + endStage);
        // show items
        for (int i = startStage; i <= endStage; i++)
        {
            var chapterInfo = Test4Main.instance.gameInfo.chapterInfos.Find(x => x.Id == this.chapterId);
            var idx = i - 1;
            var info = chapterInfo.stageInfos[idx];
            
            Debug.LogFormat("id : {0}, state : {1}, stars : {2}", info.id, info.state, info.stars);
            var item = this.arrUiPageStageItems[uiIdx++];
            item.Init(info.id+1, info.state, info.stars);
            item.Show();
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        //start page no :  1 + 18 * (this.currentPageNo -1)
        //current page no : 1   ---->   1 ~ 18
        //current page no : 2   ---->   19 ~ 36
        //current page no : 3   ---->   37 ~ 54
        //current page no : 4   ---->   55
    }

    public void Close()
    {
        this.gameObject.SetActive(false);

        this.btnPrev.onClick.RemoveAllListeners();
        this.btnNext.onClick.RemoveAllListeners();
    }

    public void Next()
    {
        this.currentPage++;
        this.startStage = 1 + 18 * (this.currentPage - 1);

        this.endStage = 0;

        if (currentPage < this.totalPage)
        {
            this.endStage = (startStage + MAX_PANEL_COUNT) - 1;
        }
        else
        {
            this.endStage = this.maxStage;
            //next 버튼을 hide
            this.btnNext.gameObject.SetActive(false);
        }

        if (currentPage > 1)
        {
            //prev 버튼을 show 
            this.btnPrev.gameObject.SetActive(true);
        }

        Debug.LogFormat("page: {0}, {1} ~ {2}", this.currentPage, startStage, endStage);

        //for (int i = startStage; i <= endStage; i++)
        //{
        //    var chapterData = Test4Main.instance.gameInfo.chapterInfos.Find(x => x.Id == this.chapterId);
        //    var idx = i - 1;
        //    var info = chapterData.stageInfos[idx];

        //    Debug.LogFormat("id : {0}, state : {1}, stars : {2}", info.id, info.state, info.stars);
        //}
        HideUIStageItems();
        UpdateUIPageStateItem();
    }

    public void Prev()
    {
        this.currentPage--;
        this.startStage = 1 + 18 * (this.currentPage - 1);

        this.endStage = 0;

        this.endStage = (startStage + MAX_PANEL_COUNT) - 1;

        if (currentPage <= 1)
        {
            //prev 버튼을 hide
            this.btnPrev.gameObject.SetActive(false);
        }

        if (currentPage >= this.totalPage)
        {
            this.btnNext.gameObject.SetActive(false);
        }
        else
        {
            this.btnNext.gameObject.SetActive(true);
        }
        Debug.LogFormat("currentPage : {0} / totalPage : {1}", this.currentPage, this.totalPage);

        Debug.LogFormat("page: {0}, {1} ~ {2}", this.currentPage, startStage, this.endStage);

        //for (int i = startStage; i <= this.endStage; i++)
        //{
        //    var chapterData = Test4Main.instance.gameInfo.chapterInfos.Find(x => x.Id == this.chapterId);
        //    var idx = i - 1;
        //    var info = chapterData.stageInfos[idx];

        //    Debug.LogFormat("id : {0}, state : {1}, stars : {2}", info.id, info.state, info.stars);

        //}
        HideUIStageItems();
        UpdateUIPageStateItem();
    }

}