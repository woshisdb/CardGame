using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkilData : SlotEffectData
{
    public string Name;

    public AttackSkilData(ISlotEffect slotEffect) : base(slotEffect)
    {
    }
}

[CreateAssetMenu(fileName = "EnemyAttackSkil", menuName = "Effect/EnemyAttackSkil")]
public class EnemyAttackSkil : SlotEffect, ISendEvent
{
    public int attackCount;
    public EnemyAttackSkil()
    {
        slotEffectEnum = SlotEffectEnum.EnemyAttackSkil;
    }
    public override bool CanExe(SlotEffectData effectData, TableModel table, SlotView slot)
    {
        return true;
    }

    public override TableExeData Effect(SlotEffectData effectData, TableModel table, SlotView card,Action done)
    {
        var enemy = (card as OneCardSlotView).GetCardModel() as EnemyCardModel;
        var heroSlot = table.FindSlotByTag(SlotTag.HeroSlot) as OneCardSlotView;
        var hero = heroSlot.cardModel as HeroCardModel;
        return new TableChangeHpData(attackCount, enemy, hero, done);
    }

    public override SlotEffectData EffectData()
    {
        return new AttackSkilData(this);
    }
}