using System;
using System.Collections.Generic;


public abstract class GoapAction
{
    public abstract bool CanRun(GoapState state);

    public abstract GoapState Run(GoapState state,out float cost);
}