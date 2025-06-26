using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchitectWork : Work
{
    public ArchitectWork() : base(
        (int)WorkType.Architect,
        "见习建筑师",
        "学习基础的建筑设计和构造")
    {
    }
}

public class Architect2Work : Work
{
    public Architect2Work() : base(
        (int)WorkType.Architect,
        "初级建筑师",
        "参与建筑项目，负责设计和绘制初步图纸")
    {
    }
}

public class ArchitectMasterWork : Work
{
    public ArchitectMasterWork() : base(
        (int)WorkType.Architect,
        "高级建筑师",
        "独立完成建筑项目的设计，负责建筑的整体规划")
    {
    }
}

public class ArchitectGrandmasterWork : Work
{
    public ArchitectGrandmasterWork() : base(
        (int)WorkType.Architect,
        "建筑大师",
        "设计并监督多个重要建筑项目，成为行业领袖")
    {
    }
}
