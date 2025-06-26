using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navy1Work : Work
{
    public Navy1Work() : base(
        (int)WorkType.Navy,
        "水手",
        "靠你自己了")
    {
    }
}

public class Navy2Work : Work
{
    public Navy2Work() : base(
        (int)WorkType.Navy,
        "船长",
        "手下靠你了")
    {
    }
}

public class Navy3Work : Work
{
    public Navy3Work() : base(
        (int)WorkType.Navy,
        "水军集团指挥",
        "赢得整个战役吧")
    {
    }
}

public class Navy4Work : Work
{
    public Navy4Work() : base(
        (int)WorkType.Navy,
        "水军司令",
        "整个国家的军团指挥")
    {
    }
}