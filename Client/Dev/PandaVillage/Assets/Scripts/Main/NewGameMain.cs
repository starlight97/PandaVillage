using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameMain : SceneMain
{
    private UINewGame uINewGame;
    public override void Init(SceneParams param = null)
    {
        base.Init();
        uINewGame = GameObject.FindObjectOfType<UINewGame>();
        uINewGame.Init();

        uINewGame.onClickButton = (gameinfo) =>
        {
            this.CreateUser(gameinfo);
        };
        uINewGame.onExitButtonClick = () => {
            Dispatch("onExitBtnClick");
        };

    }


    private void CreateUser(GameInfo gameinfo)
    {       
        InfoManager.instance.InsertInfo(gameinfo);
        InfoManager.instance.Init(gameinfo.playerId);
        Dispatch("onCreateUser");
    }


}
