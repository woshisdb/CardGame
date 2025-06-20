﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AttackSkillEffectData : CardEffectData
{
    [ShowInInspector,SerializeField]
    protected int hp;
    [ShowInInspector,SerializeField]
    protected int power;
    public AttackSkillEffectData(ICardEffect cardEffect) : base(cardEffect)
    {
        var data = cardEffect as AttackSkillEffect;
        hp = data.hp;
        power = data.power;
    }

    public override CardEffectData Clone()
    {
       var ret = new AttackSkillEffectData(cardEffect);
       ret.hp = hp;
       ret.power = power;
       return ret;
    }

    public override int GetPower()
    {
        return gameRuleProcessor.Process(ProcessType.Power,tableModel.gameRule.owner,power);//power;
    }
    public int GetHp()
    {
        return gameRuleProcessor.Process(ProcessType.Attack,tableModel.gameRule.owner,hp);
    }
}

[CreateAssetMenu(fileName = "AttackSkillEffect", menuName = "CardEffect/AttackSkillEffect")]
public class AttackSkillEffect : CardEffect, ISendEvent
{
    public int hp;
    public int power;
    public AttackSkillEffect()
    {
        cardEffectEnum = CardEffectEnum.AttackSkil;
    }
    public override TableExeData Effect(CardEffectData effectData, TableModel table, CardModel card, Action done)
    {
        var eff = effectData as AttackSkillEffectData;
        return Cost(eff.GetPower(),new RemoveHandCardData(card, () => {
                State.Next(
                new SelectSlotData((slot) =>
                {
                    return slot is OneCardSlotView && !((OneCardSlotView)slot).IsEmpty();
                },
                (slot) =>
                {
                    State.Next(new TableChangeHpData(eff.GetHp(), null, (slot as OneCardSlotView).cardModel as IAnimalCard, done));
                }));
            }, ()=>{}));
    }
    public override CardEffectData EffectData()
    {
        return new AttackSkillEffectData(this);
    }

    public override string GetEffectStr(CardEffectData data)
    {
        var cd = data as AttackSkillEffectData;
        if(cd.GetHp() < 0)
        return "造成"+ Math.Abs( cd.GetHp() )+"点伤害";
        else if(cd.GetHp() > 0)
        {
            return "回复" + Math.Abs(cd.GetHp() )+ "点生命";
        }
        else
        {
            return "什么都不做";
        }
    }
}