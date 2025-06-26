using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineWork : Work
{
    public MachineWork() : base(
        (int)WorkType.Machiner,
        "机械师",
        "靠你自己了")
    {
    }
}

public class Machine2Work : Work
{
    public Machine2Work() : base(
        (int)WorkType.Machiner,
        "机械长官",
        "负责零件生产")
    {
    }
}


public class Machine3Work : Work
{
    public Machine3Work() : base(
        (int)WorkType.Machiner,
        "机械总设计师",
        "总设计师")
    {
    }
}
