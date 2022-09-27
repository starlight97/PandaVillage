using System.Collections;
using System.Collections.Generic;

public class PlayerInfo
{
    // 인벤토리 정보
    public Inventory inventory;
    // 유저 이름
    public string playerName;
    // 선호하는 펫
    public string pet;
    // 유저 보유 골드
    public int gold;
    // 게임 진행 시간 minute만 저장해서 수식으로 뿌릴 예정
    public int playMinute;
    // 플레이어 스태미너
    public int currentEnegy;
    // 광산에서 사용할 체력
    public int currentHp;

    public PlayerInfo(string playerName, string pet, int gold = 500,
        int playMinute = 0, int currentEnegy = 100, int currentHp = 100)
    {
        inventory = new Inventory(12);
        this.playerName = playerName;
        this.pet = pet;
        this.gold = gold;
        this.playMinute = playMinute;
        this.currentEnegy = currentEnegy;
        this.currentHp = currentHp;

    }
}
