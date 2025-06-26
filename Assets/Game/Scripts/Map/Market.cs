using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 单地块市场：用于追踪本地商品的供需与价格
/// </summary>
public class Market
{
    // 本地库存（itemId → 数量）
    private Dictionary<int, float> localSupply = new Dictionary<int, float>();

    // 本地需求,实际需求（itemId → 数量）
    private Dictionary<int, float> localDemand = new Dictionary<int, float>();

    // 本地需求,真实想要的需求（itemId → 数量）
    private Dictionary<int, float> expectedDemand = new Dictionary<int, float>();


    // 历史价格（itemId → List<float>）
    private Dictionary<int, List<float>> priceHistory = new Dictionary<int, List<float>>();
    /// <summary>
    /// 本地商品的价格
    /// </summary>

    private Dictionary<int, float> localPrice = new Dictionary<int, float>();

    public float GetPrice(int itemId) =>
        localPrice.TryGetValue(itemId, out var demand) ? demand : 0f;

    /// <summary>
    /// 获取当前回合该商品的本地需求
    /// </summary>
    public float GetDemand(int itemId) =>
        localDemand.TryGetValue(itemId, out var demand) ? demand : 0f;

    public float GetSupply(int itemId) =>
        localSupply.TryGetValue(itemId, out var supply) ? supply : 0f;

    public float GetExpectedDemand(int itemId) =>
        expectedDemand.TryGetValue(itemId, out var expect) ? expect : 0f;

    public void AddSupply(int itemId, float amount)
    {
        if (!localSupply.ContainsKey(itemId)) localSupply[itemId] = 0;
        localSupply[itemId] += amount;
    }

    public void AddDemand(int itemId, float amount)
    {
        if (!localDemand.ContainsKey(itemId)) localDemand[itemId] = 0;
        localDemand[itemId] += amount;
    }

    public void AddExpectedDemand(int itemId, float amount)
    {
        if (!expectedDemand.ContainsKey(itemId)) expectedDemand[itemId] = 0;
        expectedDemand[itemId] += amount;
    }

    /// <summary>
    /// 估计该商品在本地市场中的价格（基于供需与历史价格）
    /// </summary>
    public float EstimatePrice(int itemId)
    {
        float actualDemand = GetDemand(itemId);               // 实际有支付能力的需求
        float expectedDemand = GetExpectedDemand(itemId);     // 真实想要的需求
        float supply = GetSupply(itemId);

        // 获取历史基准价格
        float basePrice = 10f;
        if (priceHistory.TryGetValue(itemId, out var history) && history.Count > 0)
            basePrice = history.Average();

        // 如果没有预期需求，退回到实际需求估价（兼容性）
        if (expectedDemand < actualDemand)
            expectedDemand = actualDemand;

        // 价格估算公式
        float price;

        if (supply > 0)
        {
            float effectiveRatio = actualDemand / supply;
            float pressureRatio = expectedDemand / supply;

            // 综合估计价格（加入购买意愿）
            price = basePrice * Mathf.Lerp(effectiveRatio, pressureRatio, 0.3f); // 30% 心理预期影响
        }
        else if (expectedDemand > 0)
        {
            price = basePrice * 2f; // 极端短缺，直接翻倍
        }
        else
        {
            price = basePrice; // 无需求无供给
        }

        // 限制价格波动幅度（避免跳动太大）
        price = Mathf.Clamp(price, basePrice * 0.5f, basePrice * 2f);

        // 更新价格历史
        if (!priceHistory.ContainsKey(itemId))
            priceHistory[itemId] = new List<float>();

        priceHistory[itemId].Add(price);
        if (priceHistory[itemId].Count > 10)
            priceHistory[itemId].RemoveAt(0);

        return price;
    }


    /// <summary>
    /// 设置本地库存
    /// </summary>
    public void SetSupply(int itemId, float amount)
    {
        localSupply[itemId] = amount;
    }

    /// <summary>
    /// 设置本地需求
    /// </summary>
    public void SetDemand(int itemId, float amount)
    {
        localDemand[itemId] = amount;
    }
    /// <summary>
    /// 设置本地需求
    /// </summary>
    public void SetExpectedDemand(int itemId, float amount)
    {
        expectedDemand[itemId] = amount;
    }
    /// <summary>
    /// 清除需求和供给（如新回合开始）
    /// </summary>
    public void ResetRound()
    {
        localDemand.Clear();
        localSupply.Clear();
        expectedDemand.Clear();
    }
}

/// <summary>
/// 定义与市场交互的行为接口
/// </summary>
public interface IMarketParticipant
{
    /// <summary>
    /// 获取当前可用资金（金币等）
    /// </summary>
    float GetAvailableMoney();

    /// <summary>
    /// 请求购买某商品及数量
    /// 返回是否成功（预算+库存）
    /// </summary>
    bool TryBuyItem(Market market,int itemId, int quantity, float pricePerUnit);

    /// <summary>
    /// 获取对某商品的预期需求数量（理论想买，预算无关）
    /// </summary>
    float GetExpectedDemand(int itemId);

    /// <summary>
    /// 获取当前持有某商品的库存数量
    /// </summary>
    float GetOwnedQuantity(int itemId);

    /// <summary>
    /// 交易完成后通知（如更新库存，资金等）
    /// </summary>
    void OnTransactionCompleted(int itemId, int quantity, float totalPrice, bool success);
}

public static class MarketParticipantExtensions
{
    // market: 购买发生的市场环境
    public static bool TryBuyItem(this IMarketParticipant participant, Market market, int itemId, int quantity)
    {
        return true;
    }
}
