using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public float topMoveSpeed;
    public float middleMoveSpeed;
    public float bottomMoveSpeed;

    public GameObject[] topBackgroundGos;
    public GameObject[] middleBackgroundGos;
    public GameObject[] bottomBackgroundGos;

    public void BossKillEffect()
    {
        StartCoroutine(BossKillEffectRoutine());
    }
    private IEnumerator BossKillEffectRoutine()
    {
        topMoveSpeed *= 3;
        middleMoveSpeed *= 3;
        bottomMoveSpeed *= 3;
        float delta = 0;
        while(true)
        {
            delta += Time.deltaTime;
            if(delta >= 5f)
            {
                break;
            }
            yield return null;
        }
        topMoveSpeed /= 3;
        middleMoveSpeed /= 3;
        bottomMoveSpeed /= 3;

    }
    void Update()
    {
        for (int i = 0; i < topBackgroundGos.Length; i++)
        {
            topBackgroundGos[i].transform.Translate(Vector3.down * this.topMoveSpeed * Time.deltaTime);
            if (topBackgroundGos[i].transform.position.y < -11f)
            {
                var pos = topBackgroundGos[i].transform.position;
                pos.y = 11f;
                topBackgroundGos[i].transform.position = pos;
            }
        }
        for (int i = 0; i < middleBackgroundGos.Length; i++)
        {
            middleBackgroundGos[i].transform.Translate(Vector3.down * this.middleMoveSpeed * Time.deltaTime);
            if (middleBackgroundGos[i].transform.position.y < -11f)
            {
                var pos = middleBackgroundGos[i].transform.position;
                pos.y = 11f;
                middleBackgroundGos[i].transform.position = pos;
            }
        }
        for (int i = 0; i < bottomBackgroundGos.Length; i++)
        {
            bottomBackgroundGos[i].transform.Translate(Vector3.down * this.bottomMoveSpeed * Time.deltaTime);
            if (bottomBackgroundGos[i].transform.position.y < -11f)
            {
                var pos = bottomBackgroundGos[i].transform.position;
                pos.y = 11f;
                bottomBackgroundGos[i].transform.position = pos;
            }
        }
    }
}
