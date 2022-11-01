using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameMain : SceneMain
{
    private UILoadGame uILoadGame;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uILoadGame = GameObject.FindObjectOfType<UILoadGame>();

        uILoadGame.Init();

        uILoadGame.onSelectedPlayerId = (selectedId) =>
        {
            InfoManager.instance.Init(selectedId);
            Dispatch("onLoadGame");
        };
        uILoadGame.onExitButtonClick = () => {
            Dispatch("onExitBtnClick");
        };
    }
}
