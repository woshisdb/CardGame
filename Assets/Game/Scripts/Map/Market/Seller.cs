using System;
using System.Collections.Generic;

public class Goods
{
    public int sum;
    public int price;
    public ProductType ProductType;
}
public class Seller
{
    public Dictionary<ProductType, Goods> Needs;
    /// <summary>
    /// 获取数目
    /// </summary>
    public Func<ProductType,int> GetProductCount;
    /// <summary>
    /// 添加数目
    /// </summary>
    public Action<ProductType,int> AddProductPrice;

    public Seller(Func<ProductType,int> GetProductCount,Action<ProductType,int> AddProductPrice)
    {
        this.AddProductPrice = AddProductPrice;
        this.GetProductCount = GetProductCount;
        Needs = new Dictionary<ProductType, Goods>();
    }
    
}

