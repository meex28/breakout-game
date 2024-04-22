using System;

public class SavedLevel {
    public int levelIndex;
    public DateTime saveDatetime;

    public SavedLevel(int levelIndex, DateTime saveDatetime)
    {
        this.levelIndex = levelIndex;
        this.saveDatetime = saveDatetime;
    }
}