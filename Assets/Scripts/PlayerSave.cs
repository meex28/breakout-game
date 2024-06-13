using System;

public class SavedLevel {
    public int levelIndex;
    public DateTime saveDatetime;
    public int lossCount;

    public SavedLevel(int levelIndex, DateTime saveDatetime, int lossCount)
    {
        this.levelIndex = levelIndex;
        this.saveDatetime = saveDatetime;
        this.lossCount = lossCount;
    }
}