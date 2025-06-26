using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorWork : Work
{
    public DoctorWork() : base(
        (int)WorkType.Doctor,
        "实习医生",
        "学习基础医学知识和诊疗技巧")
    {
    }
}

public class Doctor2Work : Work
{
    public Doctor2Work() : base(
        (int)WorkType.Doctor,
        "普通医生",
        "开始独立诊疗，负责常见病的治疗")
    {
    }
}

public class DoctorMasterWork : Work
{
    public DoctorMasterWork() : base(
        (int)WorkType.Doctor,
        "资深医生",
        "擅长诊断和治疗复杂病症，开始专注某一科目")
    {
    }
}

public class DoctorGrandmasterWork : Work
{
    public DoctorGrandmasterWork() : base(
        (int)WorkType.Doctor,
        "医学大师",
        "成为顶级专家，领导医学研究和治疗前沿")
    {
    }
}

public class DoctorLegendWork : Work
{
    public DoctorLegendWork() : base(
        (int)WorkType.Doctor,
        "医学传奇",
        "全球医学界的领军人物，影响医学发展和人类健康")
    {
    }
}
