using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TableCircleEnum
{
    Pending,//等待阶段.等待卡牌
    SelectSloting,//正在选择Slot
    SelectCarding,//正在选择卡牌
}


/// <summary>
/// Table的循环
/// </summary>
public class TableCircle
{
    public TableCircleEnum circleEnum;
    public void SetCircle(TableCircleEnum tableCircleEnum)
    {
        this.circleEnum = tableCircleEnum;
    }
}
