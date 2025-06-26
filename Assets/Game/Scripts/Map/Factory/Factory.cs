using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionLine
{
    public string ProductName { get; private set; }
    public int ProductId { get; private set; } // 产物ID
    public int ProductAmount { get; private set; } // 每次生产的数量

    // 原料ID和数量
    public Dictionary<int, int> RequiredMaterials { get; private set; }

    // 构造函数：初始化产品信息和所需的原料
    public ProductionLine(int productId, string productName, int productAmount, Dictionary<int, int> requiredMaterials)
    {
        ProductId = productId;
        ProductName = productName;
        ProductAmount = productAmount;
        RequiredMaterials = requiredMaterials;
    }

    // 执行生产：消耗原料，生产产物
    public bool Produce(Dictionary<int, int> availableMaterials)
    {
        // 检查是否有足够的原料
        foreach (var material in RequiredMaterials)
        {
            if (!availableMaterials.ContainsKey(material.Key) || availableMaterials[material.Key] < material.Value)
            {
                Debug.Log($"生产失败：原料 {material.Key} 不足！");
                return false; // 原料不足，生产失败
            }
        }

        // 消耗原料
        foreach (var material in RequiredMaterials)
        {
            availableMaterials[material.Key] -= material.Value;
        }

        // 成功生产
        Debug.Log($"成功生产 {ProductAmount} 单位的 {ProductName} (ID: {ProductId})");
        return true;
    }

    // 显示生产线状态
    public void ShowProductionLineStatus()
    {
        Debug.Log($"产品：{ProductName} (ID: {ProductId}), 生产量：{ProductAmount} 单位");
        Debug.Log("所需原料：");
        foreach (var material in RequiredMaterials)
        {
            Debug.Log($"原料ID: {material.Key}, 数量: {material.Value}");
        }
    }
}


public class Factory
{
    public string FactoryName { get; private set; }
    public ProductionLine ProductionLine { get; private set; } // 单条生产线

    // 当前原料库存：原料ID -> 数量
    public Dictionary<int, int> MaterialsInventory { get; private set; }

    // 构造函数：初始化工厂名称、生产线和原料库存
    public Factory(string factoryName, ProductionLine productionLine, Dictionary<int, int> initialMaterials)
    {
        FactoryName = factoryName;
        ProductionLine = productionLine;
        MaterialsInventory = initialMaterials;
    }

    // 执行生产：工厂调用生产线进行生产，消耗原料并生成产物
    public void Produce()
    {
        if (!ProductionLine.Produce(MaterialsInventory))
        {
            Console.WriteLine($"生产失败：无法生产 {ProductionLine.ProductName} (ID: {ProductionLine.ProductId})");
        }
    }

    // 显示工厂状态：显示生产线的状态以及当前原料库存
    public void ShowFactoryStatus()
    {
        Console.WriteLine($"工厂: {FactoryName}");
        ProductionLine.ShowProductionLineStatus();
        Console.WriteLine("当前原料库存：");
        foreach (var material in MaterialsInventory)
        {
            Console.WriteLine($"原料ID: {material.Key}, 当前库存: {material.Value} 单位");
        }
    }
}
