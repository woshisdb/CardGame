using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantWork : Work
{
    public MerchantWork() : base(
        (int)WorkType.Merchant,
        "初级商人",
        "负责小规模的商品交易")
    {
    }
}

public class Merchant2Work : Work
{
    public Merchant2Work() : base(
        (int)WorkType.Merchant,
        "中级商人",
        "管理更大范围的商品流通")
    {
    }
}

public class MerchantMasterWork : Work
{
    public MerchantMasterWork() : base(
        (int)WorkType.Merchant,
        "高级商人",
        "参与国际贸易，精通市场运作")
    {
    }
}

public class MerchantGrandmasterWork : Work
{
    public MerchantGrandmasterWork() : base(
        (int)WorkType.Merchant,
        "商业巨头",
        "拥有多个商会和大规模商业帝国")
    {
    }
}

public class MerchantTycoonWork : Work
{
    public MerchantTycoonWork() : base(
        (int)WorkType.Merchant,
        "财富帝国",
        "掌控全球贸易，成为商界传奇")
    {
    }
}
