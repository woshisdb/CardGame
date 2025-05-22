using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectData
{
    public ICardEffect cardEffect;
    public CardEffectData(ICardEffect cardEffect)
    {
        this.cardEffect = cardEffect;
    }
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
}
