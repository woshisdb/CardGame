using System;
using System.Collections.Generic;

public enum BuyerMatchStrategy
{
    PartialMatch,
    AllOrNothing,
    PriorityMatch   // 新增
}
public class BuyerNeed
{
    public ProductType Type;
    public int Amount;
    public int Priority;  // 越小优先级越高
}
public class Buyer
{
    public List<BuyerNeed> Needs;  // 支持优先级排序

    public Func<ProductType, int> GetProductCount;
    public Action<ProductType, int> AddProductPrice;
    public BuyerMatchStrategy MatchStrategy;

    public Buyer(Func<ProductType, int> getCount, Action<ProductType, int> addPrice, BuyerMatchStrategy strategy)
    {
        GetProductCount = getCount;
        AddProductPrice = addPrice;
        MatchStrategy = strategy;
        Needs = new List<BuyerNeed>();
    }

    public void AddNeed(ProductType type, int amount, int priority)
    {
        Needs.Add(new BuyerNeed { Type = type, Amount = amount, Priority = priority });
    }
}
