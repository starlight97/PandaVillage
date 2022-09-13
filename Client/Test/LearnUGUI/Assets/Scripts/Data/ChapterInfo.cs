using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterInfo
{
    public List<StageInfo> stageInfos = new List<StageInfo>();
    public int Id { get; set; }
    public ChapterInfo()
    {
        //this.Id = id;
        //this.stageInfos = new List<StageInfo>();

        //var data = Test4Main.instance.dicChapterDatas[id];
        //for (int i = 0; i < data.max_stage; i++)
        //{
        //    if(i > 0)
        //    {
        //        // lock
        //        var info = new StageInfo();
        //        info.Init(i, 0, 0);
        //        this.stageInfos.Add(info);
        //        //this.stageInfos.Add(new StageInfo(i, 2));
        //    }
        //    else
        //    {
        //        // open
        //        var info = new StageInfo();
        //        info.Init(i, 2, 0);
        //        this.stageInfos.Add(info);
        //        //this.stageInfos.Add(new StageInfo(i));

        //    }
        //}
    }

    public void Init(int id)
    {
        this.Id = id;
        this.stageInfos = new List<StageInfo>();

        var data = Test4Main.instance.dicChapterDatas[id];
        for (int i = 0; i < data.max_stage; i++)
        {
            if (i > 0)
            {
                // lock
                var info = new StageInfo();
                info.Init(i, 0, 0);
                this.stageInfos.Add(info);
                //this.stageInfos.Add(new StageInfo(i, 2));
            }
            else
            {
                // open
                var info = new StageInfo();
                info.Init(i, 2, 0);
                this.stageInfos.Add(info);
                //this.stageInfos.Add(new StageInfo(i));

            }
        }
    }
}
