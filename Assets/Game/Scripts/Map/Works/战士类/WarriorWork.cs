using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior1Work : Work
{
    public Warrior1Work() : base(
        (int)WorkType.Warrior,
        "士卒",
        "靠你自己了")
    {
    }
}

public class Warrior2Work : Work
{
    public Warrior2Work() : base(
        (int)WorkType.Warrior,
        "步兵组长",
        "手下靠你了")
    {
    }
}

public class Warrior3Work : Work
{
    public Warrior3Work() : base(
        (int)WorkType.Warrior,
        "步兵团长",
        "赢得整个战役吧")
    {
    }
}

public class Warrior4Work : Work
{
    public Warrior4Work() : base(
        (int)WorkType.Warrior,
        "步兵司令",
        "整个国家的军团指挥")
    {
    }
}