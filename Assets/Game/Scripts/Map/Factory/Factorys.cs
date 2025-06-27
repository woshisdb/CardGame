using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory1 : Factory
{
    public Factory1() : base("工厂1",new ProductionLine(1,1,1,
            new Dictionary<int, int>()
            {
                {1,1}
            }), 2,3)
    {
        
    }

    public override FactoryType GetFactoryType()
    {
        return FactoryType.Factory1;
    }
}
