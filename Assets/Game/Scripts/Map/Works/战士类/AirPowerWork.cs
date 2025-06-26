using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForce1Work : Work
{
    public AirForce1Work() : base(
        (int)WorkType.AirForce,
        "飞机驾驶员",
        "靠你自己了")
    {
    }
}

public class AirForce2Work : Work
{
    public AirForce2Work() : base(
        (int)WorkType.AirForce,
        "飞机中队长",
        "手下靠你了")
    {
    }
}

public class AirForce3Work : Work
{
    public AirForce3Work() : base(
        (int)WorkType.AirForce,
        "空军集团指挥",
        "赢得整个战役吧")
    {
    }
}

public class AirForce4Work : Work
{
    public AirForce4Work() : base(
        (int)WorkType.AirForce,
        "空军司令",
        "整个国家的军团指挥")
    {
    }
}