// public class GetCardFromDeckEffect
// {
//     
// }
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCardFromDeckEffectData : CardEffectData
{
    public int num;
    public int power;
    public GetCardFromDeckEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
    }

    public override CardEffectData Clone()
    {
        var ret = new GetCardFromDeckEffectData(cardEffect);
        ret.num = num;
        ret.power = power;
        return ret;
    }

    public override int GetPower()
    {
        return power;
    }

    public int GetCardNum()
    {
        return num;
    }
}

[CreateAssetMenu(fileName = "GetCardFromDeckEffect", menuName = "CardEffect/GetCardFromDeckEffect")]
public class GetCardFromDeckEffect : CardEffect, ISendEvent
{
    public GetCardFromDeckEffect()
    {
        cardEffectEnum = CardEffectEnum.GetCardFromDeckEffect;
    }

    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card,Action done)
    {
        var num = (effectData as GetCardFromDeckEffectData).GetCardNum();
        return Cost(effectData.GetPower(),new GetCardFromDeck(num,()=>
        {
            State.End(done);
        },()=>
        {
            State.End(done);
        }));
    }

    public override CardEffectData EffectData()
    {
        return new GetCardFromDeckEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as GetCardFromDeckEffectData;
        return "消耗"+cd.GetPower()+"抽"+cd.GetCardNum()+"张卡";
    }
}
