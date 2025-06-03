using System;
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
    public AttackBuffObjData(Action done, IAnimalCard card, int effectTime)
    {
        this.done = done;
        this.card = card;
        this.effectTime = effectTime;
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
        data.card.RegisterAttackProcess(new AttackProcesser(e =>
        {
            return 100;
        }));
        int passTime = 3;
        this.AddGameRuleListen(ActionTimePointType.Bef,data.card,1, new GameAction(e =>
        {
            passTime--;
            if (passTime <= 0)
            {
                TableModel.RemoveAfterActionFromTable<T>(linkAction);
                GameObject.Destroy(icon);
            }
        }););
    }
}