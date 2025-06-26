using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtisanWork : Work
{
    public ArtisanWork() : base(
        (int)WorkType.Artisan,
        "见习工匠",
        "学习基础的工艺和制作技能")
    {
    }
}

public class Artisan2Work : Work
{
    public Artisan2Work() : base(
        (int)WorkType.Artisan,
        "初级工匠",
        "制作简单的手工艺品，掌握基本工具使用")
    {
    }
}

public class ArtisanMasterWork : Work
{
    public ArtisanMasterWork() : base(
        (int)WorkType.Artisan,
        "高级工匠",
        "精通工艺，制作精致的手工艺术品和工具")
    {
    }
}

public class ArtisanGrandmasterWork : Work
{
    public ArtisanGrandmasterWork() : base(
        (int)WorkType.Artisan,
        "工艺大师",
        "创作独特且具收藏价值的艺术作品，成为工艺界的领袖")
    {
    }
}

public class ArtisanLegendWork : Work
{
    public ArtisanLegendWork() : base(
        (int)WorkType.Artisan,
        "传奇工匠",
        "创作出跨时代的艺术杰作，成为全球艺术界的传奇")
    {
    }
}
