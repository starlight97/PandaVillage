using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    // 게임 내 시간
    // 실제 시간
    private int day;    // 일
    private int hour;    // 시
    private int minute;     // 분
    private float currentTime;

    public UnityAction<int, int> onUpdateTime;

    public void Init()
    {
        currentTime = 0;

        StartCoroutine(TimeRoutine());
    }

    private IEnumerator TimeRoutine()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 7f)
            {                
                minute += 10;
                currentTime = 0;

                if (minute == 60)
                {
                    minute = 0;
                    hour += 1;
                }
                Debug.LogFormat("hour : {0} minute : {1}", hour, minute);
                this.onUpdateTime(hour, minute);
            }


            yield return null;
        }
    }

    public void EndDay()
    {
        hour = 0;
        minute = 0;
    }

}

//오전 6시부터 시작되어 새벽 2시에 하루가 끝난다.
//실제 시간 7초가 지나면 10분씩 증가한다.
//새벽 2시가 되면 게임 진행 상황을 저장하고 다음날 오전 6시가 된다. => 실제 시간을 0초로 변경함
//사용자가 하루를 일찍 종료할 수 있다. : 다음날 오전 6시가 된다. -> 실제 시간을 0초로 변경함