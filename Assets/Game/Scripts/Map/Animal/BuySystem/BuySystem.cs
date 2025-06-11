using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item.cs
public class Item
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public int Price { get; }       // 单价（单位：金币）
    public bool Consumable { get; } // 是否可消耗（药、食物等）

    public Item(string id, string name, string description, int price, bool consumable = true)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Consumable = consumable;
    }
}
// Shop.cs

public class Shop
{
    private Dictionary<string, (Item item, int stock)> inventory = new Dictionary<string, (Item item, int stock)>();

    public void AddItem(Item item, int quantity)
    {
        if (inventory.ContainsKey(item.Id))
            inventory[item.Id] = (item, inventory[item.Id].stock + quantity);
        else
            inventory[item.Id] = (item, quantity);
    }

    public bool HasStock(string itemId, int quantity)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId].stock >= quantity;
    }

    public Item GetItem(string itemId)
    {
        return inventory.ContainsKey(itemId) ? inventory[itemId].item : null;
    }

    public bool ReduceStock(string itemId, int quantity)
    {
        if (!HasStock(itemId, quantity)) return false;
        inventory[itemId] = (inventory[itemId].item, inventory[itemId].stock - quantity);
        return true;
    }

    public Dictionary<string, (Item item, int stock)> GetInventory()
    {
        return inventory;
    }
}

// PlayerInventory.cs

public class PlayerInventory
{
    public int Gold { get; private set; } = 1000; // 初始金钱
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddGold(int amount) => Gold += amount;
    public bool SpendGold(int amount)
    {
        if (Gold < amount) return false;
        Gold -= amount;
        return true;
    }

    public void AddItem(Item item, int quantity)
    {
        if (items.ContainsKey(item.Id))
            items[item.Id] += quantity;
        else
            items[item.Id] = quantity;
    }

    public Dictionary<string, int> GetItems() => items;
}

// ShopManager.cs
public class ShopManager
{
    public static bool TryBuyItem(PlayerInventory player, Shop shop, string itemId, int quantity)
    {
        Item item = shop.GetItem(itemId);
        if (item == null)
        {
            Debug.Log("商品不存在！");
            return false;
        }

        int totalCost = item.Price * quantity;

        if (!shop.HasStock(itemId, quantity))
        {
            Debug.Log("库存不足！");
            return false;
        }

        if (!player.SpendGold(totalCost))
        {
            Debug.Log("金钱不足！");
            return false;
        }

        shop.ReduceStock(itemId, quantity);
        player.AddItem(item, quantity);
        Debug.Log($"购买成功：{item.Name} x{quantity}");
        return true;
    }
}
