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
    /// ����Ч��
    /// </summary>
    TableExeData Effect(EffectData effectData, TableModel table, CardModel card);

    /// <summary>
    /// Ч������
    /// </summary>
    /// <returns></returns>
    EffectData EffectData();
}
