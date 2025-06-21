using System.Collections.Generic;

public class NpcSetManager
{
    public List<INpc> npcs;
    public NpcSetManager(SaveData saveData)
    {
        npcs=saveData.saveFile.npcs;
    }
}