using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffectData
{
    public ICardEffect cardEffect;
    public abstract int GetPower();
    public CardEffectData(ICardEffect cardEffect)
    {
        this.cardEffect = cardEffect;
    }
    public abstract CardEffectData Clone();


}

public interface ICardEffect
{
    bool CanExe(CardEffectData effectData, TableModel table, CardModel card);
    /// <summary>
    /// ����Ч��
    /// </summary>
    TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done);

    /// <summary>
    /// Ч������
    /// </summary>
    /// <returns></returns>
    CardEffectData EffectData();

    /// <summary>
    /// 获取字符串
    /// </summary>
    /// <returns></returns>
    string GetEffectStr(CardEffectData data);
}
