using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefWork : Work
{
    public ChefWork() : base(
        (int)WorkType.Chef,
        "见习厨师",
        "掌握烹饪的基础技能")
    {
    }
}

public class Chef2Work : Work
{
    public Chef2Work() : base(
        (int)WorkType.Chef,
        "初级厨师",
        "准备简单的菜肴，熟悉厨房操作")
    {
    }
}

public class ChefMasterWork : Work
{
    public ChefMasterWork() : base(
        (int)WorkType.Chef,
        "高级厨师",
        "烹饪出复杂且美味的菜肴")
    {
    }
}

public class ChefGrandmasterWork : Work
{
    public ChefGrandmasterWork() : base(
        (int)WorkType.Chef,
        "厨艺大师",
        "创作独特的菜品，引领烹饪潮流")
    {
    }
}

public class ChefGourmetWork : Work
{
    public ChefGourmetWork() : base(
        (int)WorkType.Chef,
        "美食大师",
        "成为世界级的厨艺大师，带领餐饮界风潮")
    {
    }
}
