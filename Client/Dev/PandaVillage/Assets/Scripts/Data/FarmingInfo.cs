using System.Collections;
using System.Collections.Generic;

public class FarmingInfo
{
    // 씨앗 아이디
    public int seedId;
    // 작물 sprite 상태 변경 변수
    public int state;
    // 자라는 시간: 시간이 지남에 따라 state 증가
    public int growthTime;

    public FarmingInfo(int seedId, int state, int growthTime)
    {
        this.seedId = seedId;
        this.state = state;
        this.growthTime = growthTime;
    }
}
