using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // 게임 내 시간
    // 실제 시간
    public int hour;    // 시
    public int min;     // 분

    private void Update()
    {
        
    }

}

//오전 6시부터 시작되어 새벽 2시에 하루가 끝난다.
//실제 시간 7초가 지나면 10분씩 증가한다.
//새벽 2시가 되면 게임 진행 상황을 저장하고 다음날 오전 6시가 된다. => 실제 시간을 0초로 변경함
//사용자가 하루를 일찍 종료할 수 있다. : 다음날 오전 6시가 된다. -> 실제 시간을 0초로 변경함