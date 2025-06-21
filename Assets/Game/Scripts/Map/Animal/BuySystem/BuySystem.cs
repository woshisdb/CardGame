using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Item.cs
public class Item
{
    public int Id;
    public string Name;
    public string Description;
    public int Price;       // 单价（单位：金币）
    public bool Consumable; // 是否可消耗（药、食物等）

    public Item(int id, string name, string description, int price, bool consumable = true)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Consumable = consumable;
    }
}
// Shop.cs
public static class ItemTools
{
    public static Item GetItem(int id)
    {
        return GameArchitect.Instance.resConfig.goods[id];
    }
}
public class Shop
{
    public int MoneyItemId()
    {
        return 0;
    }
    private Dictionary<int, int> inventory = new Dictionary<int,int>();

    public void AddItem(Item item, int quantity)
    {
        if (inventory.ContainsKey(item.Id))
            inventory[item.Id] =  inventory[item.Id] + quantity;
        else
            inventory[item.Id] = ( quantity);
    }
    public void AddItem(int item, int quantity)
    {
        if (inventory.ContainsKey(item))
            inventory[item] = inventory[item] + quantity;
        else
            inventory[item] = (quantity);
    }
    public bool HasStock(int itemId, int quantity)
    {
        return inventory.ContainsKey(itemId) && inventory[itemId] >= quantity;
    }

    public Item GetItem(int itemId)
    {
        return inventory.ContainsKey(itemId) ? ItemTools.GetItem( inventory[itemId]) : null;
    }

    public bool ReduceStock(int itemId, int quantity)
    {
        if (!HasStock(itemId, quantity)) return false;
        inventory[itemId] = (inventory[itemId] - quantity);
        return true;
    }

    public Dictionary<int, int> GetInventory()
    {
        return inventory;
    }
}

// PlayerInventory.cs


// ShopManager.cs
public class BuySystem : MonoBehaviour
{
    public Transform content;
    public GameObject buyUI;
    public Button button;
    public Button close;
    public void BuyAction(INpc buyer,INpc seller,Action Done)
    {
        if(buyer.IsPlayer())
        {
            var buyerShop = buyer.GetShop();
            var sellerShop = seller.GetShop();
            GameArchitect.Instance.uiManager.ToSceneUI(UIEnum.BuyUI);
            var ret = new Dictionary<int, int>();
            int allMoney = 0;
            var moneyItemId = sellerShop.MoneyItemId();
            foreach (var item in buyerShop.GetInventory())
            {
                var itemObj = ItemTools.GetItem(item.Key);
                var itemUi = GameObject.Instantiate(buyUI);
                var comp = itemUi.GetComponent<BuyItemUI>();
                comp.SetItem(item.Value, itemObj, () => {
                    if(ret.ContainsKey(item.Key))
                    {
                        ret[item.Key]--;
                        allMoney -= itemObj.Price;
                        if (ret[item.Key] == 0)
                        {
                            ret.Remove(item.Key);
                        }
                    }
                }, () => {
                    if (!ret.ContainsKey(item.Key))
                    {
                        ret[item.Key] = 0;
                    }
                    allMoney += itemObj.Price;
                    ret[item.Key]++;
                });
            }
            close.onClick.RemoveAllListeners();
            close.onClick.AddListener(() => { Done(); });
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                if(allMoney<= buyerShop.GetInventory()[moneyItemId])
                {
                    foreach (var x in ret)
                    {
                        TryBuyItem(buyerShop, sellerShop, x.Key, x.Value);
                    }
                    Done();
                }
            });
        }
        else
        {

        }
    }
    public static bool TryBuyItem(Shop player, Shop shop, int itemId, int quantity)
    {
        var moneyItemId = shop.MoneyItemId();
        Item item = shop.GetItem(itemId);
        if (item == null)
        {
            Debug.Log("商品不存在！");
            return false;
        }

        if (!shop.HasStock(itemId, quantity))
        {
            Debug.Log("商店库存不足！");
            return false;
        }

        int totalCost = item.Price * quantity;

        // 检查玩家是否有足够金币
        if (!player.HasStock(moneyItemId, totalCost))
        {
            Debug.Log("金币不足！");
            return false;
        }

        // 扣除金币
        player.ReduceStock(moneyItemId, totalCost);

        // 给商店加金币（等价交换）
        Item coin = ItemTools.GetItem(moneyItemId);
        shop.AddItem(coin, totalCost);

        // 给玩家添加商品
        player.AddItem(item, quantity);

        // 商店减少商品库存
        shop.ReduceStock(itemId, quantity);

        Debug.Log($"购买成功：{item.Name} x{quantity}，花费金币：{totalCost}");
        return true;
    }
}
