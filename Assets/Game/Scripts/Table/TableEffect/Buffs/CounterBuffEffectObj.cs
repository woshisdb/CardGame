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
    public int effectTime;
    public CounterBuffObjData(Action done,IAnimalCard card,int effectTime)
    {
        this.done = done;
        this.card = card;
        this.effectTime = effectTime;
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

    public int GetEffectTime()
    {
        return effectTime;
    }
}

[CreateAssetMenu(fileName = "CounterBuffEffectObj", menuName = "TableEffectObj/CounterBuffEffectObj")]
public class CounterBuffEffectObj:AddBuffEffectObj
{
    public override void AddBuff(IAnimalCard card,IAddBuffEffectData addBuffObjData)
    {
        var data = (CounterBuffObjData)addBuffObjData;
        var effect = TableModel.AddAfterActionToTable<TableChangeHpData>((e, done) =>
        {
            var nowAct = (TableChangeHpData)e;
            if (nowAct.to == data.card && nowAct.hpChange<0)
            {
                State.Next(new TableChangeHpData(-1,nowAct.to,nowAct.from,done,false));
            }
        });
        SetEffectPassTime<TableChangeHpData>(ActionTimePointType.Bef, ActionTimePointType.After, addBuffObjData.GetEffectTime(), effect, data);
    }
}