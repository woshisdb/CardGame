using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectData
{
    public ICardEffect cardEffect;
    public EffectData(ICardEffect cardEffect)
    {
        this.cardEffect = cardEffect;
    }
}

public interface ICardEffect
{
    bool CanExe(EffectData effectData, TableModel table, CardModel card);
    /// <summary>
    /// 卡牌效果
    /// </summary>
    TableExeData Effect(EffectData effectData, TableModel table, CardModel card);

    /// <summary>
    /// 效果数据
    /// </summary>
    /// <returns></returns>
    EffectData EffectData();
}
