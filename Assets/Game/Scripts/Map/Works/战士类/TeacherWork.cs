using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherWork : Work
{
    public TeacherWork() : base(
        (int)WorkType.Teacher,
        "见习教师",
        "学习教学基础技能，协助教授日常课程")
    {
    }
}

public class Teacher2Work : Work
{
    public Teacher2Work() : base(
        (int)WorkType.Teacher,
        "初级教师",
        "独立教授基础课程，处理学生日常问题")
    {
    }
}

public class TeacherMasterWork : Work
{
    public TeacherMasterWork() : base(
        (int)WorkType.Teacher,
        "资深教师",
        "教授高级课程，进行个性化教学，提升学生能力")
    {
    }
}

public class TeacherGrandmasterWork : Work
{
    public TeacherGrandmasterWork() : base(
        (int)WorkType.Teacher,
        "教育大师",
        "在教育领域具有极高声誉，进行教育理论研究和教学改革")
    {
    }
}

public class TeacherLegendWork : Work
{
    public TeacherLegendWork() : base(
        (int)WorkType.Teacher,
        "教育传奇",
        "教育界的传奇人物，培养了无数优秀人才，影响整个社会")
    {
    }
}
