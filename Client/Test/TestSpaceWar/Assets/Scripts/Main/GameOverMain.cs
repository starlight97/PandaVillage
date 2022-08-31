using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMain : SceneMain
{
    private Image dim;

    public override void Init(SceneParams param = null)
    {
        StartCoroutine(this.WaitForClick());

    }


    private IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        this.StopAllCoroutines();

        this.Dispatch("onClick");
    }
}
