using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public List<ChapterInfo> chapterInfos;

    public GameInfo()
    {
        //this.chapterInfos = new List<ChapterInfo>();

        //foreach (var data in Test4Main.instance.dicChapterDatas)
        //{
        //    this.chapterInfos.Add(new ChapterInfo(data.Value.id));
        //}
    }

    public void Init()
    {
        this.chapterInfos = new List<ChapterInfo>();

        foreach (var data in Test4Main.instance.dicChapterDatas.Values)
        {
            var info = new ChapterInfo();
            info.Init(data.id);
            this.chapterInfos.Add(info);

            //this.chapterInfos.Add(new ChapterInfo(data.Value.id));
        }
    }


}
