﻿using System;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public struct AttackBuffObjData : TableEffectData, IAddBuffEffectData
{
    public Action done;
    public IAnimalCard card;
    public int effectTime;
    public Func<IAnimalCard,int, int> func;
    public AttackBuffObjData(Action done, IAnimalCard card, int effectTime,Func<IAnimalCard,int,int> func)
    {
        this.done = done;
        this.card = card;
        this.effectTime = effectTime;
        this.func = func;
    }

    public TableEffectEnum GetEffectEnum()
    {
        return TableEffectEnum.Attack;
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

[CreateAssetMenu(fileName = "AttackBuffEffectObj", menuName = "TableEffectObj/AttackBuffEffectObj")]
public class AttackBuffEffectObj : AddBuffEffectObj
{
    public override void AddBuff(IAnimalCard card, IAddBuffEffectData addBuffObjData)
    {
        var data = (AttackBuffObjData)addBuffObjData;
        TableModel.gameRule.GameRuleProcessor.Register(ProcessType.Attack,data.func);
        var icon = (data.getCard().GetSlot() as OneCardSlotView).AddEffectIcon(effectIcon);
        int passTime = data.GetEffectTime();
        Action<Action> gameAction = null;
        gameAction = e =>
        {
            passTime--;
            if (passTime <= 0)
            {
                TableModel.gameRule.GameRuleProcessor.Remove(ProcessType.Attack, data.func);
                GameObject.Destroy(icon);
            }
        };
        this.AddGameRuleListen(ActionTimePointType.Bef,data.card, data.GetEffectTime(), gameAction);
    }
}