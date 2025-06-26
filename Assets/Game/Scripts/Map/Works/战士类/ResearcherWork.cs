using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearcherWork : Work
{
    public ResearcherWork() : base(
        (int)WorkType.Researcher,
        "初级研究员",
        "学习基础的科研方法与实验技巧")
    {
    }
}

public class Researcher2Work : Work
{
    public Researcher2Work() : base(
        (int)WorkType.Researcher,
        "研究员",
        "开始独立开展研究项目，进行数据分析与实验设计")
    {
    }
}

public class ResearcherMasterWork : Work
{
    public ResearcherMasterWork() : base(
        (int)WorkType.Researcher,
        "资深研究员",
        "在特定领域深耕细作，发表重要的科研成果")
    {
    }
}

public class ResearcherGrandmasterWork : Work
{
    public ResearcherGrandmasterWork() : base(
        (int)WorkType.Researcher,
        "研究大师",
        "领导重要的科研项目，影响科学技术的进步")
    {
    }
}

public class ResearcherVisionaryWork : Work
{
    public ResearcherVisionaryWork() : base(
        (int)WorkType.Researcher,
        "科学先知",
        "引领科技变革，提出革命性理论，改变世界")
    {
    }
}
