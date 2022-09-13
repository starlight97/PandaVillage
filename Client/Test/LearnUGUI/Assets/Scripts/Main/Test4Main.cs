using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

public class Test4Main : MonoBehaviour
{
    public UIChapter uiChapter;
    public UIPageStage uiPageStage;
    public Dictionary<int, ChapterData> dicChapterDatas;
    public static Test4Main instance;
    public GameInfo gameInfo;

    private void Awake()
    {
        Test4Main.instance = this;
    }

    void Start()
    {
        // 데이터 로드
        this.LoadData();

        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        Debug.Log(path);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
        }
        else
        {
            Debug.Log("신규 유저");
            this.gameInfo = new GameInfo();
            var json = JsonConvert.SerializeObject(this.gameInfo);
            File.WriteAllText(path, json);
            Debug.Log("save game_info.json");
        }

        // ChapterInfo들
        // StageInfo들


        //return;

        // 버튼들을 초기화
        int i = 0;
        foreach (var data in this.dicChapterDatas.Values)
        {
            this.uiChapter.btns[i++].Id = data.id;
        }

        // 이벤트 구독
        foreach (var btn in this.uiChapter.btns)
        {
            btn.onClick.AddListener((id) =>
            {
                var data = this.dicChapterDatas[id];
                Debug.LogFormat("{0} {1} {2}", data.id, data.max_stage, data.name);

                this.uiPageStage.Init(data.id, data.name, data.max_stage);
                //this.uiPageStage.Init(chapterNo);
                this.uiPageStage.Show();
            });
        }

        this.uiPageStage.btnBack.onClick.AddListener(() =>
        {
            this.uiPageStage.Close();
        });


    }

    private void LoadData()
    {
        var asset = Resources.Load<TextAsset>("Datas/chapter_data");
        this.dicChapterDatas = JsonConvert.DeserializeObject<ChapterData[]>(asset.text).ToDictionary(x => x.id);
    }

}
