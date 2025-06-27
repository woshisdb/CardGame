using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    d1,
    d2,
}

public class ProductionLine
{
    /// <summary>
    /// 需要多久生产一批
    /// </summary>
    public int NeedTime{ get; private set; }
    public int ProductId { get; private set; } // 产物ID
    public int ProductAmount { get; private set; } // 每次生产的数量
    // 原料ID和数量
    public Dictionary<int, int> RequiredMaterials { get; private set; }

    public Factory Factory;
    // 构造函数：初始化产品信息和所需的原料
    public ProductionLine(int needTime,int productId, int productAmount, Dictionary<int, int> requiredMaterials)
    {
        NeedTime = needTime;
        ProductId = productId;
        ProductAmount = productAmount;
        RequiredMaterials = requiredMaterials;
    }

    // 执行生产：消耗原料，生产产物
    public void Produce()
    {
        Factory.RemainTime --;
        if (Factory.RemainTime == 0)
        {
            Factory.GoodsSum += Factory.ProcessSum;
        }
    }
    //来判断剩余的空位
    public int EmptySum()
    {
        return Factory.MaxResourceSum - Factory.ProcessSum - Factory.GoodsSum;
    }

    public void Bind(Factory factory)
    {
        this.Factory = factory;
    }
    // 显示生产线状态
    public void ShowProductionLineStatus()
    {
        Debug.Log($" (ID: {ProductId}), 生产量：{ProductAmount} 单位");
        Debug.Log("所需原料：");
        foreach (var material in RequiredMaterials)
        {
            Debug.Log($"原料ID: {material.Key}, 数量: {material.Value}");
        }
    }
}


public abstract class Factory
{
    public string FactoryName { get; private set; }
    public ProductionLine ProductionLine { get; private set; } // 单条生产线
    
    public int MaterialsMaxInventory { get; private set; }
    /// <summary>
    /// 最多保存的数目
    /// </summary>
    public int MaxResourceSum = 0;
    // 当前原料库存：原料ID -> 数量
    public int ResourceSum = 0;
    public int RemainTime;
    public int ProcessSum = 0;
    public int GoodsSum = 0;

    // 构造函数：初始化工厂名称、生产线和原料库存
    public Factory(string factoryName, ProductionLine productionLine, int initialMaterials,int maxResourceSum)
    {
        MaxResourceSum = maxResourceSum;
        FactoryName = factoryName;
        ProductionLine = productionLine;
        MaterialsMaxInventory = initialMaterials;
    }

    // 执行生产：工厂调用生产线进行生产，消耗原料并生成产物
    public void Produce()
    {
    }

    // 显示工厂状态：显示生产线的状态以及当前原料库存
    public void ShowFactoryStatus()
    {
    }

    public abstract FactoryType GetFactoryType();

    public int GetAllGoodsSum()
    {
        return GoodsSum * ProductionLine.ProductAmount;
    }
}

public enum FactoryType
{
    Factory1,
    Factory2
}

public static class FactoryCreator
{
    public static Dictionary<FactoryType,Func<Factory>> Factories = new Dictionary<FactoryType,Func<Factory>>()
    {
        { FactoryType.Factory1 , () => { return new Factory1();}},
    };
    public static Factory CreateFactory(FactoryType factoryType)
    {
        return Factories[factoryType]();
    }
}
