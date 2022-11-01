using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMain : SceneMain
{
    public Button btnNewGame;
    public Button btnLoadGame;
    public override void Init(SceneParams param = null)
    {
        this.useOnDestoryEvent = false;
        btnNewGame.onClick.AddListener(() => 
        {
            Dispatch("onClickNewGame");
        
        });

        btnLoadGame.onClick.AddListener(() =>
        {
            Dispatch("onClickLoadGame");

        });

    }


}
