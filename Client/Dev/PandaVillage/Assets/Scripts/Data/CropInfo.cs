using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropInfo
{
    // 작물 아이디
    public int id;
    // 작물 이름
    public string name;
    // 작물 sprite 상태 변경 변수
    public int state;
    // 자라는 시간: 시간이 지남에 따라 state 증가
    public int growthTime;
    public int posX;
    public int posY;
}
