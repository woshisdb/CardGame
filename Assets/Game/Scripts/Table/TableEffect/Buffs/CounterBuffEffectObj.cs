using System;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public struct CounterBuffObjData: TableEffectData,IAddBuffEffectData
{
    public Action done;
    public IAnimalCard card;
    public CounterBuffObjData(Action done, TableEffectEnum buffType, IAnimalCard card)
    {
        this.done = done;
        this.card = card;
    }

    public TableEffectEnum GetEffectEnum()
    {
        return TableEffectEnum.Counter;
    }

    public IAnimalCard getCard()
    {
        return card;
    }

    public Action Done()
    {
        return done;
    }
}

[CreateAssetMenu(fileName = "CounterBuffEffectObj", menuName = "TableEffectObj/CounterBuffEffectObj")]
public class CounterBuffEffectObj:AddBuffEffectObj
{
    public override void AddBuff(IAnimalCard card,IAddBuffEffectData addBuffObjData)
    {
        var data = (CounterBuffObjData)addBuffObjData;
    }
}