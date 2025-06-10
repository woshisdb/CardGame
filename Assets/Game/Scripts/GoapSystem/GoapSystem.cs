using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using YBZ.Algorithm;

public class GoapSystem
{
    public void Plan(Func<GoapState, float> distance, GoapState start, GoapAction[] actions)
    {
        var runway = new PriorityQueue<(GoapState,float)>((a, b) => {
                if (a.Item2 < b.Item2) return -1;
                else if (a == b) return 0;
                else if (a.Item2 < b.Item2) return 1;
                return 0;
            });
        
    }
}