using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistWork : Work
{
    public AlchemistWork() : base(
        (int)WorkType.Alchemist,
        "见习炼金术士",
        "靠你自己了")
    {
    }
}

public class Alchemist2Work : Work
{
    public Alchemist2Work() : base(
        (int)WorkType.Alchemist,
        "炼金专家",
        "负责大部分药剂生产")
    {
    }
}

public class AlchemistMasterWork : Work
{
    public AlchemistMasterWork() : base(
        (int)WorkType.Alchemist,
        "炼金术大师",
        "炼制高级药剂与魔法物品")
    {
    }
}

public class AlchemistGrandmasterWork : Work
{
    public AlchemistGrandmasterWork() : base(
        (int)WorkType.Alchemist,
        "炼金术大宗师",
        "掌控元素，创造奇迹")
    {
    }
}