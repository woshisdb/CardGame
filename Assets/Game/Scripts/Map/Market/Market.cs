using System;
using System.Collections.Generic;
using System.Linq;

public class Market
{
    public List<Buyer> Buyers = new List<Buyer>();
    public List<Seller> Sellers = new List<Seller>();

    public void Match()
    {
        foreach (var buyer in Buyers)
        {
            switch (buyer.MatchStrategy)
            {
                case BuyerMatchStrategy.PartialMatch:
                    MatchPartial(buyer);
                    break;
                case BuyerMatchStrategy.AllOrNothing:
                    MatchAllOrNothing(buyer);
                    break;
                case BuyerMatchStrategy.PriorityMatch:
                    MatchPriority(buyer);
                    break;
            }
        }
    }

    private void MatchPartial(Buyer buyer)
    {
        foreach (var need in buyer.Needs)
        {
            var productType = need.Type;
            var neededAmount = need.Amount;
            if (neededAmount <= 0) continue;

            var sellers = Sellers
                .Where(s => s.Needs.ContainsKey(productType) && s.Needs[productType].sum > 0)
                .OrderBy(s => s.Needs[productType].price)
                .ToList();

            foreach (var seller in sellers)
            {
                var goods = seller.Needs[productType];
                int buyAmount = Math.Min(neededAmount, goods.sum);

                if (buyAmount > 0)
                {
                    goods.sum -= buyAmount;
                    buyer.AddProductPrice(productType, buyAmount);
                    seller.AddProductPrice(productType, buyAmount * goods.price);
                    neededAmount -= buyAmount;
                }

                if (neededAmount <= 0)
                    break;
            }

            need.Amount = neededAmount;
        }
    }

    private void MatchAllOrNothing(Buyer buyer)
    {
        var tentativeTrades = new List<(Seller seller, ProductType type, int amount, int price)>();

        foreach (var need in buyer.Needs)
        {
            var productType = need.Type;
            int neededAmount = need.Amount;
            int remaining = neededAmount;

            var sellers = Sellers
                .Where(s => s.Needs.ContainsKey(productType) && s.Needs[productType].sum > 0)
                .OrderBy(s => s.Needs[productType].price)
                .ToList();

            foreach (var seller in sellers)
            {
                var goods = seller.Needs[productType];
                int canSell = Math.Min(goods.sum, remaining);
                if (canSell > 0)
                {
                    tentativeTrades.Add((seller, productType, canSell, goods.price));
                    remaining -= canSell;
                }

                if (remaining <= 0) break;
            }

            if (remaining > 0)
                return; // 有一个商品无法满足 → 放弃整单交易
        }

        // 全部满足 → 执行交易
        foreach (var trade in tentativeTrades)
        {
            var goods = trade.seller.Needs[trade.type];
            goods.sum -= trade.amount;
            buyer.AddProductPrice(trade.type, trade.amount);
            trade.seller.AddProductPrice(trade.type, trade.amount * trade.price);

            var need = buyer.Needs.First(n => n.Type == trade.type);
            need.Amount -= trade.amount;
        }
    }

    private void MatchPriority(Buyer buyer)
    {
        var sortedNeeds = buyer.Needs
            .Where(n => n.Amount > 0)
            .OrderBy(n => n.Priority)
            .ToList();

        foreach (var need in sortedNeeds)
        {
            var productType = need.Type;
            var neededAmount = need.Amount;

            var sellers = Sellers
                .Where(s => s.Needs.ContainsKey(productType) && s.Needs[productType].sum > 0)
                .OrderBy(s => s.Needs[productType].price)
                .ToList();

            foreach (var seller in sellers)
            {
                var goods = seller.Needs[productType];
                int buyAmount = Math.Min(neededAmount, goods.sum);

                if (buyAmount > 0)
                {
                    goods.sum -= buyAmount;
                    buyer.AddProductPrice(productType, buyAmount);
                    seller.AddProductPrice(productType, buyAmount * goods.price);
                    neededAmount -= buyAmount;
                }

                if (neededAmount <= 0)
                    break;
            }

            need.Amount = neededAmount;
        }
    }

    public void PrintMarketStatus()
    {
        Console.WriteLine("===== MARKET STATUS =====");
        foreach (var s in Sellers)
        {
            Console.WriteLine($"[Seller]");
            foreach (var kv in s.Needs)
                Console.WriteLine($"  {kv.Key}: {kv.Value.sum} left @ {kv.Value.price}");
        }

        foreach (var b in Buyers)
        {
            Console.WriteLine($"[Buyer - {b.MatchStrategy}]");
            foreach (var need in b.Needs)
                Console.WriteLine($"  Need {need.Type}: {need.Amount} left | Own: {b.GetProductCount(need.Type)}");
        }
    }
}
