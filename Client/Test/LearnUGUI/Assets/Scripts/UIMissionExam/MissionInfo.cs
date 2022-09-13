public class MissionInfo : RawData
{
    public int state;   // 0 : doing, 1 : complete, 2 : completeCheck
    public int progress;

    public MissionInfo(int id, int state = 0, int progress = 1)
    {
        this.id = id;
        this.state = state;
        this.progress = progress;
    }
}
